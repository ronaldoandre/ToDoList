using Data.Repository.Implementations;
using Data.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TODOLIST.Data.Context;

namespace CrossCutting.DependencyInjection
{
    public class ConfigureRepository
    {
        public static void ConfigureDependenciesRepository (IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IUserRepository,UserRepository>();

            serviceCollection.AddDbContext<MyContext>(
             options => options.UseSqlServer("Server=localhost;Database=todolistDB;Trusted_Connection=True;")
             );
        }
    }
}
