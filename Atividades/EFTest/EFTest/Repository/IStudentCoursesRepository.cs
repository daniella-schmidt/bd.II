using EFTest.Models;

namespace EFTest.Repository
{
    public interface IStudentCoursesRepository
    {
        public Task Create(StudentsCourses studentCourse);
        public Task Update(StudentsCourses studentCourse);
        public Task Delete(StudentsCourses studentCourse);
        public Task<List<StudentsCourses?>> GetByCourseId(int courseId);
        public Task<List<StudentsCourses?>> GetByStudentId(int studentId);
        public Task<StudentsCourses?> Get(int studentId, int courseId);
        public Task<List<StudentsCourses>> GetAll();
        public Task<List<StudentsCourses>> GetByCourseName(string name);
        public Task<List<StudentsCourses>> GetByStudentName(string name);

    }
}
