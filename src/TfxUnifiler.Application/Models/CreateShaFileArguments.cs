namespace Xyz.TForce.Unifiler.Application.Models
{

  public class CreateShaFileArguments
  {

    public string[] FilePaths { get; set; }

    public BytesEx[] ShaHashes { get; set; }
  }
}
