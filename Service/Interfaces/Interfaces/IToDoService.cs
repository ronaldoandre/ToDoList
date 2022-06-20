using Domain.Dtos.Users;
using Domain.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;
using TODOLIST.Domain.ViewModels;

namespace Service.Interfaces.Interfaces
{
    public interface IToDoService
    {
        Task<ToDoViewModel> Create(ToDoViewModel todo);
        Task<ToDoViewModel> Update(ToDoViewModel todo);
        Task Delete(int id);
        Task<ToDoViewModel> GetById(int id);
        Task<IEnumerable<ToDoViewModel>> GetByUserId(int UserId);
        Task<IEnumerable<ToDoViewModel>> Get();
    }
}
