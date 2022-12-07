using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Xyz.TForce.Cryptography;

namespace Xyz.TForce.Unifiler.AspNetHost
{

  public delegate void WebHostStarted(WebHostStartedEventArguments args);

  internal class InternalWebHost
  {

    public event WebHostStarted Started;

    public void StartAsync()
    {
      _ = Task.Run(StartAsyncTask);
    }

    private void StartAsyncTask()
    {
      int port;
      bool isSuccess;
      int retryCount = 0;
      do
      {
        port = GetPortNumber();
        IWebHost webHost = CreateWebHost(port);
        Task pendingTask = webHost.RunAsync();
        Thread.Sleep(1000);
        isSuccess = !pendingTask.IsFaulted;
        retryCount++;
      }
      while (!isSuccess && retryCount <= 3);
      if (Started != null && isSuccess)
      {
        Started(new WebHostStartedEventArguments
        {
          Port = port,
        });
      }
    }

    private IWebHost CreateWebHost(int port)
    {
      IConfigurationRoot config = new ConfigurationBuilder()
        .SetBasePath(Path.GetDirectoryName(typeof(InternalWebHost).Assembly.Location))
        .AddJsonFile("appsettings.json", optional: true)
        .Build();

      return WebHost.CreateDefaultBuilder()
        .UseKestrel()
        .UseUrls($"http://*:{port}")
        .UseConfiguration(config)
        .UseStartup<Startup>()
        .Build();
    }

    private int GetPortNumber()
    {
      return MainModule.IN_DEBUG_MODE
        ? MainModule.Constants.DEVELOPMENT_PORT
        : RandomExpress.RandomizeInteger(10000, 19999);
    }
  }
}
