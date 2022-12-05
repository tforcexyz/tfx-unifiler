namespace Xyz.TForce.Unifiler.Application.Models
{

  public class FileHashes
  {

    public string FilePath { get; set; }

    public BytesEx Crc32Hash { get; set; }

    public BytesEx Md5Hash { get; set; }

    public BytesEx Sha1Hash { get; set; }

    public BytesEx Sha256Hash { get; set; }

    public BytesEx Sha512Hash { get; set; }
  }
}
