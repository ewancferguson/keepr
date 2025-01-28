namespace keepr.Models
{
  public class Keep
  {
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public string Name { get; set; }
    public string description { get; set; }
    public string Img { get; set; }
    public int Visits { get; set; }

    public string CreatorId { get; set; }
    public Profile Creator { get; set; }

    public int Kept { get; set; }
  }
}