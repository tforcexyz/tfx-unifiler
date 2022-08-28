using System.Collections.Generic;
using CommandLine;

namespace Xyz.TForce.Unifiler
{

  public class CommandLineArgs
  {

    [Option("source", HelpText = "Source directory")]
    public string SourceDir { get; set; }

    [Option("target", HelpText = "Target directory", Required = false)]
    public string TargetDir { get; set; }

    [Option("module", HelpText = "Which tool you want to use which selected files", Required = false, Default = 0)]
    public int Module { get; set; }

    [Value(0)]
    public ICollection<string> Selected { get; set; }
  }
}
