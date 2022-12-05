namespace Xyz.TForce.Unifiler.Application.Models
{

  public class CreateMd5FileArguments
  {

    public string[] FilePaths { get; set; }

    public BytesEx[] Md5Hashes { get; set; }
  }
}
