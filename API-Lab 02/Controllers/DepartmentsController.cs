using API_Lab_02.DTOs.DepartmentDtos;
using API_Lab_02.DTOs.StudentDto;
using API_Lab_02.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace API_Lab_02.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class DepartmentsController : ControllerBase
	{
		private readonly ITIContext _db;

		public DepartmentsController(ITIContext db)
        {
			_db = db;
		}



		[HttpGet]
		[SwaggerOperation(
			Summary = "Method To Return All Department Data",
			Description = "Return All Departments Exist in ITI DataBase",
			OperationId = "GetAll"	
			)]
		[SwaggerResponse(200, "All Departments", typeof(List<DepartmentToReturnDto>))]
		[SwaggerResponse(404, "No Department Exists")]
		public IActionResult GetAllDepartments()
		{
			List<Department> departments = _db.Departments.ToList();
			List<DepartmentToReturnDto> departmentsToReturn = new List<DepartmentToReturnDto>();

			foreach (Department department in departments)
			{
				DepartmentToReturnDto d = new DepartmentToReturnDto()
				{
					Id = department.Id,
					Name = department.Name,
					Capacity = department.Capacity,
					SupervisorName = department.Supervisor.Name,
					StudentNumber = department.Students.Count(),
				};
				departmentsToReturn.Add(d);
			}

			return Ok(departmentsToReturn);
		}

		[HttpGet("{id}")]
		[SwaggerOperation(
			Summary = "Method To Return Department By Id",
			Description = "Return Department Data By Id if Exists",
			OperationId = "GetById"
			)]
		[SwaggerResponse(200, "Department Data", typeof(DepartmentToReturnDto))]
		[SwaggerResponse(404, "Department Id Doesn't Exist")]
		public IActionResult GetDepartmentById(int id)
		{

			Department department = _db.Departments.SingleOrDefault(d => d.Id == id);
			if (department == null)
				return NotFound();
			else
			{
				DepartmentToReturnDto d = new DepartmentToReturnDto()
				{
					Id= department.Id,
					Name = department.Name,
					Capacity = department.Capacity,
					SupervisorName= department.Supervisor.Name,
					StudentNumber = department.Students.Count(),
				};
				return Ok(d);
			}
		}

		[HttpPost]
		[SwaggerOperation(
			Summary = "Method To Add New Department",
			Description = "Return The New Department",
			OperationId = "Add"
			)]
		[SwaggerResponse(200, "New Department", typeof(DepartmentToReturnDto))]
		[SwaggerResponse(400, "SomeThing Went Wrong")]
		public IActionResult AddDepartment(DepartmentDto departmentDto)
		{
			if (departmentDto == null)
				return BadRequest();
			if (!ModelState.IsValid)
				return BadRequest();

			Department department = new Department()
			{
				Name = departmentDto.Name,
				Capacity= departmentDto.Capacity,
				SupervisorId= departmentDto.SupervisorId,
			};

			_db.Departments.Add(department);
			_db.SaveChanges();

			return RedirectToAction("GetDepartmentById", new {id = department.Id});

		}

		[HttpPut]
		[SwaggerOperation(
			Summary = "Method To Edit a Department",
			Description = "Return Nothing",
			OperationId = "Edit"
			)]
		[SwaggerResponse(200, "Department Edited")]
		[SwaggerResponse(400, "SomeThing Went Wrong")]
		public IActionResult EditDepartment(int id, DepartmentDto departmentDto)
		{
			Department department = _db.Departments.SingleOrDefault(x => x.Id == id);
			if (department == null)
				return NotFound();

			if (!ModelState.IsValid)
				return BadRequest();
			department.Name = departmentDto.Name;
			department.Capacity = departmentDto.Capacity;
			department.SupervisorId = departmentDto.SupervisorId;

			_db.Departments.Update(department);
			_db.SaveChanges();

			return NoContent();

		}

		[HttpDelete]
		[SwaggerOperation(
			Summary = "Method To Delete Department",
			Description = "Return All Remaining Departments",
			OperationId = "Delete"
			)]
		[SwaggerResponse(200, "Delete Department", typeof(List<DepartmentToReturnDto>))]
		[SwaggerResponse(400, "SomeThing Went Wrong")]
		public IActionResult DeleteDepartment(int id)
		{
			Department d = _db.Departments.Find(id);
			if (d == null)
				return NotFound();
			_db.Departments.Remove(d);
			_db.SaveChanges();

			return RedirectToAction("GetAllDepartments");
		}

	}
}
