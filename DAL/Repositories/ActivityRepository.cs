using Logic.IRepositories;
using Logic.Models;
using Microsoft.EntityFrameworkCore;
using System;
using DAL.ActivityDbContext;

namespace DAL.Repositories
{
	public class ActivityRepository : IActivityRepository
	{
		private readonly ActivityDbContext.ActivityDbContext _context;

		public ActivityRepository(ActivityDbContext.ActivityDbContext context)
		{
			_context = context;
		}

		public async Task CreateAsync(Activity activity)
		{
			await _context.Activities.AddAsync(activity);
			await _context.SaveChangesAsync();
		}

		public async Task DeleteAsync(Activity activity)
		{
			_context.Activities.Remove(activity);
			await _context.SaveChangesAsync();
		}

		public async Task DeleteAsync(Guid activityId)
		{
			var activity = await _context.Activities.FindAsync(activityId);
			if (activity != null)
			{
				_context.Activities.Remove(activity);
				await _context.SaveChangesAsync();
			}
		}

		public async Task<Activity> GetAsync(Guid Id)
		{
			return await _context.Activities
				.Include(a => a.ActivityContents)
				.FirstOrDefaultAsync(a => a.Id == Id);
		}

		public async Task<List<Activity>> GetAsync(List<Guid> Ids)
		{
			return await _context.Activities
				.Include(a => a.ActivityContents)
				.Where(a => Ids.Contains(a.Id))
				.ToListAsync();
		}

		public async Task UpdateAsync(Activity activity)
		{
			_context.Activities.Update(activity);
			await _context.SaveChangesAsync();
		}
	}
}
