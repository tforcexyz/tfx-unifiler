using System.Windows.Forms;
using Xyz.TForce.TfxUnifiler.AspNetHost;

namespace Xyz.TForce.TfxUnifiler.WinFormHost
{

  public partial class MainForm : Form
  {

    public MainForm()
    {
      InitializeComponent();
      chromiumWebBrowser1.Load("https://www.google.com/");

      InternalWebHost webHost = new InternalWebHost();
      webHost.Started += WebHost_Started;
      webHost.StartAsync();
    }

    private void WebHost_Started(WebHostStartedEventArgs args)
    {
      chromiumWebBrowser1.Load($"http://localhost:{args.Port}/");
    }
  }
}
