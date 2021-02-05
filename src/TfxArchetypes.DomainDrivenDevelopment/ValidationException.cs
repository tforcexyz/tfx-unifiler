using System;

namespace Xyz.TForce.Archetypes.DomainDrivenDevelopment
{

  public class ValidationException : Exception
  {

    public ValidationException(ValidationCollection validations)
      : base("Messages.BaseResult_ValidationExeption")
    {
      Validations = validations;
    }

    public ValidationException(string message, ValidationCollection validations)
      : base(message)
    {
      Validations = validations;
    }

    public ValidationCollection Validations { get; }
  }
}
