using Service.Interfaces.Interfaces;
using TODOLIST.Domain.ViewModels;
using Data.Repository.Interfaces;
using AutoMapper;
using TODOLIST.Domain.Models;
using System;
using Service.Configurations;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Service.Interfaces.Implementations
{
    public class ToDoService : IToDoService
    {
        private readonly IToDoRepository _todoRepository;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly ITokenService _token;
        private readonly TokenConfigurations _configuration;

        private const string DATE_FORMAT = "yyy-MM-dd HH:mm:ss";


        public ToDoService(
            IToDoRepository repo,
            IUserService user,
            IMapper mapper,
            ITokenService token,
            TokenConfigurations config


            )
        {
            _todoRepository = repo;
            _userService = user;
            _mapper = mapper;
            _token = token;
            _configuration = config;
        }

        public async Task<ToDoViewModel> Create(ToDoViewModel todo)
        {
            ToDo model = _mapper.Map<ToDo>(todo);
            ToDo result = await _todoRepository.Create(model);
            ToDoViewModel userReturn = _mapper.Map<ToDoViewModel>(result);
            return userReturn;

        }

        public async Task<ToDoViewModel> Update(ToDoViewModel todo)
        {
            var edit = await GetById(todo.ToDoId);
            edit.Titulo = todo.Titulo;
            edit.Descricao = todo.Descricao;
            ToDo model = _mapper.Map<ToDo>(edit);
            ToDo result = await _todoRepository.Update(model);
            return _mapper.Map<ToDoViewModel>(result);
        }

        public async Task Delete(int id)
        {
            if (id < 0)
                throw new Exception("Identificador inválido!");

            await _todoRepository.Delete(id);
        }

        public async Task<ToDoViewModel> GetById(int id)
        {
            ToDo todo = await _todoRepository.GetById(id);
            return _mapper.Map<ToDoViewModel>(todo);
        }

        public async Task<IEnumerable<ToDoViewModel>> GetByUserId(int UserId)
        {
            UserViewModel user = await _userService.GetById(UserId);
            IEnumerable<ToDo> todo = await _todoRepository.GetByUserId(user.UserId);
            return _mapper.Map<IEnumerable<ToDoViewModel>>(todo);
        }

        public async Task<IEnumerable<ToDoViewModel>> Get()
        {
            IEnumerable<ToDo>  listToDo = await _todoRepository.Get();
            IEnumerable<ToDoViewModel> result = _mapper.Map<IEnumerable<ToDoViewModel>>(listToDo);
            return result;
        }
    }
}