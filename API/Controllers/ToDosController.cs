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
    public class ToDosController : ControllerBase
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
                return Ok(await _service.Get());
            }
            catch (ArgumentException e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }
        [HttpGet("User/{UserId}")]
        public async Task<ActionResult<IEnumerable<ToDoViewModel>>> GetByUserId([FromRoute] int UserId)
        {
            if (UserId < 1)
            {
                return BadRequest("Modelo passado não é válido!");
            }
            try
            {
                return Ok(await _service.GetByUserId(UserId));
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
                var result = await _service.Update(todo);
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
                await _service.Delete(TodoId);
                return Ok("Registros deletado com sucesso!");
            }
            catch (ArgumentException e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [HttpPost("create")]
        public async Task<ActionResult> Create([FromBody] ToDoViewModel todo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var result = await _service.Create(todo);
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
