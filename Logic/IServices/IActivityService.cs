using Logic.Models;

namespace Logic.IServices;

public interface IActivityService
{
	public Task<Activity> GetAsync(Guid Id);
	public Task<List<Activity>> GetAsync(List<Guid> Ids);
	public Task<List<Activity>> GetByContentAsync(Guid contentId);
	public Task CreateAsync(Activity activity);
	public Task DeleteAsync(Activity activity);
	public Task DeleteAsync(Guid activityId);
	public Task UpdateAsync(Activity activity);
}