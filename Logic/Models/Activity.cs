namespace Logic.Models;

public class Activity
{
	public Guid Id { get; set; }
	public string Title { get; set; }
	public bool IsPrivate { get; set; }
	public ICollection<ActivityContent> ActivityContents { get; set; } = new List<ActivityContent>();
}