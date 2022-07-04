using Domain.Dtos.ToDo;
using Domain.Dtos.Users;
using Domain.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;
using TODOLIST.Domain.ViewModels;

namespace Service.Interfaces.Interfaces
{
    public interface IToDoService
    {
        Task<ToDoCreateDto> Create(ToDoCreateDto todo,string email);
        Task<ToDoViewModel> Update(ToDoViewModel todo,string email);
        Task Delete(int id,string email);
        Task<ToDoViewModel> GetById(int id);
        Task<IEnumerable<ToDoViewModel>> GetByUser(string email);
        Task<IEnumerable<ToDoViewModel>> Get(string email);
    }
}
