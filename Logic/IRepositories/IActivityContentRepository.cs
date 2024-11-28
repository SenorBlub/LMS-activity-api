using Logic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.IRepositories
{
	public interface IActivityContentRepository
	{
		public Task<List<ActivityContent>> GetByContentAsync(Guid contentId);
		public Task<List<ActivityContent>> GetByActivityIdAsync(Guid activityId);
		public Task ConnectAsync(Guid activityId, Guid contentId);
		public Task ConnectAsync(List<Guid> activityIds, Guid contentId);
		public Task ConnectAsync(Guid activityId, List<Guid> contentIds);
		public Task ConnectAsync(List<Guid> activityIds, List<Guid> contentIds);
		public Task DisconnectAsync(Guid activityId, Guid contentId);
		public Task DisconnectByActivityAsync(Guid activityId);
		public Task DisconnectByContentAsync(Guid contentId);

	}
}
