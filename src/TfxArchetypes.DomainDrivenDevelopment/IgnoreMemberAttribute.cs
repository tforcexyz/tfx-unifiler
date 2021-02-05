using System;

namespace Xyz.TForce.Archetypes.DomainDrivenDevelopment
{

  // Source: https://github.com/jhewlett/ValueObject
  [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
  public class IgnoreMemberAttribute : Attribute
  {
  }
}
