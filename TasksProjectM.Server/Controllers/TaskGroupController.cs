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
    public class TaskGroupController : ControllerBase
    {
        private readonly ITaskGroupService _taskGroupService;

        public TaskGroupController(ITaskGroupService taskGroupService)
        {
            _taskGroupService = taskGroupService;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<TaskGroup>>> GetAllTaskGroups()
        {
            var taskGroups = await _taskGroupService.GetAllAsync();
            return Ok(taskGroups);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<TaskGroup>> GetTaskGroupById(int id)
        {
            var taskGroup = await _taskGroupService.GetByIdAsync(id);
            if (taskGroup == null)
            {
                return NotFound();
            }
            return Ok(taskGroup);
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> AddTaskGroup([FromBody] TaskGroup taskGroup)
        {
            await _taskGroupService.AddAsync(taskGroup);
            return CreatedAtAction(nameof(GetTaskGroupById), new { id = taskGroup.TaskGroupId }, taskGroup);
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult> UpdateTaskGroup(int id, [FromBody] TaskGroup taskGroup)
        {
            if (id != taskGroup.TaskGroupId)
            {
                return BadRequest("TaskGroup ID mismatch");
            }

            await _taskGroupService.UpdateAsync(taskGroup);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult> DeleteTaskGroup(int id)
        {
            await _taskGroupService.DeleteAsync(id);
            return NoContent();
        }
    }
}
