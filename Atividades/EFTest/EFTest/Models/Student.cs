using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFTest.Models
{
    public class Student
    {
        [Key]
        public int Id { get; set; }
        public string? LastName { get; set; }
        public string? FirstMidName { get; set; }
        public DateTime EnrollmentDate { get; set; }
        public List<StudentsCourses>? StudentCourses { get; set; }

        // Propriedade apenas para exibição
        [NotMapped]
        public string FullName => $"{FirstMidName} {LastName}";
    }
}
