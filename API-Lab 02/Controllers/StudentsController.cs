using API_Lab_02.DTOs.StudentDto;
using API_Lab_02.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Xml;

namespace API_Lab_02.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class StudentsController : ControllerBase
	{
		private readonly ITIContext _db;

		public StudentsController(ITIContext db)
        {
			_db = db;
		}



		/// <summary>
		/// Function To Get All Studnets
		/// </summary>
		/// <returns>
		/// 404 if No students Found
		/// 200+ Student Exists 
		/// </returns>
		[HttpGet]
		[Produces("application/json")]
		[ProducesResponseType<List<StudentToReturnDto>>(200)]
		[ProducesResponseType(typeof(void), 404)]
		public IActionResult GetAllStudents()
		{
			List<Student> students = _db.Students.ToList();
			List<StudentToReturnDto> studentsToReturn = new List<StudentToReturnDto>();

			foreach (Student student in students)
			{
				StudentToReturnDto s = new StudentToReturnDto()
				{
					Id = student.Id,
					Name = student.Name,
					Age = student.Age,
					Email = student.Email,
					DepartmentId = student.DepartmentId,
					DepartmentName = student.Department.Name,
					SupervisorName = student.Department.Supervisor.Name,
				};
				studentsToReturn.Add(s);
			}

			return Ok(studentsToReturn);
		}


		/// <summary>
		/// Function To Get Studnet By Id
		/// </summary>
		/// <param name="id">Student Id</param>
		/// <returns>
		///	404 if no student found
		///	200+ student if student exists
		/// </returns>
		/// <remarks>
		/// Request Example
		/// /api/students/2
		/// </remarks>
		[HttpGet("{id}")]
		[Produces("application/json")]
		[ProducesResponseType<StudentToReturnDto>(200)]
		[ProducesResponseType(typeof(void),404)]
		public IActionResult GetStudentById(int id)
		{

			Student student = _db.Students.SingleOrDefault(s => s.Id == id);
			if (student == null)
				return	NotFound();
			else
			{
				StudentToReturnDto s = new StudentToReturnDto()
				{
					Id = student.Id,
					Name = student.Name,
					Age = student.Age,
					Email = student.Email,
					DepartmentId = student.DepartmentId,
					DepartmentName = student.Department.Name,
					SupervisorName = student.Department.Supervisor.Name,
				};
				return Ok(s);
			}
		}


		/// <summary>
		/// Function To Add New Student
		/// </summary>
		/// <param name="studentDto">Student Data</param>
		/// <returns>
		///	400 Something Went Wrong
		///	200+ student Added Successfully
		/// </returns>
		[HttpPost]
		[Consumes("application/json")] // Accepts only JSON data
		[Produces("application/json")] // Sends only JSON data
		[ProducesResponseType<StudentToReturnDto>(200)]
		[ProducesResponseType(typeof(void), 404)]
		public IActionResult AddStudent(StudentDto studentDto) 
		{
			if (studentDto == null)
				return BadRequest();
			if(!ModelState.IsValid)
				return BadRequest();

			Student student = new Student()
			{
				Name = studentDto.Name,
				Age = studentDto.Age,
				Email = studentDto.Email,
				DepartmentId = studentDto.DepartmentId,
			};

			_db.Students.Add(student);
			_db.SaveChanges();

			return RedirectToAction("GetStudentById",new {id = student.Id});

		}


		/// <summary>
		/// Function To Edit Student
		/// </summary>
		/// <param name="id">Student Id</param>
		/// <param name="studentDto">Student New Data</param>
		/// <returns>
		///	404 Student Doesn't Exists
		///	200+ student Updated Successfully
		/// </returns>
		[HttpPut]
		[ProducesResponseType(typeof(void), 201)]
		[ProducesResponseType(typeof(void), 404)]
		public IActionResult EditStudent(int id,StudentDto studentDto)
		{
			Student student = _db.Students.SingleOrDefault(s => s.Id == id);
			if (student == null)
				return NotFound();

			if (!ModelState.IsValid)
				return BadRequest();

			student.Name = studentDto.Name;
			student.Age = studentDto.Age;
			student.Email = studentDto.Email;
			student.DepartmentId = studentDto.DepartmentId;

			_db.Students.Update(student);
			_db.SaveChanges();

			return NoContent();
		}

		/// <summary>
		/// Function To Delete Student
		/// </summary>
		/// <param name="id">Student Id</param>
		/// <returns>
		/// 404 Student Doesn't Exists
		///	200+ student Deleted Successfully
		/// </returns>
		[HttpDelete]
		public IActionResult DeleteStudent(int id)
		{
			Student s = _db.Students.Find(id);
			if (s == null)
				return NotFound();
			_db.Students.Remove(s);
			_db.SaveChanges();

			//return RedirectToAction("GetAllStudents");
			return CreatedAtAction("GetAllStudents", null, null);
		}




		/// <summary>
		/// funcion To return a specific student with the name
		/// </summary>
		/// <param name="name">Student Name</param>
		/// <returns>
		/// 404 if No students Found
		/// 200+ Student Exists 
		/// </returns>
		[HttpGet("/api/search")]
		[ProducesResponseType<List<StudentToReturnDto>>(200)]
		[ProducesResponseType(typeof(void), 404)]
		public IActionResult SearchStudents(string name)
		{
			List<Student> students = _db.Students.Where(s => s.Name.ToLower().Trim().Contains(name.ToLower().Trim())).ToList();
			List<StudentToReturnDto> studentsToReturn = new List<StudentToReturnDto>();

			foreach (Student student in students)
			{
				StudentToReturnDto s = new StudentToReturnDto()
				{
					Id = student.Id,
					Name = student.Name,
					Age = student.Age,
					Email = student.Email,
					DepartmentId = student.DepartmentId,
					DepartmentName = student.Department.Name,
					SupervisorName = student.Department.Supervisor.Name,
				};
				studentsToReturn.Add(s);
			}
			return Ok(studentsToReturn);
		}



		/// <summary>
		/// Function that creat pagination
		/// </summary>
		/// <param name="Page">Page Index</param>
		/// <param name="PageSize">Number Of Element</param>
		/// <returns>
		/// 200+ Student Exists 
		/// 404 if No students Found
		/// </returns>
		[HttpGet("/api/stdPagination")]
		public IActionResult GetStudentsPagination(int Page = 1, int PageSize = 2)
		{
			var students = _db.Students.ToList();
			List<StudentToReturnDto> studentsToReturn = new List<StudentToReturnDto>();

			foreach (Student student in students)
			{
				StudentToReturnDto s = new StudentToReturnDto()
				{
					Id = student.Id,
					Name = student.Name,
					Age = student.Age,
					Email = student.Email,
					DepartmentId = student.DepartmentId,
					DepartmentName = student.Department.Name,
					SupervisorName = student.Department.Supervisor.Name,
				};
				studentsToReturn.Add(s);
			}
			int TotalCount = students.Count;
			var TotalPages = Math.Ceiling((double)TotalCount / PageSize);

			List<StudentToReturnDto> studentsDto = studentsToReturn.Skip((Page - 1) * PageSize).Take(PageSize).ToList();

			return Ok(studentsDto);


		}
	}
}
