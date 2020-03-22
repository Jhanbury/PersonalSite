using System;

namespace Site.Domain.Entities.Audit
{
  public class PerformanceLog
  {
    public int Id { get; set; }
    public string RequestName { get; set; }
    public string RequestData { get; set; }
    public string ClientIPAddress { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public TimeSpan Duration { get; set; }
  }
}
