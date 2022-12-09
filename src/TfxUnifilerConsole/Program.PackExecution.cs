using System;
using System.Collections.Generic;
using System.Linq;
using Xyz.TForce.Unifiler.Application;
using Xyz.TForce.Unifiler.Application.Background;
using Xyz.TForce.Unifiler.Application.Models;
using Xyz.TForce.Unifiler.Commands;

namespace Xyz.TForce.Unifiler
{

  internal partial class Program
  {

    private static readonly IDictionary<Guid, PackCommand> _packCommands = new Dictionary<Guid, PackCommand>();

    private static void ExecutePack(PackCommand command)
    {
      command.SourceDir = TrimTrailingPath(command.SourceDir);
      command.TargetDir = TrimTrailingPath(command.TargetDir);

      Console.WriteLine($"Command: pack");
      Console.WriteLine($"Selected items:");
      foreach (string targetPath in command.Selected)
      {
        Console.WriteLine($"    {targetPath}");
      }
      Console.WriteLine($"Move to archive: {command.MoveToArchive}");
      Console.WriteLine($"Separate inputs: {command.SeparateInput}");
      Console.WriteLine($"Normalize props: {command.NormalizeProperties}");
      Console.WriteLine($"Dictionary size: {command.DictionarySize}");
      Console.WriteLine($"Solid size     : {command.SolidBlockSize}");
      Console.WriteLine($"Split file size: {command.SplitFileSize}");
      Console.WriteLine($"Thread count   : {command.ThreadCount}");
      Console.WriteLine($"Target path: {command.TargetDir}");

      Console.WriteLine("");
      Console.WriteLine("Overrive options");
      Console.WriteLine("    1- MoveToArchive");
      Console.WriteLine("    2- SeparateInput");
      Console.WriteLine("    3- NormalizeProperties");
      Console.Write("Enter: ");

      string overrideCode = Console.ReadLine();
      IList<char> charCodes = overrideCode.ToCharArray().ToList();
      if (charCodes.Contains('1'))
      {
        command.MoveToArchive = !command.MoveToArchive;
      }
      if (charCodes.Contains('2'))
      {
        command.SeparateInput = !command.SeparateInput;
      }
      if (charCodes.Contains('3'))
      {
        command.NormalizeProperties = !command.NormalizeProperties;
      }

      IFilePackBackgroundService filePackService = new FilePackBackgroundService();
      filePackService.FilePackStarted += FilePackService_FilePackStarted;
      filePackService.ProcessFinished += FilePackService_ProcessFinished;
      Guid processId = filePackService.StartAsync(new FilePackArguments
      {
        InputPaths = command.Selected.AsArray(),
        OutputDirectoryPath = command.TargetDir,
        CompressionLevel = command.Compress,
        MoveToArchive = command.MoveToArchive,
        SeparateInput = command.SeparateInput,
        NormalizeProperties = command.NormalizeProperties,
        DictionarySize = command.DictionarySize,
        SolidBlockSize = command.SolidBlockSize,
        SplitFileSize = command.SplitFileSize,
        TheadCount = command.ThreadCount,
        Dryrun = command.IsDebug,
      });
      _packCommands[processId] = command;
    }

    private static void FilePackService_FilePackStarted(Guid processId, string outputPath)
    {
      Console.WriteLine($"Packing {outputPath}...");
    }

    private static void FilePackService_ProcessFinished(Guid processId)
    {
    }
  }
}
