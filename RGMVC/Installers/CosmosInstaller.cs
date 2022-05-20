using Cosmonaut;
using Cosmonaut.Extensions.Microsoft.DependencyInjection;
using Microsoft.Azure.Documents.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RGMVC.Domain;

namespace RGMVC.Installers
{
	public class CosmosInstaller : IInstaller
	{
		public void InstallServices(IServiceCollection services, IConfiguration configuration)
		{
			//CosmosStoreSettings cosmosStoreSettings = new CosmosStoreSettings(
			//	configuration["CosmosSettings: DatabaseName"],
			//	configuration["CosmosSettings: AccountUri"],
			//	configuration["CosmosSettings: AccountKey"],
			//	new ConnectionPolicy{ConnectionMode = ConnectionMode.Direct, ConnectionProtocol = Protocol.Tcp});

			//services.AddCosmosStore<CosmosPostDto>(cosmosStoreSettings);

		}
	}
}
