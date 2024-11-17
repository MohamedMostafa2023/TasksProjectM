using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using TasksProjectM.Server.Models;
using TasksProjectM.Server.Services;

namespace TasksProjectM.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpGet]
        [Authorize]
        public async System.Threading.Tasks.Task<ActionResult<IEnumerable<Models.Task>>> GetAllTasks()
        {
            var tasks = await _taskService.GetAllAsync();
            return Ok(tasks);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async System.Threading.Tasks.Task<ActionResult<Models.Task>> GetTaskById(int id)
        {
            var task = await _taskService.GetByIdAsync(id);
            if (task == null)
            {
                return NotFound();
            }
            return Ok(task);
        }

        [HttpPost]
        [Authorize]
        public async System.Threading.Tasks.Task<ActionResult> AddTask([FromBody] Models.Task task)
        {
            await _taskService.AddAsync(task);
            return CreatedAtAction(nameof(GetTaskById), new { id = task.TaskId }, task);
        }

        [HttpPut("{id}")]
        [Authorize]
        public async System.Threading.Tasks.Task<ActionResult> UpdateTask(int id, [FromBody] Models.Task task)
        {
            if (id != task.TaskId)
            {
                return BadRequest("Task ID mismatch");
            }

            await _taskService.UpdateAsync(task);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async System.Threading.Tasks.Task<ActionResult> DeleteTask(int id)
        {
            await _taskService.DeleteAsync(id);
            return NoContent();
        }

        [HttpGet("taskgroup/{taskGroupId}")]
        [Authorize]
        public async System.Threading.Tasks.Task<ActionResult<IEnumerable<Models.Task>>> GetTasksByTaskGroup(int taskGroupId)
        {
            var tasks = await _taskService.GetTasksByTaskGroupAsync(taskGroupId);
            return Ok(tasks);
        }

        [HttpGet("taskList/{taskName}")]
        [Authorize]
        public async System.Threading.Tasks.Task<ActionResult<IEnumerable<Models.Task>>> GetByTaskNameAsync(string taskName)
        {
            var tasks = await _taskService.GetByTaskNameAsync(taskName);
            return Ok(tasks);
        }
    }
}
