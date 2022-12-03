using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using Force.Crc32;
using Xyz.TForce.InOut.FileSystem.Types;

namespace Xyz.TForce.InOut.FileSystem
{

  public static class FileExpress
  {

    public static FileProperties GetFileProperties(string path)
    {
      FileInfo fileInfo = new FileInfo(path);
      FileProperties result = new FileProperties
      {
        FilePath = path,
        FileSize = fileInfo.Length,
      };
      return result;
    }

    public static FileHashes HashFile(string path, HashFileOptions options)
    {
      Dictionary<string, HashAlgorithm> hashers = new Dictionary<string, HashAlgorithm>();
      if (options.UseCrc32)
      {
        hashers.Add(HashAlgorithmNames.Crc32, new Crc32Algorithm());
      }
      if (options.UseMd5)
      {
        hashers.Add(HashAlgorithmNames.Md5, MD5.Create());
      }
      if (options.UseSha1)
      {
        hashers.Add(HashAlgorithmNames.Sha1, SHA1.Create());
      }
      if (options.UseSha256)
      {
        hashers.Add(HashAlgorithmNames.Sha256, SHA256.Create());
      }
      if (options.UseSha512)
      {
        hashers.Add(HashAlgorithmNames.Sha512, SHA512.Create());
      }

      using (Stream fileStream = File.OpenRead(path))
      {
        byte[] buffer = new byte[1048576];
        int byteRead;
        do
        {
          byteRead = fileStream.Read(buffer, 0, buffer.Length);
          foreach (HashAlgorithm hasher in hashers.Values)
          {
            if (byteRead == buffer.Length)
            {
              _ = hasher.TransformBlock(buffer, 0, byteRead, null, 0);
            }
            else
            {
              _ = hasher.TransformFinalBlock(buffer, 0, byteRead);
            }
          }
        }
        while (byteRead == buffer.Length);
      }

      FileHashes result = new FileHashes
      {
        FilePath = path,
      };
      if (options.UseCrc32)
      {
        result.Crc32 = BytesEx.FromBytes(hashers[HashAlgorithmNames.Crc32].Hash);
      }
      if (options.UseMd5)
      {
        result.Md5 = BytesEx.FromBytes(hashers[HashAlgorithmNames.Md5].Hash);
      }
      if (options.UseSha1)
      {
        result.Sha1 = BytesEx.FromBytes(hashers[HashAlgorithmNames.Sha1].Hash);
      }
      if (options.UseSha256)
      {
        result.Sha256 = BytesEx.FromBytes(hashers[HashAlgorithmNames.Sha256].Hash);
      }
      if (options.UseSha512)
      {
        result.Sha512 = BytesEx.FromBytes(hashers[HashAlgorithmNames.Sha512].Hash);
      }

      return result;
    }
  }
}
