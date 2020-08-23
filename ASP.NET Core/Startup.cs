using FluentValidation;
using FluentValidation.AspNetCore;
using Furmanov.Controllers;
using Furmanov.Data.Data;
using Furmanov.Models;
using Furmanov.MVP.MainView;
using Furmanov.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Diagnostics;

namespace Furmanov
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		public void ConfigureServices(IServiceCollection services)
		{
			//services.AddSingleton<ExceptionService>();

			services.AddMvc().AddFluentValidation();
			services.AddTransient<IValidator<SalaryPay>, SalaryPayValidator>();

			var connection = Configuration.GetConnectionString("DefaultConnection");
			services.AddDbContext<UserContext>(options => options.UseSqlServer(connection));

			services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
				.AddCookie(options =>
				{
					options.LoginPath = new PathString("/Account/Login");
				});
			services.AddControllersWithViews();
		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			app.Use(async (context, next) =>
			{
				var requestId = Activity.Current?.Id ?? context.TraceIdentifier;
				if (!context.Request.Cookies.ContainsKey(ExceptionService.RequestKey))
				{
					context.Response.Cookies.Append(ExceptionService.RequestKey, requestId);
				}
				await next();
			});

#warning env.EnvironmentName = "Production";
			env.EnvironmentName = "Production";

			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseExceptionHandler($"/{ExceptionController.Name}/{nameof(ExceptionController.ReportBug)}");
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseRouting();

			app.UseAuthentication();
			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllerRoute(
					name: "default",
					pattern: "{controller=Home}/{action=Index}/{id?}");
			});
		}
	}
}
