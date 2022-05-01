using System;

namespace Xyz.TForce.Data
{

  public static class IEntityMetadataExtensions
  {

    public static void SetMetadataForCreation(this IEntityMetadata entityMetadata)
    {
      long currentSuperEpoch = DateTime.Now.ToSuperEpochUtc();
      entityMetadata.MetaCreatedTimeCode = currentSuperEpoch;
      entityMetadata.MetaModifiedTimeCode = currentSuperEpoch;
    }

    public static void SetMetadataForModification(this IEntityMetadata entityMetadata)
    {
      long currentSuperEpoch = DateTime.Now.ToSuperEpochUtc();
      entityMetadata.MetaModifiedTimeCode = currentSuperEpoch;
    }
  }
}
