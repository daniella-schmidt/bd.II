using EFTest.Models;
using EFTest.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EFTest.Controllers
{
    public class StudentCoursesController : Controller
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly IStudentCoursesRepository _studentCoursesRepository;
        private readonly ILogger<StudentCoursesController> _logger;

        public StudentCoursesController(
            ICourseRepository courseRepository,
            IStudentRepository studentRepository,
            IStudentCoursesRepository studentCoursesRepository,
            ILogger<StudentCoursesController> logger)
        {
            _courseRepository = courseRepository;
            _studentRepository = studentRepository;
            _studentCoursesRepository = studentCoursesRepository;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var matriculas = await _studentCoursesRepository.GetAll();
                return View(matriculas ?? new List<StudentsCourses>());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao carregar matrículas");
                TempData["ErrorMessage"] = "Erro ao carregar a lista de matrículas.";
                return View(new List<StudentsCourses>());
            }
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            try
            {
                await CarregarViewData();
                return View(new List<StudentsCourses>());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao carregar página de criação de matrícula");
                TempData["ErrorMessage"] = "Erro ao carregar a página de matrícula.";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int studentId, int[] courseIds)
        {
            try
            {
                // Validações básicas
                if (studentId == 0)
                {
                    ModelState.AddModelError("", "Selecione um estudante.");
                    await CarregarViewData();
                    return View(new List<StudentsCourses>());
                }

                if (courseIds == null || courseIds.Length == 0)
                {
                    ModelState.AddModelError("", "Selecione pelo menos um curso.");
                    await CarregarViewData();
                    return View(new List<StudentsCourses>());
                }

                var student = await _studentRepository.GetById(studentId);
                if (student == null)
                {
                    ModelState.AddModelError("", "Estudante não encontrado.");
                    await CarregarViewData();
                    return View(new List<StudentsCourses>());
                }

                var matriculasCriadas = new List<StudentsCourses>();
                var errors = new List<string>();

                foreach (var courseId in courseIds.Distinct())
                {
                    try
                    {
                        var course = await _courseRepository.GetById(courseId);
                        if (course == null)
                        {
                            errors.Add($"Curso ID {courseId} não encontrado.");
                            continue;
                        }

                        var existing = await _studentCoursesRepository.Get(studentId, courseId);
                        if (existing != null && existing.CancelDate == null)
                        {
                            errors.Add($"Já existe matrícula ativa para: {course.Name}");
                            continue;
                        }

                        var studentCourse = new StudentsCourses
                        {
                            StudentId = studentId,
                            CourseId = courseId,
                            SignDate = DateTime.Now,
                            CancelDate = null
                        };

                        await _studentCoursesRepository.Create(studentCourse);
                        matriculasCriadas.Add(studentCourse);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Erro ao matricular no curso {CourseId}", courseId);
                        errors.Add($"Erro ao processar o curso ID {courseId}.");
                    }
                }

                if (matriculasCriadas.Any())
                {
                    TempData["SuccessMessage"] = $"{matriculasCriadas.Count} matrícula(s) realizada(s) com sucesso!";

                    if (errors.Any())
                    {
                        TempData["WarningMessage"] = $"Alguns cursos não puderam ser matriculados.";
                    }

                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", errors.Any() ? string.Join("; ", errors.Take(3)) : "Nenhuma matrícula pôde ser realizada.");
                    await CarregarViewData();
                    return View(new List<StudentsCourses>());
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao criar matrículas");
                ModelState.AddModelError("", "Erro interno ao processar as matrículas.");
                await CarregarViewData();
                return View(new List<StudentsCourses>());
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Cancel(int studentId, int courseId)
        {
            try
            {
                var matricula = await _studentCoursesRepository.Get(studentId, courseId);
                if (matricula == null)
                {
                    TempData["ErrorMessage"] = "Matrícula não encontrada.";
                    return RedirectToAction("Index");
                }

                if (matricula.CancelDate.HasValue)
                {
                    TempData["WarningMessage"] = "Matrícula já estava cancelada.";
                    return RedirectToAction("Index");
                }

                matricula.CancelDate = DateTime.Now;
                await _studentCoursesRepository.Update(matricula);

                TempData["SuccessMessage"] = "Matrícula cancelada com sucesso!";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao cancelar matrícula");
                TempData["ErrorMessage"] = "Erro ao cancelar a matrícula.";
                return RedirectToAction("Index");
            }
        }

        private async Task CarregarViewData()
        {
            try
            {
                var students = await _studentRepository.GetAll();
                var courses = await _courseRepository.GetAll();

                // Garantir que as listas não sejam nulas
                var studentList = students?.ToList() ?? new List<Student>();
                var courseList = courses?.ToList() ?? new List<Course>();

                // Usar ViewData em vez de ViewBag para melhor controle
                ViewData["StudentId"] = studentList.Any()
                    ? new SelectList(studentList, "Id", "FullName")
                    : new SelectList(Enumerable.Empty<SelectListItem>());

                ViewData["CoursesList"] = courseList.Any()
                    ? courseList
                    : new List<Course>();

                ViewData["TotalStudents"] = studentList.Count;
                ViewData["TotalCourses"] = courseList.Count;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao carregar ViewData");

                // Valores padrão seguros
                ViewData["StudentId"] = new SelectList(Enumerable.Empty<SelectListItem>());
                ViewData["CoursesList"] = new List<Course>();
                ViewData["TotalStudents"] = 0;
                ViewData["TotalCourses"] = 0;
            }
        }
    }
}