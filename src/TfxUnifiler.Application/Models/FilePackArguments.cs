namespace Xyz.TForce.Unifiler.Application.Models
{

  public class FilePackArguments
  {

    public string[] InputPaths { get; set; }

    public string OutputDirectoryPath { get; set; }

    public bool NormalizeProperties { get; set; }

    public string CompressionLevel { get; set; }

    public bool MoveToArchive { get; set; }

    public long? DictionarySize { get; set; }

    public long? SolidBlockSize { get; set; }

    public long? SplitFileSize { get; set; }

    public int? TheadCount { get; set; }

    public bool SeparateInput { get; set; }

    public bool Dryrun { get; set; }
  }
}
