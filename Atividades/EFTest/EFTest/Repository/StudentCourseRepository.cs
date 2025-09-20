using EFTest.Data;
using EFTest.Models;
using Microsoft.EntityFrameworkCore;

namespace EFTest.Repository
{
    public class StudentCourseRepository : IStudentCoursesRepository
    {
        private readonly SchoolContext _context;

        public StudentCourseRepository(SchoolContext context) 
        { 
            _context = context;
        }
        public async Task Create(StudentsCourses studentCourse)
        {
            await _context.StudentsCourses.AddAsync(studentCourse);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(StudentsCourses studentCourse)
        {
            _context.Remove(studentCourse);
            await _context.SaveChangesAsync();
        }

        public async Task<StudentsCourses?> Get(int studentId, int courseId)
        {
            var data = await _context.StudentsCourses.Include(x => x.Course)
                                                     .Include(x => x.Student)
                                                     .Where(w => w.StudentId == studentId && w.CourseId == courseId)
                                                     .FirstOrDefaultAsync();
            return data;
        }

        public async Task<List<StudentsCourses>> GetAll()
        {
            var data = await _context.StudentsCourses.Include(x => x.Course)
                                                     .Include(x => x.Student)
                                                     .ToListAsync();
            return data;
        }

        public async Task<List<StudentsCourses?>> GetByCourseId(int courseId)
        {
            var data = await _context.StudentsCourses.Include(x => x.Course)
                                                     .Where(w => w.CourseId == courseId)
                                                     .ToListAsync();
            return data;
        }

        public async Task<List<StudentsCourses>> GetByCourseName(string name)
        {
            var data = await _context.StudentsCourses.Include(x => x.Course)
                                                     .Include(x => x.Student)
                                                     .Where(w => w.Course!.Name!.ToLower().Contains(name.ToLower()))
                                                     .ToListAsync();
            return data;
        }

        public async Task<List<StudentsCourses?>> GetByStudentId(int studentId)
        {
            var data = await _context.StudentsCourses.Include(x => x.Student)
                                                     .Where(w => w.StudentId == studentId)
                                                     .ToListAsync();
            return data;
        }

        public async Task<List<StudentsCourses>> GetByStudentName(string name)
        {
            var data = await _context.StudentsCourses.Include(x => x.Course)
                                                     .Include(x => x.Student)
                                                     .Where(w => w.Student!.FirstMidName!.ToLower().Contains(name.ToLower()) || w.Student!.LastName!.ToLower().Contains(name.ToLower()))
                                                     .ToListAsync();
            return data;
        }

        public async Task Update(StudentsCourses studentCourse)
        {
             _context.StudentsCourses.Update(studentCourse);
            await _context.SaveChangesAsync();
        }
    }
}
