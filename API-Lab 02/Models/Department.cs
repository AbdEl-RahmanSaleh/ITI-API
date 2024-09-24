using System.ComponentModel.DataAnnotations.Schema;

namespace API_Lab_02.Models
{
	public class Department
	{
        public int Id { get; set; }
        public string Name { get; set; }

        public int Capacity { get; set; }

        public virtual ICollection<Student> Students { get; set; } = new List<Student>();
        public virtual ICollection<Instructor> Instructors { get; set; } = new List<Instructor>();
        public int? SupervisorId { get; set; }
        public virtual Instructor Supervisor { get; set; }
    }
}
