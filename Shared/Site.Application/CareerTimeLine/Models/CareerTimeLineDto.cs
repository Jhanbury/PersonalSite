using System;

namespace Site.Application.CareerTimeLine.Models
{
  public class CareerTimeLineDto
  {
    public string Title { get; set; }
    public string SubTitle { get; set; }
    public string SmallText { get; set; }
    public string TimeLineType { get; set; }
    public DateTime Date { get; set; }
  }
}

public enum TimeLineType
{
  Job,
  Degree,
  Certification
}
