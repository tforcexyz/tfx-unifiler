using System;
using Xyz.TForce.Unifiler.Application.Models;

namespace Xyz.TForce.Unifiler.Application
{

  public delegate void ProcessStart(Guid processId);
  public delegate void FileHashStart(Guid processId, string filePath, long fileSize);
  public delegate void FileHashFinish(Guid processId, string filePath, FileHashes hashes);
  public delegate void ProcessFinish(Guid processId, FileHashes[] allHashes);

  public interface IFileHashBackgroundService
  {

    event ProcessStart ProcessStarted;
    event FileHashStart FileHashStarted;
    event FileHashFinish FileHashFinished;
    event ProcessFinish ProcessFinished;

    Guid StartAsync(FileHashArguments args);
  }
}
