using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RGMVC.Installers
{
	public static class InstallerExtensions
	{
		public static void InstallServicesInAssembly(this IServiceCollection services, IConfiguration configuration)
		{
			List<IInstaller> installers = typeof(Startup).Assembly.ExportedTypes.Where(installer => typeof(IInstaller).IsAssignableFrom(installer) && !installer.IsInterface && !installer.IsAbstract).Select(Activator.CreateInstance).Cast<IInstaller>().ToList();

			installers.ForEach(installer => installer.InstallServices(services, configuration));
		}
	}
}
