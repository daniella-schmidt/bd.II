using EFTest.Data;
using EFTest.Models;
using Microsoft.EntityFrameworkCore;

namespace EFTest.Repository
{
    public class SubjectRepository : ISubjectRepository
    {
        private readonly SchoolContext _context;

        public SubjectRepository(SchoolContext context)
        {
            _context = context;
        }

        public async Task Create(Subject subject)
        {
            await _context.Subjects.AddAsync(subject);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Subject subject)
        {
            _context.Subjects.Remove(subject);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Subject>> GetAll()
        {
            return await _context.Subjects
                .Include(s => s.Course)
                .ToListAsync();
        }

        public async Task<Subject?> GetById(int id)
        {
            return await _context.Subjects
                .Include(s => s.Course)
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<List<Subject>> GetByCourseId(int courseId)
        {
            return await _context.Subjects
                .Include(s => s.Course)
                .Where(s => s.CourseId == courseId)
                .ToListAsync();
        }

        public async Task<List<Subject>> GetByName(string name)
        {
            return await _context.Subjects
                .Include(s => s.Course)
                .Where(s => s.Name!.ToLower().Contains(name.ToLower()))
                .ToListAsync();
        }

        public async Task Update(Subject subject)
        {
            _context.Subjects.Update(subject);
            await _context.SaveChangesAsync();
        }
    }
}