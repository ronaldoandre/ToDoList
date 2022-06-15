using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using TODOLIST.Data.Context;

namespace Data.Test
{
    public abstract class BaseTest
    {
        public BaseTest()
        {

        }
    }
    
    public class DbTeste : IDisposable
    {
        private string dataBaseName = $"dbApiTest {Guid.NewGuid().ToString().Replace("-",string.Empty)}";
        public ServiceProvider ServiceProvider { get; private set; }
        public DbTeste()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddDbContext<MyContext>(o =>
            o.UseSqlServer($"Server=localhost;Database={dataBaseName};Trusted_Connection=True;"),
                ServiceLifetime.Transient);
            ServiceProvider = serviceCollection.BuildServiceProvider();
            using (var context = ServiceProvider.GetService<MyContext>())
            {
                context.Database.EnsureCreated();
            }
        }
        public void Dispose()
        {
            using (var context = ServiceProvider.GetService<MyContext>())
            {
                context.Database.EnsureDeleted();
            }
        }
        
    }
}
