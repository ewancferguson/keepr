namespace keepr.Models;

public class Vault
{
  public int Id { get; set; }
  public DateTime CreatedAt { get; set; }
  public DateTime UpdatedAt { get; set; }
  public string Name { get; set; }
  public string description { get; set; }
  public string Img { get; set; }
  public bool IsPrivate { get; set; }
  public string CreatorId { get; set; }
  public Profile Creator { get; set; }
}


