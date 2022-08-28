namespace Xyz.TForce.Unifiler
{

  public static class MainModule
  {
#if DEBUG
      public const bool IN_DEBUG_MODE = true;
#else
    public const bool IN_DEBUG_MODE = false;
#endif

    public static class Constants
    {
      public const int DEVELOPMENT_PORT = 22080;
    }
  }
}
