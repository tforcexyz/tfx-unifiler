using System;

namespace Xyz.TForce.Archetypes.DomainDrivenDevelopment
{

  public class DateUnit
  {

    private DateUnit()
    {
    }

    public DateTime Date { get; private set; }

    public long DateCode { get; private set; }

    public static DateUnit FromDate(DateTime date)
    {
      return new DateUnit
      {
        Date = date,
        DateCode = date.ToLongEpoch()
      };
    }

    public static DateUnit FromDateCode(long dateCode)
    {
      return new DateUnit
      {
        Date = dateCode.AsLongEpochToDateTime(),
        DateCode = dateCode
      };
    }
  }
}
