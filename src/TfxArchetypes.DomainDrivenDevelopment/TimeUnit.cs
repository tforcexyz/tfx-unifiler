using System;

namespace Xyz.TForce.Archetypes.DomainDrivenDevelopment
{

  public class TimeUnit
  {

    private TimeUnit()
    {
    }

    public DateTime Time { get; private set; }

    public long TimeCode { get; private set; }

    public static TimeUnit FromDate(DateTime time)
    {
      return new TimeUnit
      {
        Time = time,
        TimeCode = time.ToSuperEpochUtc()
      };
    }

    public static TimeUnit FromTimeCode(long timeCode)
    {
      return new TimeUnit
      {
        Time = timeCode.AsSuperEpochToDateTimeUtc(),
        TimeCode = timeCode
      };
    }
  }
}
