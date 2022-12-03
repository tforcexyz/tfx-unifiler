using System.Collections.Generic;
using System.IO;

namespace Xyz.TForce.InOut.FileSystem
{

  public static class DirectoryExpress
  {

    public static string[] GetDirectories(string path, bool isRecursive = false)
    {
      List<string> results = new List<string>();
      string[] directoryPaths = Directory.GetDirectories(path);
      foreach (string directoryPath in directoryPaths)
      {
        results.Add(directoryPath);
        if (isRecursive)
        {
          string[] subDirectoryPaths = GetDirectories(directoryPath, isRecursive);
          results.AddRange(subDirectoryPaths);
        }
      }
      return results.ToArray();
    }

    public static string[] GetDirectoriesAndFiles(string path, bool isRecursive = false)
    {
      List<string> results = new List<string>();
      string[] directoryPaths = Directory.GetDirectories(path);
      foreach (string directoryPath in directoryPaths)
      {
        results.Add(directoryPath);
        if (isRecursive)
        {
          string[] subDirectoryPaths = GetDirectoriesAndFiles(directoryPath, isRecursive);
          results.AddRange(subDirectoryPaths);
        }
      }
      string[] filePaths = Directory.GetFiles(path);
      results.AddRange(filePaths);
      return results.ToArray();
    }

    public static string[] GetFiles(string path, bool isRecursive = false)
    {
      List<string> results = new List<string>();
      string[] filePaths = Directory.GetFiles(path);
      results.AddRange(filePaths);
      if (isRecursive)
      {
        string[] directoryPaths = Directory.GetDirectories(path);
        foreach (string directoryPath in directoryPaths)
        {
          string[] subFilePaths = GetFiles(directoryPath, isRecursive);
          results.AddRange(subFilePaths);
        }
      }
      return results.ToArray();
    }

    public static bool IsRoot(string path)
    {
      DirectoryInfo directoryInfo = new DirectoryInfo(path);
      return directoryInfo.Parent == null;
    }
  }
}
