using System;

namespace Xyz.TForce.InOut.FileSystem
{

  public class SetPropertiesOptions
  {

    public bool? Archive { get; set; }

    public bool? Hidden { get; set; }

    public bool? ReadOnly { get; set; }

    public bool? System { get; set; }

    public DateTime? CreatedTime { get; set; }

    public DateTime? AccessedTime { get; set; }

    public DateTime? ModifiedTime { get; set; }
  }
}
