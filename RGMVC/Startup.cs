using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RGMVC.Data;
using RGMVC.Options;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RGMVC.Installers;

namespace RGMVC
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
			services.InstallServicesInAssembly(Configuration);
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseHsts();
			}

			SwaggerOptions swaggerOptions = new();
			Configuration.GetSection(nameof(SwaggerOptions)).Bind(swaggerOptions);
			app.UseSwagger(option =>
			{
				option.RouteTemplate = swaggerOptions.JsonRoute;
			});

			app.UseSwaggerUI(option =>
			{
				option.SwaggerEndpoint(swaggerOptions.UiEndpoint,swaggerOptions.Description);
			});

			app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseMvc();

		}
	}
}
