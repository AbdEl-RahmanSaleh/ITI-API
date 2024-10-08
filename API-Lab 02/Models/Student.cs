﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace API_Lab_02.Models
{
	public class Student
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public int Age { get; set; }
		public string Email { get; set; }


		public int DepartmentId { get; set; }
		public virtual Department Department { get; set; }

	}
}
