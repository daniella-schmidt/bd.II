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

        public StudentCoursesController(
            ICourseRepository courseRepository,
            IStudentRepository studentRepository,
            IStudentCoursesRepository studentCoursesRepository)
        {
            _courseRepository = courseRepository;
            _studentRepository = studentRepository;
            _studentCoursesRepository = studentCoursesRepository;
        }

        public async Task<IActionResult> Index()
        {
            var matriculas = await _studentCoursesRepository.GetAll();
            return View(matriculas);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            await CarregarViewData();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(StudentsCourses studentCourse)
        {
            if (ModelState.IsValid)
            {
                // Verificar se já existe matrícula para este estudante e curso
                var existing = await _studentCoursesRepository.Get(studentCourse.StudentId, studentCourse.CourseId);
                if (existing != null)
                {
                    ModelState.AddModelError("", "Este estudante já está matriculado neste curso.");
                    await CarregarViewData();
                    return View(studentCourse);
                }

                studentCourse.SignDate = DateTime.Now;
                await _studentCoursesRepository.Create(studentCourse);

                TempData["SuccessMessage"] = "Matrícula realizada com sucesso!";
                return RedirectToAction("Index");
            }

            await CarregarViewData();
            return View(studentCourse);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Cancel(int studentId, int courseId)
        {
            var matricula = await _studentCoursesRepository.Get(studentId, courseId);
            if (matricula == null)
            {
                TempData["ErrorMessage"] = "Matrícula não encontrada.";
                return RedirectToAction("Index");
            }

            matricula.CancelDate = DateTime.Now;
            await _studentCoursesRepository.Update(matricula);

            TempData["SuccessMessage"] = "Matrícula cancelada com sucesso!";
            return RedirectToAction("Index");
        }

        private async Task CarregarViewData()
        {
            var students = await _studentRepository.GetAll();
            var courses = await _courseRepository.GetAll();

            // Verifique se as listas não são nulas e têm itens
            ViewData["StudentId"] = students != null && students.Any()
                ? new SelectList(students, "Id", "FirstMidName")
                : new SelectList(Enumerable.Empty<SelectListItem>());

            ViewData["CourseId"] = courses != null && courses.Any()
                ? new SelectList(courses, "Id", "Name")
                : new SelectList(Enumerable.Empty<SelectListItem>());
        }
    }
}