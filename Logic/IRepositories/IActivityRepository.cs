using Logic.Models;

namespace Logic.IRepositories;

public interface IActivityRepository
{
	public Task<Activity> GetAsync(Guid Id);
	public Task<List<Activity>> GetAsync(List<Guid> Ids);
	public Task CreateAsync(Activity activity);
	public Task UpdateAsync(Activity activity);
	public Task DeleteAsync(Activity activity);
	public Task DeleteAsync(Guid activityId);
}