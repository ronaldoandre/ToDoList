using Domain.Dtos.Users;
using Domain.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;
using TODOLIST.Domain.ViewModels;

namespace Service.Interfaces.Interfaces
{
    public interface IUserService
    {
        Task<UserRegisterDto> Register(UserRegisterDto user);
        Task<UserViewModel> Update(UserViewModel user);
        Task<UserViewModel> GetById(int id);
        Task<IEnumerable<UserViewModel>> Get();
        Task<TokenViewModel> Login(UserLoginDto user);
        Task<UserViewModel> GetByEmail(string email);
    }
}
