using System;

namespace Xyz.TForce.Archetypes.DomainDrivenDevelopment
{

  public class BaseCommand : ICommand
  {

    public BaseCommand()
    {
      CommandId = Guid.NewGuid();
    }

    protected BaseCommand(Guid id)
    {
      CommandId = id;
    }

    public Guid CommandId { get; }
  }

  public abstract class BaseCommand<TResult> : ICommand<TResult>
  {

    public BaseCommand()
    {
      CommandId = Guid.NewGuid();
    }

    protected BaseCommand(Guid id)
    {
      CommandId = id;
    }

    public Guid CommandId { get; }
  }
}
