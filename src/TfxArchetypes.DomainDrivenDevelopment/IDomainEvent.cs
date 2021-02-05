using System;
using MediatR;

namespace Xyz.TForce.Archetypes.DomainDrivenDevelopment
{

  public class IDomainEvent : INotification
  {

    Guid EventId { get; set; }

    long OccuredTimeCode { get; set; }
  }
}
