using System;
using System.Windows.Forms;

namespace Xyz.TForce.TfxUnifiler
{

  public class Program
  {

    [STAThread]
    static void Main()
    {
      Application.SetHighDpiMode(HighDpiMode.SystemAware);
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);
      Application.Run(new MainForm());
    }
  }
}
