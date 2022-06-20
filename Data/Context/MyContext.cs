using Data.Mapping;
using Microsoft.EntityFrameworkCore;
using TODOLIST.Domain.Models;

namespace TODOLIST.Data.Context
{
    public class MyContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<ToDo> ToDos { get; set; }

        public MyContext(DbContextOptions<MyContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>(new UserMap().Configure);
            modelBuilder.Entity<ToDo>(new ToDoMap().Configure);
        }
    }
}