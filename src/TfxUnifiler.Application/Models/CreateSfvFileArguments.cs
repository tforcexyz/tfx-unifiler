namespace Xyz.TForce.Unifiler.Application.Models
{

  public class CreateSfvFileArguments
  {

    public string[] FilePaths { get; set; }

    public BytesEx[] Crc32Hashes { get; set; }
  }
}
