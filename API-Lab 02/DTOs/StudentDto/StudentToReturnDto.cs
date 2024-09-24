using API_Lab_02.Models;

namespace API_Lab_02.DTOs.StudentDto
{
	public class StudentToReturnDto
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public int Age { get; set; }
		public string Email { get; set; }
		public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public string SupervisorName { get; set; }
    }
}
