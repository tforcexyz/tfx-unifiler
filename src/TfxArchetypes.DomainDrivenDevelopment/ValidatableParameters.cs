using System;

namespace Xyz.TForce.Archetypes.DomainDrivenDevelopment
{

  public abstract class ValidatableParameters : IValidatableParameters
  {

    protected ValidatableParameters()
    {
      Validations = new ValidationCollection();
    }

    protected ValidationCollection Validations { get; private set; }

    public abstract bool Validate();

    public void EnsureValidation()
    {
      bool isValidated = Validate();
      if (!isValidated)
      {
        Exception validationException = new ValidationException(Validations);
        throw validationException;
      }
    }
  }
}
