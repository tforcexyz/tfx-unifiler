using System;
using Xyz.TForce.Unifiler.Application.Models;

namespace Xyz.TForce.Unifiler.Application
{

  public interface IFilePackBackgroundService
  {

    public delegate void ProcessStart(Guid processId);
    public delegate void FilePackStart(Guid processId, string commandLine);
    public delegate void FilePackFinish(Guid processId, string path);
    public delegate void ProcessFinish(Guid processId);

    event ProcessStart ProcessStarted;
    event FilePackStart FilePackStarted;
    event FilePackFinish FilePackFinished;
    event ProcessFinish ProcessFinished;

    Guid StartAsync(FilePackArguments args);
  }
}
