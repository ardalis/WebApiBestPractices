namespace BackendData;

public class Author : BaseEntity
{
  public string Name { get; set; } = "New Author";
  public string? PluralsightUrl { get; set; }
  public string? TwitterAlias { get; set; }
}
