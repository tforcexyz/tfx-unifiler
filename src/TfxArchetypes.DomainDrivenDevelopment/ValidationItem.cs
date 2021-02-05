using System.Collections.Generic;

namespace Xyz.TForce.Archetypes.DomainDrivenDevelopment
{

  public class ValidationItem
  {

    internal ValidationItem(string validationId, IEnumerable<string> messages, IEnumerable<string> tags)
    {
      Messages = new HashSet<string>();
      Tags = new HashSet<string>();
      Id = validationId;
      if (messages != null)
      {
        foreach (string message in messages)
        {
          _ = Messages.Add(message);
        }
      }
      if (tags != null)
      {
        foreach (string tag in tags)
        {
          _ = Tags.Add(tag);
        }
      }
    }

    public string Id { get; set; }

    public HashSet<string> Messages { get; private set; }

    public HashSet<string> Tags { get; private set; }
  }
}
