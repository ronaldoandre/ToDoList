using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using TODOLIST.Data.Context;
using TODOLIST.Domain.Models;

namespace Data.Repository.Implementations
{
    public class ToDoRepository : IToDoRepository
    {
        private readonly MyContext _context;
        public ToDoRepository(MyContext context)
        {
           _context = context;
        }

        public async Task<ToDo> Create(ToDo todo)
        {
            await _context.AddAsync(todo);
            await _context.SaveChangesAsync();
            return todo;
        }

        public async Task Delete(int id)
        {
            var todo = await this.GetById(id);
            _context.Remove(todo);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<ToDo>> Get()
        {
            return await _context.ToDos.ToArrayAsync<ToDo>();
        }

        public async Task<ToDo> GetById(int id)
        {
            return await _context.ToDos
                .AsNoTracking()
                .Include(x => x.User)
                .FirstOrDefaultAsync(x => x.ToDoId.Equals(id));
        }

        public async Task<IEnumerable<ToDo>> GetByUserId(int UserId)
        {
            return await _context.ToDos
                .AsNoTracking()
                .Include(x => x.User)
                .Where(x => x.UserId.Equals(UserId))
                .ToListAsync();
        }

        public async Task<ToDo> Update(ToDo todo)
        {
             var result = await _context.ToDos
                        .FirstOrDefaultAsync(u => u.ToDoId.Equals(todo.ToDoId));

            _context.Entry(result).CurrentValues.SetValues(todo);
            await _context.SaveChangesAsync();
            return todo;
        }
    }
}
