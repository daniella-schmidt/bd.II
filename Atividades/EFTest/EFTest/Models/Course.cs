using System.ComponentModel.DataAnnotations;

namespace EFTest.Models
{
    public class Course
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public List<StudentsCourses>? StudentCourses { get; set; }
        // Nova propriedade para as matérias
        public List<Subject>? Subjects { get; set; }

    }
}
