using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_Lab_02.Models
{
	public class Instructor
	{
		public int Id { get; set; }
		[Required]
		[StringLength(50)]
		public string Name { get; set; }
		[Column(TypeName = "money")]
		public decimal Salary { get; set; }

		[ForeignKey("Department")]
		public int? DepartmentId { get; set; }
		public virtual Department Department { get; set; }


	}
}
