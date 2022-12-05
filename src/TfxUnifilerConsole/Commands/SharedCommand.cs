using System.Collections.Generic;
using CommandLine;

namespace Xyz.TForce.Unifiler.Commands
{

  public class SharedCommand : ISharedCommand
  {

    [Option("source", HelpText = "Source directory")]
    public string SourceDir { get; set; }

    [Option("target", HelpText = "Target directory", Required = false)]
    public string TargetDir { get; set; }

    [Value(0)]
    public ICollection<string> Selected { get; set; }

    [Option("debug", Required = false)]
    public bool IsDebug { get; set; }
  }
}
