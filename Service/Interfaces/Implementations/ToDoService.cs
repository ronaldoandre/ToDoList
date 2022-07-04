using Service.Interfaces.Interfaces;
using TODOLIST.Domain.ViewModels;
using Data.Repository.Interfaces;
using AutoMapper;
using TODOLIST.Domain.Models;
using System;
using Service.Configurations;
using System.Threading.Tasks;
using System.Collections.Generic;
using TODOLIST.Domain.Enum;
using Domain.Dtos.ToDo;

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
        private void ValidarStatus(int status)
        {
            if( !Enum.IsDefined(typeof(StatusTodoEnum), status) )
                throw new Exception("Status não existe!");
        }

        public async Task<ToDoCreateDto> Create(ToDoCreateDto todo,string email)
        {
            ToDo model = _mapper.Map<ToDo>(todo);

            String[] hora = todo.Hora.Split(':');
            String[] data = todo.Data.Split('-');
            var Day = Convert.ToInt32(data[2]);
            var Month = Convert.ToInt32(data[1]);
            var Year = Convert.ToInt32(data[0]);
            var Hour = Convert.ToInt32(hora[0]);
            var Minute = Convert.ToInt32(hora[1]);
            model.DataTarefa = new DateTime(Year,Month,Day,Hour,Minute,00);
            var user = await _userService.GetByEmail(email);
            model.UserId = user.UserId;

            if (!model.isValid)
                throw new Exception("Informações não são válidas!");
            
            ValidarStatus((int)model.Status);

            
            model.DataCriada = DateTime.Now;
            
            ToDo result = await _todoRepository.Create(model);
            ToDoCreateDto todoReturn = _mapper.Map<ToDoCreateDto>(result);
            return todoReturn;
        }

        public async Task<ToDoViewModel> Update(ToDoViewModel todo,string email)
        {
            ToDo model = _mapper.Map<ToDo>(todo);

            if (!model.isValid)
                throw new Exception("Informações não são válidas!");

            ValidarStatus((int)model.Status);

            var user = await _userService.GetByEmail(email);
            model.UserId = user.UserId;

            ToDo result = await _todoRepository.Update(model);
            return _mapper.Map<ToDoViewModel>(result);
        }

        public async Task Delete(int id,string email)
        {
            if (id < 0)
                throw new Exception("Identificador inválido!");

            var user = await _userService.GetByEmail(email);
            var todo = await _todoRepository.GetById(id);
            if(todo.UserId == user.UserId){
                await _todoRepository.Delete(id);
            }else{
                throw new Exception("Usuario inválido para essa tarefa!");
            }
        }

        public async Task<ToDoViewModel> GetById(int todoId)
        {
            ToDo todo = await _todoRepository.GetById(todoId);
            return _mapper.Map<ToDoViewModel>(todo);
        }

        public async Task<IEnumerable<ToDoViewModel>> GetByUser(string email)
        {
            
            var user = await _userService.GetByEmail(email);
            var id = user.UserId;

            IEnumerable<ToDo> todo = await _todoRepository.GetByUserId(id);
            return _mapper.Map<IEnumerable<ToDoViewModel>>(todo);
        }

        public async Task<IEnumerable<ToDoViewModel>> Get(string email)
        {

            await _userService.ValidaUsuarioAdministrador(email);

            IEnumerable<ToDo>  listToDo = await _todoRepository.Get();
            IEnumerable<ToDoViewModel> result = _mapper.Map<IEnumerable<ToDoViewModel>>(listToDo);
            return result;
        }
    }
}