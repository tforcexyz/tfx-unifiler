namespace Xyz.TForce.InOut.FileSystem
{

  public class FileHashes
  {

    public string FilePath { get; set; }

    public BytesEx Crc32 { get; set; }

    public BytesEx Md5 { get; set; }

    public BytesEx Sha1 { get; set; }

    public BytesEx Sha256 { get; set; }

    public BytesEx Sha512 { get; set; }
  }
}
