using System.ComponentModel.DataAnnotations.Schema;

namespace CampaniasLito.Models
{
    [Table("student")]
    public class Student
    {
        public string StudentId { get; set; }
        public string RollNo { get; set; }
        public string FullName { get; set; }
        public string Gender { get; set; }
        public string City { get; set; }
    }
}