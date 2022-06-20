using Data.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using TODOLIST.Data.Context;
using TODOLIST.Domain.Models;

namespace Data.Repository.Implementations
{
    public class UserRepository : IUserRepository
    {
        private readonly MyContext _context;
        public UserRepository(MyContext context)
        {
           _context = context;
        }

        public async Task<User> Register(User user)
        {
            await _context.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User> GetById(int UserId)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.UserId.Equals(UserId));
        }

        public async Task<IEnumerable<User>> Get()
        {
            return await _context.Users.ToArrayAsync<User>();
        }

        public async Task<User> Update(User user)
        {
            var result = await _context.Users
                        .FirstOrDefaultAsync(u => u.UserId.Equals(user.UserId));

            _context.Entry(result).CurrentValues.SetValues(user);
            await _context.SaveChangesAsync();
            return user;
        }
        public async Task<User> Login(User user)
        {
            var usuario = await _context.Users
                        .FirstOrDefaultAsync(u => (u.Password.Equals(user.Password)
                                                && u.Email.Equals(user.Email)));
            IEnumerable<ToDo> todo = await _context.ToDos.ToArrayAsync<ToDo>();
            IEnumerable<ToDo> dados = todo.Where(p => p.UserId == usuario.UserId).ToList();
            usuario.ToDos = dados;
            return usuario;
        }


        public async Task<User> GetByEmail(string email)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.Email.Equals(email));
        }

        public string EncryptPassword(User user, SHA256CryptoServiceProvider algorithm)
        {
            var valueBytes = System.Convert.FromBase64String(user.Password);
            var pass = Encoding.UTF8.GetString(valueBytes);
            Byte[] inputBytes = Encoding.UTF8.GetBytes(pass);
            Byte[] hashedBytes = algorithm.ComputeHash(inputBytes);
            user.Password = BitConverter.ToString(hashedBytes);
            return user.Password;
        }
    }
}
