namespace OwnerPermissions.Models;

public class Document
{
	public int Id { get; set; }
	public string Author { get; set; } = "";
	public string Title { get; set; } = "";
	public string Content { get; set; } = "";
}
