using System;
using System.Linq;
using System.Reflection;

namespace Xyz.TForce.Archetypes.DomainDrivenDevelopment
{

  public abstract class ValueObject : IEquatable<ValueObject>
  {

    private FieldInfo[] _fields;
    private PropertyInfo[] _properties;

    private FieldInfo[] Fields
    {
      get
      {
        if (_fields == null)
        {
          _fields = GetType().GetFields(BindingFlags.Instance | BindingFlags.Public)
            .Where(x => { return x.GetCustomAttribute(typeof(IgnoreMemberAttribute)) == null; })
            .ToArray();
        }
        return _fields;
      }
    }

    private PropertyInfo[] Properties
    {
      get
      {

        if (_properties == null)
        {
          _properties = GetType()
            .GetProperties(BindingFlags.Instance | BindingFlags.Public)
            .Where(p => { return p.GetCustomAttribute(typeof(IgnoreMemberAttribute)) == null; })
            .ToArray();
          // Not available in .NET Core
          // !Attribute.IsDefined(p, typeof(IgnoreMemberAttribute))).ToList();
        }
        return _properties;
      }
    }

    public static bool operator ==(ValueObject obj1, ValueObject obj2)
    {
#pragma warning disable IDE0046 // Convert to conditional expression
      if (Equals(obj1, null))
      {
        if (Equals(obj2, null))
        {
          return true;
        }
        return false;
      }
      return obj1.Equals(obj2);
#pragma warning restore IDE0046 // Convert to conditional expression
    }

    public static bool operator !=(ValueObject obj1, ValueObject obj2)
    {
      return !(obj1 == obj2);
    }

    public override int GetHashCode()
    {
      unchecked //allow overflow
      {
        int hash = 17;
        foreach (PropertyInfo prop in Properties)
        {
          object value = prop.GetValue(this, null);
          hash = CalculateHash(hash, value);
        }

        foreach (FieldInfo field in Fields)
        {
          object value = field.GetValue(this);
          hash = CalculateHash(hash, value);
        }

        return hash;
      }
    }

    public bool Equals(ValueObject obj)
    {
      return Equals(obj as object);
    }

    public override bool Equals(object obj)
    {
#pragma warning disable IDE0046 // Convert to conditional expression
      if (obj == null || GetType() != obj.GetType())
      {
        return false;
      }
#pragma warning restore IDE0046 // Convert to conditional expression

      return Properties.All(x => { return PropertiesAreEqual(obj, x); })
        && Fields.All(x => { return FieldsAreEqual(obj, x); });
    }

    private bool FieldsAreEqual(object obj, FieldInfo field)
    {
      return Equals(field.GetValue(this), field.GetValue(obj));
    }

    private bool PropertiesAreEqual(object obj, PropertyInfo prop)
    {
      return Equals(prop.GetValue(this, null), prop.GetValue(obj, null));
    }

    private int CalculateHash(int seed, object value)
    {
      int currentHash = value != null
        ? value.GetHashCode()
        : 0;
      return (seed * 23) + currentHash;
    }
  }
}
