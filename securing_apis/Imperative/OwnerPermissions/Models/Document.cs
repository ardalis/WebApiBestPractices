namespace OwnerPermissions.Models;

public class Document
{
	public Guid ID { get; set; }
	public string Author { get; set; }
	public string Title { get; set; }
	public string Content { get; set; }
}
