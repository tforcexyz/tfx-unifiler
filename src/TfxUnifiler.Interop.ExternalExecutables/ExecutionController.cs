using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace Xyz.TForce.Unifiler.Interop.ExternalExecutables
{

  public class ExecutionController
  {

    public delegate void ProcessFinish(ProcessFinishedEventArgs ev);
    public delegate void ProcessStart(ProcessStartedEventArgs ev);

    private CancellationToken _cancellationToken;

    public event ProcessStart ProcessStarted;

    public event ProcessFinish ProcessFinished;

    public void Execute(CommandInfo command)
    {
      Process process = new Process();
      process.StartInfo.FileName = command.ExecutablePath;
      process.StartInfo.Arguments = command.Arguments;
      process.StartInfo.UseShellExecute = true;
      Guid processId = Guid.NewGuid();
      OnProcessStarted(processId);
      try
      {
        _ = process.Start();
        process.WaitForExit();
        OnProcessFinished(processId, true, null);
      }
      catch (Exception ex)
      {
        OnProcessFinished(processId, false, ex);
      }
    }

    public void StartAsync(string[] commands)
    {
      _cancellationToken = new CancellationToken();
      StartProcessingParameters parameters = new StartProcessingParameters
      {
      };
      _ = Task.Factory.StartNew(StartProcessingBackground, parameters, _cancellationToken);
    }

    public void StartProcessingBackground(object args)
    {
      StartProcessingParameters parameters = (StartProcessingParameters)args;
      foreach (CommandInfo command in parameters.Commands)
      {
        Process process = new Process();
        process.StartInfo.FileName = command.ExecutablePath;
        process.StartInfo.Arguments = command.Arguments;
        process.StartInfo.UseShellExecute = true;
        Guid processId = Guid.NewGuid();
        OnProcessStarted(processId);
        try
        {
          _ = process.Start();
          process.WaitForExit();
          OnProcessFinished(processId, true, null);
        }
        catch (Exception ex)
        {
          OnProcessFinished(processId, false, ex);
        }
      }
    }

    protected void OnProcessStarted(Guid processId)
    {
      ProcessStartedEventArgs ev = new ProcessStartedEventArgs
      {
        ProcessId = processId,
      };
      if (ProcessStarted != null)
      {
        ProcessStarted(ev);
      }
    }

    protected void OnProcessFinished(Guid processId, bool isSuccess, Exception error)
    {
      ProcessFinishedEventArgs ev = new ProcessFinishedEventArgs
      {
        ProcessId = processId,
        Error = error,
        IsSuccess = isSuccess,
      };
      if (ProcessFinished != null)
      {
        ProcessFinished(ev);
      }
    }

    public class StartProcessingParameters
    {
      public CommandInfo[] Commands { get; set; }
    }
  }
}
