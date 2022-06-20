using Domain.Dtos.Users;
using Domain.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces.Interfaces;
using System;
using System.Net;
using System.Threading.Tasks;
using TODOLIST.Domain.ViewModels;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IUserService _service;
        public UsersController(IUserService service)
        {
            _service = service;
        }
        
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Modelo passado não é válido!");
            }
            try
            {
                return Ok(await _service.Get());

            }
            catch (ArgumentException e)
            {

                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }
        
        [HttpGet]
        [Route("{id}", Name = "GetWithId")]
        public async Task<ActionResult<UserViewModel>> GetById([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                return Ok(await _service.GetById(id));

            }
            catch (ArgumentException e)
            {

                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }
        [HttpPost("register")]
        public async Task<ActionResult> Register([FromBody] UserRegisterDto user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                
                var testEmail = await _service.GetByEmail(user.Email);
                if (testEmail != null) throw new Exception("Email ja cadastrado");
                var result = await _service.Register(user);
                if (result != null)
                {
                    return Ok(result);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (ArgumentException e)
            {

                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }
        
        [HttpPut]
        [Authorize("Bearer")]
        public async Task<ActionResult<UserViewModel>> Update([FromBody] UserViewModel user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var result = await _service.Update(user);
                if (result != null)
                {
                    return Ok(result);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (ArgumentException e)
            {

                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }
        
        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<ActionResult<object>> Login([FromBody] UserLoginDto user)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest("Modelo passado não é válido!");

                var token = await _service.Login(user);

                if (token == null) return Unauthorized("Acesso negado!");
                return Ok(token);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
