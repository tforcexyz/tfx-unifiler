using System;

namespace Xyz.TForce.Archetypes.DomainDrivenDevelopment
{

  public abstract class QueryBase<TResult> : ICommand<TResult>
  {

    public QueryBase()
    {
      CommandId = Guid.NewGuid();
    }

    protected QueryBase(Guid id)
    {
      CommandId = id;
    }

    public Guid CommandId { get; }
  }
}
