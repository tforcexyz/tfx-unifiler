using System;

namespace Xyz.TForce.Unifiler.Interop.ExternalExecutables
{

  public class ProcessFinishedEventArgs
  {

    public Guid ProcessId { get; set; }

    public bool IsSuccess { get; set; }

    public Exception Error { get; set; }
  }
}
