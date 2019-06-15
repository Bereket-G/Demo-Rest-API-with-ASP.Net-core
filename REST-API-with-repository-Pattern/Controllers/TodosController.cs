using System;
using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using REST_API_with_repository_Pattern.Models.Entities;
using REST_API_with_repository_Pattern.Repositories;

namespace REST_API_with_repository_Pattern.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public TodoController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        // GET: api/todo/id
        [HttpGet]
        public IActionResult Get()
        {
            var allTodos = _unitOfWork.Todos.GetAll();
            return Ok(allTodos);
        }


        // GET: api/todo/id
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetOne(int id)
        {
            try
            {
                var todo = _unitOfWork.Todos.Get(id);
                if (todo != null) return Ok(todo);
                return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(new {message = ex.Message});
            }
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Register([FromBody] Todo todo)
        {
            try
            {
                _unitOfWork.Todos.Add(todo);
                if (_unitOfWork.Complete() == 1)
                {
                    return Created($"/api/todo/{todo.Id}", todo);
                }
            }
            catch (Exception ex)
            {
                // return error message if there was an exception
                return BadRequest(new {message = ex.Message});
            }

            return BadRequest("Failed to save Todo.");
        }


        // PUT: api/todo/1
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Update(int id, [FromBody] Todo todo)
        {
            try
            {
                var todoToBeUpdated = _unitOfWork.Todos.Get(id);

                if (todoToBeUpdated == null)
                {
                    return NotFound("Requested resource not found");
                }

                todoToBeUpdated.TaskListId = todo.TaskListId;
                todoToBeUpdated.Status = todo.Status;
                todoToBeUpdated.Body = todo.Body;


                _unitOfWork.Todos.Update(todoToBeUpdated);
                if (_unitOfWork.Complete() == 1)
                {
                    return Created($"/api/todo/{todo.Id}", todo);
                }
            }
            catch (Exception ex)
            {
                // return error message if there was an exception
                return BadRequest(new {message = ex.Message});
            }

            return BadRequest("Failed to updated Todo.");
        }


        // DELETE: api/todo/1
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Delete(int id)
        {
            try
            {
                var todoToBeRemoved = _unitOfWork.Todos.Get(id);

                if (todoToBeRemoved == null)
                {
                    return NotFound("Requested resource not found");
                }

                _unitOfWork.Todos.Remove(todoToBeRemoved);
                if (_unitOfWork.Complete() == 1)
                {
                    return Ok("Successfully Deleted");
                }
            }
            catch (Exception ex)
            {
                // return error message if there was an exception
                return BadRequest(new {message = ex.Message});
            }

            return BadRequest("Failed to save Todo.");
        }
    }
}