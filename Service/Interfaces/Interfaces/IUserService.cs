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
        Task<UserRegisterDto> Update(UserRegisterDto user,string email);
        Task<UserViewModel> GetById(int id);
        Task<IEnumerable<UserViewModel>> Get(string email);
        Task<TokenViewModel> Login(UserLoginDto user);
        Task<UserViewModel> GetByEmail(string email);
        Task ValidaUsuarioAdministrador(string email);
    }
}
