using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Security.Claims;
using TaskManagementAPI.Models.Domain;
using TaskManagementAPI.Models.DTO;
using TaskManagementAPI.Repositories.Interface;

namespace TaskManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITaskRepository _taskRepository;
        private readonly ILogger<TaskController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;

        public TaskController(ITaskRepository taskRepository, ILogger<TaskController> logger, UserManager<ApplicationUser> userManager)
        {
            _taskRepository = taskRepository;
            _logger = logger;
            _userManager = userManager;

        }


        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAllTasks()
        {
            try
            {
                return Ok(await _taskRepository.GetAllTask());
            }
            catch (Exception ex)
            {
                _logger.LogInformation("TaskController" + ex.Message);
                throw;
            }
        }


        [Authorize]
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetTaskById(int id)
        {
            try
            {
                var result = await _taskRepository.GetTaskById(id);
                if (result == null)
                {
                    return NotFound();
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogInformation("TaskController" + ex.Message);
                throw;
            }
        }


        [Authorize]
        [HttpPost]
        public async Task<ActionResult> CreateTask(TaskDto model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();


                string userId = User.Claims.First(c => c.Type == "UserId").Value;
                var applicationUser = await _userManager.FindByIdAsync(userId);

                model.CreatedBy = applicationUser?.Id;

                await _taskRepository.InsertTask(model);

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogInformation("TaskController" + ex.Message);
                throw;
            }
        }


        [Authorize]
        [HttpPut("{id:int}")]
        public async Task<ActionResult> UpdateTask(int id, TaskDto model)
        {
            try
            {
                if (id != model.Id)
                    return BadRequest("Task ID mismatch");

                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                var exist = await _taskRepository.GetTaskById(id);
                if (exist == null)
                {
                    return NotFound();
                }


                await _taskRepository.UpdateTask(model);

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogInformation("TaskController" + ex.Message);
                throw;
            }
        }

        [Authorize]
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteTask(int id)
        {
            try
            {
                TaskDto model = await _taskRepository.GetTaskById(id);
                if (model == null)
                {
                    return NotFound();
                }

                await _taskRepository.DeleteTask(model);

      
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogInformation("TaskController" + ex.Message);
                throw;
            }
        }



    }
}
