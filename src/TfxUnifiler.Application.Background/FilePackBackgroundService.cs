using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using Xyz.TForce.InOut.FileSystem;
using Xyz.TForce.Unifiler.Application.Models;
using Xyz.TForce.Unifiler.Interop.ExternalExecutables;
using Xyz.TForce.Unifiler.Interop.ExternalExecutables.Types.SevenZip;
using static Xyz.TForce.Unifiler.Application.IFilePackBackgroundService;

namespace Xyz.TForce.Unifiler.Application.Background
{

  public class FilePackBackgroundService : IFilePackBackgroundService
  {

    public event ProcessStart ProcessStarted;
    public event FilePackStart FilePackStarted;
    public event FilePackFinish FilePackFinished;
    public event ProcessFinish ProcessFinished;

    public Guid StartAsync(FilePackArguments args)
    {
      Thread thread = new Thread(Process);
      Guid processId = Guid.NewGuid();

      thread.Start(new FilePackProcessArgs
      {
        ProcessId = processId,
        Arguments = args,
      });

      return processId;
    }

    private void Process(object args)
    {
      FilePackProcessArgs processArgs = (FilePackProcessArgs)args;
      Guid processId = processArgs.ProcessId;
      FilePackArguments filePackArgs = processArgs.Arguments;
      OnProcessStarted(processId);

      ExecutionController controller = new ExecutionController();
      CommandInfo[] commandInfos = Process_BuildCommandInfos(processArgs);
      if (filePackArgs.SeparateInput)
      {
        int i = 0;
        foreach (string path in filePackArgs.InputPaths)
        {
          string targetPath = filePackArgs.OutputDirectoryPath;
          string targetName = Path.GetFileName(path);
          string outputPath = Path.Combine(targetPath, $"{targetName}.7z");
          CommandInfo commandInfo = commandInfos[i];
          OnFilePackStarted(processId, outputPath);
          if (filePackArgs.NormalizeProperties && !filePackArgs.Dryrun)
          {
            string[] paths = GetDirectoriesAndFilePaths(path);
            Process_ChangeAttributes(paths);
          }
          if (!filePackArgs.Dryrun)
          {
            controller.Execute(commandInfo);
            if (filePackArgs.MoveToArchive && Directory.Exists(path))
            {
              Directory.Delete(path);
            }
          }
          i++;
        }
      }
      else
      {
        CommandInfo commandInfo = commandInfos[0];
        string targetPath = filePackArgs.OutputDirectoryPath;
        string targetName = Path.GetFileName(targetPath);
        string outputPath = Path.Combine(targetPath, $"{targetName}.7z");
        OnFilePackStarted(processId, outputPath);
        if (filePackArgs.NormalizeProperties && !filePackArgs.Dryrun)
        {
          string[] paths = GetDirectoriesAndFilePaths(filePackArgs.InputPaths);
          Process_ChangeAttributes(paths);
        }
        if (!filePackArgs.Dryrun)
        {
          controller.Execute(commandInfo);
        }
      }

      OnProcessFinished(processId);
    }

    private CommandInfo[] Process_BuildCommandInfos(FilePackProcessArgs processArgs)
    {
      FilePackArguments filePackArgs = processArgs.Arguments;
      SevenZipCommandBuilder builder = new SevenZipCommandBuilder();
      builder.ExecutablePath = @".\7-zip\7z.exe";
      builder.CompressionLevel = MapCompressionLevel(filePackArgs.CompressionLevel);
      builder.DeleteAfterArchiving = filePackArgs.MoveToArchive;
      if (filePackArgs.DictionarySize.HasValue && filePackArgs.DictionarySize > 0)
      {
        builder.DictionarySize = StorageSize.FromMegabyte(filePackArgs.DictionarySize.Value);
      }
      if (filePackArgs.SolidBlockSize.HasValue)
      {
        builder.SolidMode = true;
        if (filePackArgs.SolidBlockSize > 0)
        {
          builder.SolidBlockSize = StorageSize.FromMegabyte(filePackArgs.SolidBlockSize.Value);
        }
      }
      else
      {
        builder.SolidMode = false;
        builder.SolidBlockSize = null;
      }
      if (filePackArgs.SplitFileSize.HasValue && filePackArgs.SplitFileSize > 0)
      {
        builder.SplitSize = StorageSize.FromMegabyte(filePackArgs.SplitFileSize.Value);
      }
      if (filePackArgs.TheadCount.HasValue && filePackArgs.TheadCount > 0)
      {
        builder.ThreadCount = filePackArgs.TheadCount.Value;
      }

      List<CommandInfo> commandInfos = new List<CommandInfo>();
      if (filePackArgs.SeparateInput)
      {
        foreach (string path in filePackArgs.InputPaths)
        {
          string[] inputPaths = PreProcessInputPaths(new string[] { path }, true);
          builder.InputPaths = inputPaths;
          string targetPath = filePackArgs.OutputDirectoryPath;
          string targetName = Path.GetFileName(path);
          string outputPath = Path.Combine(targetPath, $"{targetName}.7z");
          builder.OutputPath = outputPath;
          commandInfos.Add(builder.ToCommandInfo());
        }
      }
      else
      {
        builder.InputPaths = PreProcessInputPaths(filePackArgs.InputPaths, false);
        string targetPath = filePackArgs.OutputDirectoryPath;
        string targetName = Path.GetFileName(targetPath);
        string outputPath = Path.Combine(targetPath, $"{targetName}.7z");
        builder.OutputPath = outputPath;
        commandInfos.Add(builder.ToCommandInfo());
      }

      return commandInfos.AsArray();
    }

    private void Process_ChangeAttributes(string[] paths)
    {
      SetPropertiesOptions options = new SetPropertiesOptions
      {
        Archive = true,
        ReadOnly = false,
        Hidden = false,
        System = false,
        CreatedTime = new DateTime(637162416000000000, DateTimeKind.Utc),
        AccessedTime = new DateTime(637162416000000000, DateTimeKind.Utc),
        ModifiedTime = new DateTime(637162416000000000, DateTimeKind.Utc),
      };
      foreach (string path in paths)
      {
        FileExpress.SetProperties(path, options);
      }
    }

    protected void OnProcessStarted(Guid processId)
    {
      if (ProcessStarted != null)
      {
        ProcessStarted(processId);
      }
    }

    protected void OnFilePackStarted(Guid processId, string outputPath)
    {
      if (FilePackStarted != null)
      {
        FilePackStarted(processId, outputPath);
      }
    }

    protected void OnFileHashFinished(Guid processId, string filePath)
    {
      if (FilePackFinished != null)
      {
        FilePackFinished(processId, filePath);
      }
    }

    protected void OnProcessFinished(Guid processId)
    {
      if (ProcessFinished != null)
      {
        ProcessFinished(processId);
      }
    }

    private string[] GetDirectoriesAndFilePaths(params string[] targetPaths)
    {
      List<string> results = new List<string>();
      foreach (string path in targetPaths)
      {
        bool isDirectory = Directory.Exists(path);
        if (isDirectory)
        {
          string[] subFileAndDirPaths = DirectoryExpress.GetDirectoriesAndFiles(path, true);
          results.Add(path);
          results.AddRange(subFileAndDirPaths);
        }
        bool isFile = File.Exists(path);
        if (isFile)
        {
          results.Add(path);
        }
      }
      return results.AsArray();
    }

    private string[] PreProcessInputPaths(ICollection<string> paths, bool notIncludeRoot)
    {
      List<string> results = new List<string>();
      int i = 0;
      foreach (string path in paths)
      {
        bool isDirectory = Directory.Exists(path);
        if (isDirectory)
        {
          if (notIncludeRoot)
          {
            string inputPath = Path.Combine(path, "**");
            results.Add(inputPath);
          }
          else
          {
            results.Add(path);
          }
        }
        bool isFile = File.Exists(path);
        if (isFile)
        {
          results.Add(path);
        }
        i++;
      }
      return results.AsArray();
    }

    private CompressionLevel MapCompressionLevel(string level)
    {
      if (level == "max")
      {
        return CompressionLevel.Ultra;
      }
      if (level == "high")
      {
        return CompressionLevel.Maximum;
      }
      if (level == "normal")
      {
        return CompressionLevel.Normal;
      }
      if (level == "low")
      {
        return CompressionLevel.Fast;
      }
      return CompressionLevel.Copy;
    }

    private class FilePackProcessArgs
    {
      public Guid ProcessId { get; set; }

      public FilePackArguments Arguments { get; set; }
    }
  }
}
