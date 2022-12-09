using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xyz.TForce.InOut.FileSystem;
using Xyz.TForce.Unifiler.Application;
using Xyz.TForce.Unifiler.Application.Background;
using Xyz.TForce.Unifiler.Application.Logic;
using Xyz.TForce.Unifiler.Application.Models;
using Xyz.TForce.Unifiler.Commands;

namespace Xyz.TForce.Unifiler
{
  internal partial class Program
  {

    private static readonly IDictionary<Guid, HashCommand> _hashCommands = new Dictionary<Guid, HashCommand>();

    private static void ExecuteHash(HashCommand command)
    {
      command.SourceDir = TrimTrailingPath(command.SourceDir);
      command.TargetDir = TrimTrailingPath(command.TargetDir);
      if (command.IsDebug)
      {
        Console.WriteLine($"Debug Mode{Environment.NewLine}----------");
        Console.WriteLine($"Command: hash");
        Console.WriteLine($"Source path: {command.SourceDir}");
        Console.WriteLine($"Selected items:");
        foreach (string targetPath in command.Selected)
        {
          Console.WriteLine($"    {targetPath}");
        }
        Console.WriteLine($"Hash algorithms:");
        foreach (string algorithm in command.Algorithms)
        {
          Console.WriteLine($"    {algorithm}");
        }
        Console.WriteLine($"Target path: {command.TargetDir}");
        Console.ReadLine();
        return;
      }
      if (command.Algorithms == null)
      {
        return;
      }

      IFileHashBackgroundService fileHashService = new FileHashBackgroundService();
      fileHashService.FileHashStarted += FileHashService_FileHashStarted;
      fileHashService.ProcessFinished += FileHashService_ProcessFinished;
      Guid processId = fileHashService.StartAsync(new FileHashArguments
      {
        TargetPaths = command.Selected.AsArray(),
        IncludeCrc32 = command.Algorithms.Contains("crc32"),
        IncludeMd5 = command.Algorithms.Contains("md5"),
        IncludeSha1 = command.Algorithms.Contains("sha1"),
        IncludeSha256 = command.Algorithms.Contains("sha256"),
        IncludeSha512 = command.Algorithms.Contains("sha512"),
      });
      _hashCommands[processId] = command;
    }

    private static void FileHashService_FileHashStarted(Guid processId, string filePath, long fileSize)
    {
      Console.WriteLine($"Processing {filePath}... {fileSize}");
    }

    private static void FileHashService_ProcessFinished(Guid processId, Application.Models.FileHashes[] allHashes)
    {
      HashCommand command = _hashCommands[processId];
      string parentDirectory = Coalesce(command.TargetDir, Environment.CurrentDirectory);
      bool isRoot = DirectoryExpress.IsRoot(parentDirectory);
      string fileName = isRoot ? "[Checksum]" : $"[{Path.GetFileName(parentDirectory)}]";
      string[] fileRelativePaths = allHashes.Select(hash =>
        {
          string relativePath = GetRelativePath(hash.FilePath, parentDirectory);
          return relativePath;
        })
        .AsArray();
      IFileFormatter formatter = new FileFormatter();

      if (command.Algorithms.Contains("crc32"))
      {
        string content = formatter.CreateSfvFile(new CreateSfvFileArguments
        {
          FilePaths = fileRelativePaths,
          Crc32Hashes = allHashes.Select(x => { return x.Crc32Hash; }).AsArray(),
        });
        string filePath = Path.Combine(parentDirectory, $"{fileName}.sfv");
        File.WriteAllText(filePath, content);
      }
      if (command.Algorithms.Contains("md5"))
      {
        string content = formatter.CreateMd5File(new CreateMd5FileArguments
        {
          FilePaths = fileRelativePaths,
          Md5Hashes = allHashes.Select(x => { return x.Md5Hash; }).AsArray(),
        });
        string filePath = Path.Combine(parentDirectory, $"{fileName}.md5");
        File.WriteAllText(filePath, content);
      }
      if (command.Algorithms.Contains("sha1"))
      {
        string content = formatter.CreateShaFile(new CreateShaFileArguments
        {
          FilePaths = fileRelativePaths,
          ShaHashes = allHashes.Select(x => { return x.Sha1Hash; }).AsArray(),
        });
        string filePath = Path.Combine(parentDirectory, $"{fileName}.sha");
        File.WriteAllText(filePath, content);
      }
      if (command.Algorithms.Contains("sha256"))
      {
        string content = formatter.CreateShaFile(new CreateShaFileArguments
        {
          FilePaths = fileRelativePaths,
          ShaHashes = allHashes.Select(x => { return x.Sha256Hash; }).AsArray(),
        });
        string filePath = Path.Combine(parentDirectory, $"{fileName}.sha256");
        File.WriteAllText(filePath, content);
      }
    }

    private static T Coalesce<T>(params T[] items)
    {
      for (int i = 0; i < items.Length; i++)
      {
        T item = items[i];
        if (item != null)
        {
          return item;
        }
      }
      return default(T);
    }

    private static string GetRelativePath(string path, string rootDirectoryPath)
    {
      string relativePath = path.Replace(rootDirectoryPath, string.Empty);
      relativePath = relativePath.Trim(Path.DirectorySeparatorChar);
      return relativePath;
    }
  }
}
