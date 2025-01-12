using Logic.IRepositories;
using Logic.IServices;
using Logic.Models;

namespace Logic.Services;

public class ActivityService : IActivityService
{
	private readonly IActivityRepository _activityRepository;
	private readonly IActivityContentRepository _activityContentRepository;
	public ActivityService(IActivityContentRepository activityContentRepository)
	{
		_activityContentRepository = activityContentRepository;
	}
	public async Task CreateAsync(Activity activity)
	{
		if(activity != null)
			await _activityRepository.CreateAsync(activity);
		if (activity.ActivityContents != null && activity.ActivityContents.Count > 0)
		{
			foreach (ActivityContent activityContent in activity.ActivityContents)
			{
				await _activityContentRepository.ConnectAsync(activityContent.ActivityId, activityContent.ContentId);
			}
		}
	}

	public async Task DeleteAsync(Activity activity)
	{
		await _activityRepository.DeleteAsync(activity);
	}

	public async Task DeleteAsync(Guid activityId)
	{
		await _activityRepository.DeleteAsync(activityId);
	}

	public async Task<Activity> GetAsync(Guid Id)
	{
		return await _activityRepository.GetAsync(Id);
	}

	public async Task<List<Activity>> GetAsync(List<Guid> Ids)
	{
		return await _activityRepository.GetAsync(Ids);
	}

	public async Task<List<Activity>> GetByContentAsync(Guid contentId)
	{
		List<ActivityContent> contentRelatedActivityContents = await _activityContentRepository.GetByContentAsync(contentId);
		List<Guid> activityGuids = new List<Guid>();
		foreach(var activityContent in contentRelatedActivityContents) 
		{
			activityGuids.Add(activityContent.ActivityId);
		}
		return await _activityRepository.GetAsync(activityGuids);
		
	}

	public async Task UpdateAsync(Activity activity)
	{
		await _activityRepository.UpdateAsync(activity);
	}
}