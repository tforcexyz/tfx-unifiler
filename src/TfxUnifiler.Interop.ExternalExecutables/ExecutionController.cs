using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace Xyz.TForce.Unifiler.Interop.ExternalExecutables
{

  public delegate void ProcessFinished(ProcessFinishedEventArgs ev);
  public delegate void ProcessStarted(ProcessStartedEventArgs ev);

  public class ExecutionController
  {

    private CancellationToken _cancellationToken;

    public event ProcessStarted ProcessStarted;

    public event ProcessFinished ProcessFinished;

    public void StartProcessing(string[] commands)
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
        process.StartInfo.FileName = command.FilePath;
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
