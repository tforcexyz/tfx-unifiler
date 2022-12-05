using System.Collections.Generic;
using CommandLine;

namespace Xyz.TForce.Unifiler.Commands
{

  [Verb("hash")]
  public class HashCommand : SharedCommand
  {

    [Option("algorithm", Required = true, Separator = ',')]
    public IEnumerable<string> Algorithms { get; set; }
  }
}
