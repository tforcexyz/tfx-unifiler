using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Results;
using MediatR;

namespace Xyz.TForce.Archetypes.DomainDrivenDevelopment
{

  public class FluentValidationPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
  {

    private readonly IList<IValidator<TRequest>> _validators;

    public FluentValidationPipelineBehavior(IList<IValidator<TRequest>> validators)
    {
      _validators = validators;
    }

    public Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
    {
      ValidationFailure[] errors = _validators
          .Select(x => { return x.Validate(request); })
          .SelectMany(x => { return x.Errors; })
          .Where(x => { return x != null; })
          .ToArray();

      if (errors.Any())
      {
        ValidationCollection validations = new ValidationCollection();
        foreach (ValidationFailure error in errors)
        {
          validations.AddItem(error.ErrorCode, new[] { error.ErrorMessage }, new[] { error.PropertyName });
        }
        ValidationException exception = new ValidationException(validations);
        throw exception;
      }

      return next();
    }
  }
}
