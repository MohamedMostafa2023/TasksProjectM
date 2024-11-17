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
    public class TaskStatusController : ControllerBase
    {
        private readonly ITaskStatusService _taskStatusService;

        public TaskStatusController(ITaskStatusService taskStatusService)
        {
            _taskStatusService = taskStatusService;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Models.TaskStatus>>> GetAllTaskStatuses()
        {
            var taskStatuses = await _taskStatusService.GetAllAsync();
            return Ok(taskStatuses);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<Models.TaskStatus>> GetTaskStatusById(int id)
        {
            var taskStatus = await _taskStatusService.GetByIdAsync(id);
            if (taskStatus == null)
            {
                return NotFound();
            }
            return Ok(taskStatus);
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> AddTaskStatus([FromBody] Models.TaskStatus taskStatus)
        {
            await _taskStatusService.AddAsync(taskStatus);
            return CreatedAtAction(nameof(GetTaskStatusById), new { id = taskStatus.TaskStatusId }, taskStatus);
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult> UpdateTaskStatus(int id, [FromBody] Models.TaskStatus taskStatus)
        {
            if (id != taskStatus.TaskStatusId)
            {
                return BadRequest("TaskStatus ID mismatch");
            }

            await _taskStatusService.UpdateAsync(taskStatus);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult> DeleteTaskStatus(int id)
        {
            await _taskStatusService.DeleteAsync(id);
            return NoContent();
        }
    }
}
