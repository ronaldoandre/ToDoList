using System.Collections.Generic;
using System.Security.Cryptography;
using System.Threading.Tasks;
using TODOLIST.Domain.Models;

namespace Data.Repository.Interfaces
{
    public interface IUserRepository
    {
        Task<User> Register(User user);
        Task<User> Update(User user);
        Task<User> GetById(int id);
        Task<IEnumerable<User>> Get();
        Task<User> Login(User user);
        Task<User> GetByEmail(string email);
        string EncryptPassword(User user, SHA256CryptoServiceProvider algorithm);
    }
}
