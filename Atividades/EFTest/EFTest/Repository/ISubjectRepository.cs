using EFTest.Models;

namespace EFTest.Repository
{
    public interface ISubjectRepository
    {
        public Task Create(Subject subject);
        public Task Update(Subject subject);
        public Task Delete(Subject subject);
        public Task<Subject?> GetById(int id);
        public Task<List<Subject>> GetAll();
        public Task<List<Subject>> GetByCourseId(int courseId);
        public Task<List<Subject>> GetByName(string name);
    }
}