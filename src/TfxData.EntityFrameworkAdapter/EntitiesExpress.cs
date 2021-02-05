using System;
using System.Linq;
using System.Reflection;

namespace Xyz.TForce.Data.EntityFrameworkAdapter
{

  public static class EntitiesExpress
  {

    public static void CopyProperties(object source, object target)
    {
      if (source == null)
      {
        throw new ArgumentNullException(nameof(source));
      }
      if (target == null)
      {
        throw new ArgumentNullException(nameof(target));
      }
      Type sourceType = source.GetType();
      PropertyInfo[] sourceProperties = sourceType.GetProperties();
      Type targetType = target.GetType();
      PropertyInfo[] targetProperties = targetType.GetProperties();
      foreach (PropertyInfo sourceProperty in sourceProperties)
      {
        PropertyInfo targetProperty = targetProperties.FirstOrDefault(x => { return x.Name == sourceProperty.Name; });
        if (targetProperty == null)
        {
          // equivalent properties not found
          continue;
        }
        if (!sourceProperty.CanRead || !targetProperty.CanWrite || sourceProperty.GetGetMethod().IsVirtual || targetProperty.GetSetMethod().IsVirtual)
        {
          // read/write permission not allow value copy
          continue;
        }

        object value = sourceProperty.GetValue(source, null);
        targetProperty.SetValue(target, value, null);
      }
    }
  }
}
