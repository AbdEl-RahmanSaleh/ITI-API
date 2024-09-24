
using API_Lab_02.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace API_Lab_02
{
	public class Program
	{
		public static void Main(string[] args)
		{
			string txt = "";
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.

			builder.Services.AddControllers();
			builder.Services.AddDbContext<ITIContext>(options =>
			{
				options.UseLazyLoadingProxies().UseSqlServer(builder.Configuration.GetConnectionString("iticonn"));
			});
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen(o => {
				o.SwaggerDoc("v1", new OpenApiInfo()
				{
					Title = ".NET Api",
					Description = "API To manage ITI Data",
					Version = "v1",
					TermsOfService = new Uri("http://tempuri.org/terms"),
					Contact = new OpenApiContact
					{
						Name = "ASaleh",
						Email = "Asaleh@gmail.com"
					},

				});

				o.IncludeXmlComments("E:\\ITI - ITP .NET\\1- BackEnd\\9- ASP.NET API\\Day2\\Lab\\API-Lab 02\\API-Lab 02\\ApiD2.xml");
				o.EnableAnnotations();
			}

				);

			builder.Services.AddCors(op =>
			{
				op.AddPolicy(txt, builder =>
				{
					builder.AllowAnyOrigin();
					builder.AllowAnyMethod();
					builder.AllowAnyHeader();
				});
			});


			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseHttpsRedirection();

			app.UseAuthorization();

			app.UseCors(txt);

			app.MapControllers();

			app.Run();
		}
	}
}
