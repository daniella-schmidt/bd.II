using EFTest.Models;
using EFTest.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EFTest.Controllers
{
    public class SubjectController : Controller
    {
        private readonly ISubjectRepository _subjectRepository;
        private readonly ICourseRepository _courseRepository;

        public SubjectController(ISubjectRepository subjectRepository, ICourseRepository courseRepository)
        {
            _subjectRepository = subjectRepository;
            _courseRepository = courseRepository;
        }

        public async Task<IActionResult> Index()
        {
            var subjects = await _subjectRepository.GetAll();
            return View(subjects);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            await LoadCoursesViewData();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Subject subject)
        {
            if (ModelState.IsValid)
            {
                await _subjectRepository.Create(subject);
                TempData["SuccessMessage"] = "Matéria cadastrada com sucesso!";
                return RedirectToAction("Index");
            }

            await LoadCoursesViewData();
            return View(subject);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var subject = await _subjectRepository.GetById(id);
            if (subject == null)
            {
                return NotFound();
            }

            await LoadCoursesViewData();
            return View(subject);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Subject subject)
        {
            if (id != subject.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _subjectRepository.Update(subject);
                    TempData["SuccessMessage"] = "Matéria atualizada com sucesso!";
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Erro ao atualizar a matéria.");
                }
            }

            await LoadCoursesViewData();
            return View(subject);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var subject = await _subjectRepository.GetById(id);
            if (subject == null)
            {
                return NotFound();
            }

            await _subjectRepository.Delete(subject);
            TempData["SuccessMessage"] = "Matéria excluída com sucesso!";
            return RedirectToAction("Index");
        }

        private async Task LoadCoursesViewData()
        {
            var courses = await _courseRepository.GetAll();
            ViewData["CourseId"] = new SelectList(courses, "Id", "Name");
        }
    }
}