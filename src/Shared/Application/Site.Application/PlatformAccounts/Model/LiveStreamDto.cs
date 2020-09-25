namespace Site.Application.Videos.Models
{
  public class LiveStreamDto
  {
    public string Url { get; set; }
    public string Platform { get; set; }
    public string Streamer { get; set; }
    public bool IsLive { get; set; }
  }
}
