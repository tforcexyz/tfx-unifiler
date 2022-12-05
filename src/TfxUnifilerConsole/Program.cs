using System;
using System.Collections.Generic;
using CommandLine;
using Xyz.TForce.Unifiler.Commands;

namespace Xyz.TForce.Unifiler
{

  internal partial class Program
  {

    static void Main(string[] args)
    {
      Console.WriteLine($"TFX UNIFILER v1.0.0.20221224");
      Console.WriteLine("");

      _ = Parser.Default.ParseArguments<HashCommand, PackCommand>(args)
        .WithParsed<HashCommand>(ExecuteHash)
        .WithParsed<PackCommand>(ExecutePack)
        .WithNotParsed(Exit);
    }

    private static void ExecutePack(PackCommand args)
    {
    }

    private static void Exit(IEnumerable<Error> errors)
    {
    }
  }
}
