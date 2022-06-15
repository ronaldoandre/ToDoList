using Microsoft.Extensions.DependencyInjection;
using Service.Interfaces.Interfaces;
using Service.Interfaces.Implementations;

namespace CrossCutting.DependencyInjection
{
    public class ConfigureService
    {
        public static void ConfigureDependenciesService(IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IUserService, UserService>();
            serviceCollection.AddScoped<ITokenService, TokenService>();
        }
    }
}
