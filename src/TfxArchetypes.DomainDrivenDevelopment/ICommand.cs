using System;
using MediatR;

namespace Xyz.TForce.Archetypes.DomainDrivenDevelopment
{

  public interface ICommand : IRequest
  {
    Guid CommandId { get; }
  }

  public interface ICommand<out TResult> : IRequest<TResult>
  {
    Guid CommandId { get; }
  }
}
