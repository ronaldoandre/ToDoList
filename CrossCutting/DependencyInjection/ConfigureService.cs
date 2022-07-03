using Microsoft.Extensions.DependencyInjection;
using Service.Interfaces.Interfaces;
using Service.Interfaces.Implementations;
using Service.Configurations;
using System.Collections.Generic;

namespace CrossCutting.DependencyInjection
{
    public class ConfigureService
    {
        public static void ConfigureDependenciesService(IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IUserService, UserService>();
            serviceCollection.AddScoped<ITokenService, TokenService>();
            serviceCollection.AddScoped<IToDoService, ToDoService>();
        

            var status = new List<StatusConfigurations>
            {
                new StatusConfigurations(0, "Agendado"),
                new StatusConfigurations(1, "Iniciado"),
                new StatusConfigurations(2, "Concluido")
            };

            var roles = new List<RoleConfigurations>
            {
                new RoleConfigurations(0, "Usuario"),
                new RoleConfigurations(1, "Admin")
            };

            serviceCollection.AddSingleton(status);
            serviceCollection.AddSingleton(roles);
        }
    }
}
