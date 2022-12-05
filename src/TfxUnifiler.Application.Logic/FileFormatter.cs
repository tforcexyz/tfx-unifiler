using System;
using System.Text;
using Xyz.TForce.Unifiler.Application.Models;

namespace Xyz.TForce.Unifiler.Application.Logic
{

  public class FileFormatter : IFileFormatter
  {

    public string CreateMd5File(CreateMd5FileArguments args)
    {
      if (args.FilePaths == null || args.Md5Hashes == null)
      {
        throw new ArgumentNullException();
      }
      if (args.FilePaths.Length != args.Md5Hashes.Length)
      {
        throw new ArgumentException();
      }

      StringBuilder builder = new StringBuilder();
      for (int i = 0; i < args.FilePaths.Length; i++)
      {
        string filePath = args.FilePaths[i];
        string hashHex = args.Md5Hashes[i].Hex;
        _ = builder.AppendFormat("{0} *{1}{2}", hashHex, filePath, Environment.NewLine);
      }
      string content = builder.ToString();
      return content;
    }

    public string CreateSfvFile(CreateSfvFileArguments args)
    {
      if (args.FilePaths == null || args.Crc32Hashes == null)
      {
        throw new ArgumentNullException();
      }
      if (args.FilePaths.Length != args.Crc32Hashes.Length)
      {
        throw new ArgumentException();
      }

      StringBuilder builder = new StringBuilder();
      for (int i = 0; i < args.FilePaths.Length; i++)
      {
        string filePath = args.FilePaths[i];
        string hashHex = args.Crc32Hashes[i].Hex;
        _ = builder.AppendFormat("{0} {1}{2}", filePath, hashHex, Environment.NewLine);
      }
      string content = builder.ToString();
      return content;
    }

    public string CreateShaFile(CreateShaFileArguments args)
    {
      if (args.FilePaths == null || args.ShaHashes == null)
      {
        throw new ArgumentNullException();
      }
      if (args.FilePaths.Length != args.ShaHashes.Length)
      {
        throw new ArgumentException();
      }

      StringBuilder builder = new StringBuilder();
      for (int i = 0; i < args.FilePaths.Length; i++)
      {
        string filePath = args.FilePaths[i];
        string hashHex = args.ShaHashes[i].Hex;
        _ = builder.AppendFormat("{0} *{1}{2}", hashHex, filePath, Environment.NewLine);
      }
      string content = builder.ToString();
      return content;
    }
  }
}
