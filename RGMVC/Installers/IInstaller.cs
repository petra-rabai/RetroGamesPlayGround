using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace RGMVC.Installers
{
	public interface IInstaller
	{
		void InstallServices(IServiceCollection services ,IConfiguration Configuration);
	}
}
