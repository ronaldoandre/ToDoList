using Domain.Dtos.ToDo;
using Domain.Dtos.Users;
using Domain.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces.Interfaces;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using TODOLIST.Domain.ViewModels;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize("Bearer")]
    public class ToDosController : BaseController
    {
        private IToDoService _service;
        public ToDosController(IToDoService service)
        {
            _service = service;
        }

        [HttpGet("Admin/")]
        public async Task<ActionResult> Get()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Modelo passado não é válido!");
            }
            try
            {
                return Ok(await _service.Get(Email));
            }
            catch (ArgumentException e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }
        [HttpGet("User/")]
        public async Task<ActionResult<IEnumerable<ToDoViewModel>>> GetByUser()
        {
            try
            {
                return Ok(await _service.GetByUser(Email));
            }
            catch (ArgumentException e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [HttpGet("{TodoId}")]
        public async Task<ActionResult<ToDoViewModel>> GetById(int TodoId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                return Ok(await _service.GetById(TodoId));
            }
            catch (ArgumentException e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }
        
        [HttpPut]
        public async Task<ActionResult<ToDoViewModel>> Update([FromBody] ToDoViewModel todo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var result = await _service.Update(todo,Email);
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

        [HttpDelete("Delete/{TodoId}")]
        public async Task<ActionResult> Delete([FromRoute] int TodoId)
        {
            try
            {
                await _service.Delete(TodoId,Email);
                return Ok("Registros deletado com sucesso!");
            }
            catch (ArgumentException e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [HttpPost("create")]
        public async Task<ActionResult> Create([FromBody] ToDoCreateDto todo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var result = await _service.Create(todo,Email);
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
    }
}
