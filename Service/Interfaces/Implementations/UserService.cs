using Service.Interfaces.Interfaces;
using TODOLIST.Domain.ViewModels;
using System.Threading.Tasks;
using System.Collections.Generic;
using Data.Repository.Interfaces;
using AutoMapper;
using TODOLIST.Domain.Models;
using System;
using Domain.ViewModels;
using Service.Configurations;
using System.Text;
using System.Security.Cryptography;
using Domain.Dtos.Users;

namespace Service.Interfaces.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly ITokenService _token;
        private readonly TokenConfigurations _configuration;

        private const string DATE_FORMAT = "yyy-MM-dd HH:mm:ss";


        public UserService(
            IUserRepository repo,
            IMapper mapper,
            ITokenService token,
            TokenConfigurations config

            )
        {
            _userRepository = repo;
            _mapper = mapper;
            _token = token;
            _configuration = config;
        }
        public async Task ValidaUsuarioAdministrador(string email)
        {
            var user = await this.GetByEmail(email) ?? throw new Exception("Nenhum usuário encontrado!");
            if (user.Role != TODOLIST.Domain.Enum.RolePerfilEnum.ADMIN)
                throw new Exception("Você não tem o perfil de ADMINISTRADOR!");
        }

        public async Task<IEnumerable<UserViewModel>> Get(string email)
        {
            await ValidaUsuarioAdministrador(email);
            IEnumerable<User>  listUser = await _userRepository.Get();
            IEnumerable<UserViewModel> result = _mapper.Map<IEnumerable<UserViewModel>>(listUser);
            return result;
        }

        public async Task<UserViewModel> GetById(int id)
        {
            User user = await _userRepository.GetById(id);

            user.Password = "****";
            return _mapper.Map<UserViewModel>(user);
        }

        public async Task<TokenViewModel> Login(UserLoginDto user)
        {

            var baseUser = new User();
            User model = _mapper.Map<User>(user);
            model.Password = _userRepository.EncryptPassword(model, new SHA256CryptoServiceProvider());
            baseUser = await _userRepository.Login(model);
            if (baseUser == null)
            {
                return null;
            }
            else
            {
                var AccessToken = _token.GenerateToken(baseUser);
                DateTime createDate = DateTime.Now;
                DateTime ExpirationDate = createDate.AddMinutes(_configuration.Minutes);

                baseUser.Password = "****";

                var userview = _mapper.Map<UserViewModel>(baseUser);

                return new TokenViewModel(
                    true,
                    createDate.ToString(DATE_FORMAT),
                    ExpirationDate.ToString(DATE_FORMAT),
                    AccessToken,
                    userview
                ); ;
            }
        } 

        public async Task<UserRegisterDto> Register(UserRegisterDto user)
        {
            User model = _mapper.Map<User>(user);

            var modelUserName = await this.GetByEmail(user.Email);
            if (modelUserName != null)
                throw new Exception("Usuário já existente!");

            model.Role = 0;// 0: Usuario Comum
            model.Password = _userRepository.EncryptPassword(model, new SHA256CryptoServiceProvider());
            User result = await _userRepository.Register(model);
            UserRegisterDto userReturn = _mapper.Map<UserRegisterDto>(result);
            userReturn.Password ="****";
            return userReturn;
        }

        public async Task<UserRegisterDto> Update(UserRegisterDto user,string email)
        {
            User model = _mapper.Map<User>(user);
            if(model.Password == null)
            {
                model.Password = GetByEmail(model.Email).Result.Password;
            }
            else
            {
                model.Password = _userRepository.EncryptPassword(model, new SHA256CryptoServiceProvider());
            }
            var userdb = await _userRepository.GetByEmail(email);
            model.Role = userdb.Role;// obriga a role não ser alterada

            User result = await _userRepository.Update(model);
            return _mapper.Map<UserRegisterDto>(result);
        }

        public async Task<UserViewModel> GetByEmail(string email)
        {
            User user = await _userRepository.GetByEmail(email);

            return _mapper.Map<UserViewModel>(user);
        }
    }
}
