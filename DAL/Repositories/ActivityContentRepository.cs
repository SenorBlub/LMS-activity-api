using Logic.IRepositories;
using Logic.Models;
using Microsoft.EntityFrameworkCore;
using DAL.ActivityDbContext;

namespace DAL.Repositories
{
	public class ActivityContentRepository : IActivityContentRepository
	{
		private readonly ActivityDbContext.ActivityDbContext _context;

		public ActivityContentRepository(ActivityDbContext.ActivityDbContext context)
		{
			_context = context;
		}

		public async Task ConnectAsync(Guid activityId, Guid contentId)
		{
			var activityContent = new ActivityContent
			{
				Id = Guid.NewGuid(),
				ActivityId = activityId,
				ContentId = contentId
			};

			await _context.ActivityContents.AddAsync(activityContent);
			await _context.SaveChangesAsync();
		}

		public async Task ConnectAsync(List<Guid> activityIds, Guid contentId)
		{
			var activityContents = activityIds.Select(activityId => new ActivityContent
			{
				Id = Guid.NewGuid(),
				ActivityId = activityId,
				ContentId = contentId
			}).ToList();

			await _context.ActivityContents.AddRangeAsync(activityContents);
			await _context.SaveChangesAsync();
		}

		public async Task ConnectAsync(Guid activityId, List<Guid> contentIds)
		{
			var activityContents = contentIds.Select(contentId => new ActivityContent
			{
				Id = Guid.NewGuid(),
				ActivityId = activityId,
				ContentId = contentId
			}).ToList();

			await _context.ActivityContents.AddRangeAsync(activityContents);
			await _context.SaveChangesAsync();
		}

		public async Task ConnectAsync(List<Guid> activityIds, List<Guid> contentIds)
		{
			var activityContents = activityIds.SelectMany(activityId =>
				contentIds.Select(contentId => new ActivityContent
				{
					Id = Guid.NewGuid(),
					ActivityId = activityId,
					ContentId = contentId
				})
			).ToList();

			await _context.ActivityContents.AddRangeAsync(activityContents);
			await _context.SaveChangesAsync();
		}

		public async Task DisconnectAsync(Guid activityId, Guid contentId)
		{
			var activityContent = await _context.ActivityContents
				.FirstOrDefaultAsync(ac => ac.ActivityId == activityId && ac.ContentId == contentId);

			if (activityContent != null)
			{
				_context.ActivityContents.Remove(activityContent);
				await _context.SaveChangesAsync();
			}
		}

		public async Task DisconnectByActivityAsync(Guid activityId)
		{
			var activityContents = _context.ActivityContents.Where(ac => ac.ActivityId == activityId);
			_context.ActivityContents.RemoveRange(activityContents);
			await _context.SaveChangesAsync();
		}

		public async Task DisconnectByContentAsync(Guid contentId)
		{
			var activityContents = _context.ActivityContents.Where(ac => ac.ContentId == contentId);
			_context.ActivityContents.RemoveRange(activityContents);
			await _context.SaveChangesAsync();
		}

		public async Task<List<ActivityContent>> GetByActivityIdAsync(Guid activityId)
		{
			return await _context.ActivityContents
				.Where(ac => ac.ActivityId == activityId)
				.ToListAsync();
		}

		public async Task<List<ActivityContent>> GetByContentAsync(Guid contentId)
		{
			return await _context.ActivityContents
				.Where(ac => ac.ContentId == contentId)
				.ToListAsync();
		}
	}
}
