using System;
using System.Collections.Generic;
using System.IO;
using CommandLine;
using Xyz.TForce.Unifiler.Commands;

namespace Xyz.TForce.Unifiler
{

  internal partial class Program
  {

    static void Main(string[] args)
    {
      Console.WriteLine($"TFX UNIFILER v1.0.0.20221231");
      Console.WriteLine("");

      _ = Parser.Default.ParseArguments<HashCommand, PackCommand>(args)
        .WithParsed<HashCommand>(ExecuteHash)
        .WithParsed<PackCommand>(ExecutePack)
        .WithNotParsed(Exit);
    }

    private static void Exit(IEnumerable<Error> errors)
    {
    }

    private static string TrimTrailingPath(string path)
    {
      if (path == null)
      {
        return null;
      }
      return path.TrimEnd(Path.DirectorySeparatorChar);
    }
  }
}
