using System;
using System.Collections.Generic;
using System.Windows.Forms;
using CommandLine;
using Xyz.TForce.Unifiler.WinFormHost;

namespace Xyz.TForce.Unifiler
{

  public class Program
  {

    [STAThread]
    static void Main(string[] args)
    {
      _ = Parser.Default.ParseArguments<CommandLineArgs>(args)
        .WithParsed(Execute)
        .WithNotParsed(Exit);
    }

    private static void Execute(CommandLineArgs args)
    {
      _ = Application.SetHighDpiMode(HighDpiMode.SystemAware);
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);
      Application.Run(new MainForm());
    }

    private static void Exit(IEnumerable<Error> errors)
    {
    }
  }
}
