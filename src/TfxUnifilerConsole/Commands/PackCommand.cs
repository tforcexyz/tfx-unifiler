using CommandLine;

namespace Xyz.TForce.Unifiler.Commands
{

  [Verb("pack")]
  public class PackCommand : SharedCommand
  {

    [Option("normalize")]
    public bool NormalizeProperties { get; set; }

    [Option("compress", Default = "normal")]
    public string Compress { get; set; }

    [Option("move")]
    public bool MoveToArchive { get; set; }

    [Option("separate")]
    public bool SeparateInput { get; set; }

    [Option("dict")]
    public long? DictionarySize { get; set; }

    [Option("solid")]
    public long? SolidBlockSize { get; set; }

    [Option("split")]
    public long? SplitFileSize { get; set; }

    [Option("thread")]
    public int? ThreadCount { get; set; }
  }
}
