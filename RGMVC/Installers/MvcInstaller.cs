using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace RGMVC.Installers
{
	public class MvcInstaller : IInstaller
	{
		public void InstallServices(IServiceCollection services, IConfiguration Configuration)
		{
			services.AddMvc().AddMvcOptions(option => option.EnableEndpointRouting = false);

			services.AddSwaggerGen(swagger =>
			{
				swagger.SwaggerDoc("v1", new OpenApiInfo { Title = "Retro Games API", Version = "v1" });
			});
		}
	}
}
