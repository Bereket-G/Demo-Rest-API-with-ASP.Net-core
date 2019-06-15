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
    public class TaskListController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public TaskListController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        // GET: api/taskLists
        [HttpGet]
        public IActionResult Get()
        {
            var allTaskLists = _unitOfWork.TaskLists.GetAll();
            return Ok(allTaskLists);
        }


        // GET: api/taskLists
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetOne(int id)
        {
            try
            {
                var taskList = _unitOfWork.TaskLists.Get(id);
                if (taskList != null) return Ok(taskList);
                return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(new {message = ex.Message});
            }
        }

        // GET: api/taskLists
        [HttpGet("{id:int}/todo")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetTodoOne(int id)
        {
            try
            {
                var taskList = _unitOfWork.Todos.Find(x => (x.TaskListId == id));
                if (taskList != null) return Ok(taskList);
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
        public IActionResult Register([FromBody] TaskList taskList)
        {
            try
            {
                _unitOfWork.TaskLists.Add(taskList);
                if (_unitOfWork.Complete() == 1)
                {
                    return Created($"/api/taskList/{taskList.Id}", taskList);
                }
            }
            catch (Exception ex)
            {
                // return error message if there was an exception
                return BadRequest(new {message = ex.Message});
            }

            return BadRequest("Failed to save TaskList.");
        }
    }
}