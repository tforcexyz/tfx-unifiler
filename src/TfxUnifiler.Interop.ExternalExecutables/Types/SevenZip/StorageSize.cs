namespace Xyz.TForce.Unifiler.Interop.ExternalExecutables.Types.SevenZip
{

  public class StorageSize
  {

    private const long KILOBYTE = 1024;
    private const long MEGABYTE = 1048576;
    private const long GIGABYTE = 1073741824;

    private readonly long _sizeInByte;

    public StorageSize(long numberOfBytes)
    {
      _sizeInByte = numberOfBytes;
    }

    public string Byte
    {
      get { return $"{_sizeInByte}b"; }
    }

    public string Kilobyte
    {
      get { return $"{_sizeInByte / KILOBYTE}k"; }
    }

    public string Megabyte
    {
      get { return $"{_sizeInByte / MEGABYTE}m"; }
    }

    public string Gigabyte
    {
      get { return $"{_sizeInByte / 1073741824}g"; }
    }

    public string DynamicSize()
    {
      if (_sizeInByte > GIGABYTE)
      {
        return Gigabyte;
      }
      if (_sizeInByte > MEGABYTE)
      {
        return Megabyte;
      }
      if (_sizeInByte > KILOBYTE)
      {
        return Kilobyte;
      }
      return Byte;
    }

    public static StorageSize FromByte(long numberOfBytes)
    {
      return new StorageSize(numberOfBytes);
    }

    public static StorageSize FromKilobyte(long numberOfKBs)
    {
      return new StorageSize(numberOfKBs * KILOBYTE);
    }

    public static StorageSize FromMegabyte(long numberOfMBs)
    {
      return new StorageSize(numberOfMBs * MEGABYTE);
    }

    public static StorageSize FromGigabyte(long numberOfGBs)
    {
      return new StorageSize(numberOfGBs * GIGABYTE);
    }
  }
}
