namespace Xyz.TForce.Unifiler.Application.Models
{

  public class FileHashArguments
  {

    public string[] TargetPaths { get; set; }

    public bool IncludeCrc32 { get; set; }

    public bool IncludeMd5 { get; set; }

    public bool IncludeSha1 { get; set; }

    public bool IncludeSha256 { get; set; }

    public bool IncludeSha512 { get; set; }
  }
}
