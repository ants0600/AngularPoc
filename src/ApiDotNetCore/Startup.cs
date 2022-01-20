using Domain.Model;
using DotNetCoreApi.Models;
using DotNetCoreApi.Repositories;
using InfrastructureDotNetCore.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Reflection;
using System.Text.Json;

namespace DotNetCoreApi
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			//todo: JSON serialize to use Camel instead of lower case
			JsonNamingPolicy policy = JsonNamingPolicy.CamelCase;
			policy = null;
			services.AddControllers().AddJsonOptions(options =>
			{
				options.JsonSerializerOptions.DictionaryKeyPolicy = policy;
				options.JsonSerializerOptions.PropertyNamingPolicy = policy;
			});

			DependencyInjector.RegisterTypes(services, this.Configuration);

			services.AddControllers();
			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new OpenApiInfo { Title = "DotNetCoreApi", Version = "v1" });

				var fileName = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
				var filePath = Path.Combine(AppContext.BaseDirectory, fileName);
				c.IncludeXmlComments(filePath);
			});
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{

			}

			app.UseDeveloperExceptionPage();
			app.UseSwagger();
			app.UseSwaggerUI(c =>
			{
				c.SwaggerEndpoint("/swagger/v1/swagger.json", "DotNetCoreApi v1");
				c.RoutePrefix = string.Empty;
			});


			app.UseHttpsRedirection();

			app.UseRouting();

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}
