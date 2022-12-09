using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using Xyz.TForce.InOut.FileSystem;
using Xyz.TForce.Unifiler.Application.Models;
using static Xyz.TForce.Unifiler.Application.IFileHashBackgroundService;

namespace Xyz.TForce.Unifiler.Application.Background
{

  public class FileHashBackgroundService : IFileHashBackgroundService
  {

    public event ProcessStart ProcessStarted;
    public event FileHashStart FileHashStarted;
    public event FileHashFinish FileHashFinished;
    public event ProcessFinish ProcessFinished;

    public Guid StartAsync(FileHashArguments args)
    {
      Thread thread = new Thread(Process);
      Guid processId = Guid.NewGuid();

      thread.Start(new FileHashProcessArgs
      {
        ProcessId = processId,
        Arguments = args,
      });

      return processId;
    }

    private void Process(object args)
    {
      FileHashProcessArgs processArgs = (FileHashProcessArgs)args;
      Guid processId = processArgs.ProcessId;
      FileHashArguments fileHashArgs = processArgs.Arguments;
      OnProcessStarted(processId);

      List<string> filePaths = new List<string>();
      foreach (string targetPath in fileHashArgs.TargetPaths)
      {
        bool isDirectory = Directory.Exists(targetPath);
        if (isDirectory)
        {
          string[] subFilePaths = DirectoryExpress.GetFiles(targetPath, true);
          filePaths.AddRange(subFilePaths);
          continue;
        }
        bool isFile = File.Exists(targetPath);
        if (isFile)
        {
          filePaths.Add(targetPath);
        }
      }
      List<Models.FileHashes> results = new List<Models.FileHashes>();
      foreach (string filePath in filePaths)
      {
        FileProperties fileProperties = FileExpress.GetFileProperties(filePath);
        OnFileHashStarted(processId, filePath, fileProperties.FileSize);

        InOut.FileSystem.FileHashes fileHashes = FileExpress.HashFile(filePath, new HashFileOptions
        {
          UseCrc32 = fileHashArgs.IncludeCrc32,
          UseMd5 = fileHashArgs.IncludeMd5,
          UseSha1 = fileHashArgs.IncludeSha1,
          UseSha256 = fileHashArgs.IncludeSha256,
          UseSha512 = fileHashArgs.IncludeSha512,
        });
        Models.FileHashes result = new Models.FileHashes
        {
          FilePath = filePath,
          Crc32Hash = fileHashes.Crc32,
          Md5Hash = fileHashes.Md5,
          Sha1Hash = fileHashes.Sha1,
          Sha256Hash = fileHashes.Sha256,
          Sha512Hash = fileHashes.Sha512,
        };
        results.Add(result);
        OnFileHashFinished(processId, filePath, result);
      }

      OnProcessFinished(processId, results.AsArray());
    }

    protected void OnProcessStarted(Guid processId)
    {
      if (ProcessStarted != null)
      {
        ProcessStarted(processId);
      }
    }

    protected void OnFileHashStarted(Guid processId, string filePath, long fileSize)
    {
      if (FileHashStarted != null)
      {
        FileHashStarted(processId, filePath, fileSize);
      }
    }

    protected void OnFileHashFinished(Guid processId, string filePath, Models.FileHashes hashes)
    {
      if (FileHashFinished != null)
      {
        FileHashFinished(processId, filePath, hashes);
      }
    }

    protected void OnProcessFinished(Guid processId, Models.FileHashes[] allHashes)
    {
      if (ProcessFinished != null)
      {
        ProcessFinished(processId, allHashes);
      }
    }

    private class FileHashProcessArgs
    {
      public Guid ProcessId { get; set; }

      public FileHashArguments Arguments { get; set; }
    }
  }
}
