using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Xyz.TForce.TfxUnifiler
{
  public partial class MainForm : Form
  {
    public MainForm()
    {
      InitializeComponent();
      chromiumWebBrowser1.Load("https://www.google.com/");
    }
  }
}
