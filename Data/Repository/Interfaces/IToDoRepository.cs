using System.Collections.Generic;
using System.Security.Cryptography;
using System.Threading.Tasks;
using TODOLIST.Domain.Models;

namespace Data.Repository.Interfaces
{
    public interface IToDoRepository
    {
        Task<ToDo> Create(ToDo todo);
        Task<ToDo> Update(ToDo todo);
        Task Delete(int id);
        Task<ToDo> GetById(int id);
        Task<IEnumerable<ToDo>> GetByUserId(int UserId);
        Task<IEnumerable<ToDo>> Get();
    }
}