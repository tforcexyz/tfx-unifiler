using System.Collections.Generic;

namespace Xyz.TForce.Unifiler.Commands
{

  public interface ISharedCommand
  {

    string SourceDir { get; set; }

    string TargetDir { get; set; }

    ICollection<string> Selected { get; set; }
  }
}
