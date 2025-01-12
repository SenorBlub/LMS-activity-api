using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Logic.IServices;
using Logic.Models;

namespace LMS_activity_api.Controllers
{
	[Route("activity/[controller]")]
	[ApiController]
	public class ActivityController : ControllerBase
	{
		private readonly IActivityService _activityService;

		public ActivityController(IActivityService activityService)
		{
			_activityService = activityService;
		}

		[HttpPost("create")]
		public async Task<IActionResult> Create([FromBody] Activity activity)
		{
			activity.Id = Guid.NewGuid();
			await _activityService.CreateAsync(activity);
			return Ok(activity);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> Get(Guid id)
		{
			var activity = await _activityService.GetAsync(id);
			if (activity == null)
				return NotFound();
			return Ok(activity);
		}

		[HttpGet("batch")]
		public async Task<IActionResult> GetBatch([FromQuery] List<Guid> ids)
		{
			var activities = await _activityService.GetAsync(ids);
			return Ok(activities);
		}

		[HttpGet("content/{contentId}")]
		public async Task<IActionResult> GetByContent(Guid contentId)
		{
			var activities = await _activityService.GetByContentAsync(contentId);
			return Ok(activities);
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> Update(Guid id, [FromBody] Activity activity)
		{
			if (id != activity.Id)
				return BadRequest();

			await _activityService.UpdateAsync(activity);
			return NoContent();
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(Guid id)
		{
			await _activityService.DeleteAsync(id);
			return NoContent();
		}

		[HttpDelete]
		public async Task<IActionResult> Delete([FromBody] Activity activity)
		{
			await _activityService.DeleteAsync(activity);
			return NoContent();
		}
	}
}
