using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace TODOLIST.Data.Context
{
    public class ContextFactory : IDesignTimeDbContextFactory<MyContext>
    {
        public MyContext CreateDbContext(string[] args){
            var connectionString = "Server=localhost;Database=todolistDB;Trusted_Connection=True;";
            var optionsBuilder = new DbContextOptionsBuilder<MyContext>();
            optionsBuilder.UseSqlServer (connectionString);
            return new MyContext(optionsBuilder.Options);
        }
    }
}