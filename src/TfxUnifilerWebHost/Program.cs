using System;
using System.Windows.Forms;
using Xyz.TForce.TfxUnifiler.WinFormHost;

namespace Xyz.TForce.TfxUnifiler
{

  public class Program
  {

    [STAThread]
    static void Main(string[] args)
    {
      Application.SetHighDpiMode(HighDpiMode.SystemAware);
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);
      Application.Run(new MainForm());
    }
  }
}
