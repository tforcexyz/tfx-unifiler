using System.Collections.Generic;
using Xyz.TForce.Unifiler.Interop.ExternalExecutables.Types.SevenZip;

namespace Xyz.TForce.Unifiler.Interop.ExternalExecutables
{

  public class SevenZipCommandBuilder
  {

    public string ExecutablePath { get; set; }

    public CompressionLevel CompressionLevel { get; set; }

    public StorageSize DictionarySize { get; set; }

    public bool SolidMode { get; set; }

    public StorageSize SolidBlockSize { get; set; }

    public StorageSize SplitSize { get; set; }

    public int ThreadCount { get; set; }

    public string OutputPath { get; set; }

    public IEnumerable<string> InputPaths { get; set; }

    public bool DeleteAfterArchiving { get; set; }

    public string Build()
    {
      string quotedExecutablePath = QuotePath(ExecutablePath);
      string arguments = BuildArguments();
      return $"{quotedExecutablePath} {arguments}";
    }

    public string BuildArguments()
    {
      List<string> parameters = new List<string>();
      parameters.Add("a"); // add file to archive
      parameters.Add("-r"); // directory recursize
      parameters.Add("-t7z"); // use 7-zip archive format
      parameters.Add("-m0=LZMA2"); // use LZMA2 compression method

      if (DeleteAfterArchiving)
      {
        parameters.Add("-sdel");
      }
      parameters.Add($"-mx{(int)CompressionLevel}"); // compression level
      parameters.Add($"-myx{(int)CompressionLevel}"); // analysis level
      if (DictionarySize != null)
      {
        parameters.Add($"-md{DictionarySize.DynamicSize()}");
      }
      if (SolidMode)
      {
        if (SolidBlockSize == null)
        {
          parameters.Add("-ms=on");
        }
        else
        {
          parameters.Add($"-ms={SolidBlockSize.DynamicSize()}");
        }
      }
      else
      {
        parameters.Add("-ms=off");
      }
      if (ThreadCount > 0)
      {
        parameters.Add($"-mmt={ThreadCount}");
      }

      parameters.Add("--");
      parameters.Add(QuotePath(OutputPath));
      foreach (string path in InputPaths)
      {
        parameters.Add(QuotePath(path));
      }

      return string.Join(' ', parameters);
    }
    
    public CommandInfo ToCommandInfo()
    {
      string arguments = BuildArguments();
      return new CommandInfo
      {
        ExecutablePath = ExecutablePath,
        Arguments = arguments,
      };
    }

    private string QuotePath(string path)
    {
      string quotedPath = path;
      if (!path.StartsWith("\""))
      {
        quotedPath = "\"" + quotedPath;
      }
      if (!path.EndsWith("\""))
      {
        quotedPath = quotedPath + "\"";
      }
      return quotedPath;
    }
  }
}
