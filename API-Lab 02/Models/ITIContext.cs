using Microsoft.EntityFrameworkCore;
using API_Lab_02.DTOs;

namespace API_Lab_02.Models
{
	public class ITIContext : DbContext
	{
		public ITIContext(DbContextOptions options) : base(options)
		{
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			modelBuilder.Entity<Department>()
			  .HasOne(d => d.Supervisor)
			  .WithMany()
			  .HasForeignKey(d => d.SupervisorId)
			  .OnDelete(DeleteBehavior.Restrict);
		}
		public DbSet<Student> Students { get; set; }
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<Department> Departments { get; set; }

    }
}
