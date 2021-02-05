using System.Collections.Generic;
using System.Linq;

namespace Xyz.TForce.Archetypes.DomainDrivenDevelopment
{

  public class ValidationCollection
  {

    internal ValidationCollection()
    {
      Items = new List<ValidationItem>();
    }

    public bool IsValidated
    {
      get { return !Items.Any(); }
    }

    public List<ValidationItem> Items { get; private set; }

    public void Clear()
    {
      Items.Clear();
    }

    public HashSet<string> GetMessages(string validationId)
    {
      ValidationItem item = Items.FirstOrDefault(x => { return x.Id == validationId; });
      if (item == null)
      {
        return new HashSet<string>();
      }
      return item.Messages;
    }

    public HashSet<string> GetTags(string validationId)
    {
      ValidationItem item = Items.FirstOrDefault(x => { return x.Id == validationId; });
      if (item == null)
      {
        return new HashSet<string>();
      }
      return item.Tags;
    }

    public void AddItem(string validationId, IEnumerable<string> messages, IEnumerable<string> tags)
    {
      ValidationItem item = Items.FirstOrDefault(x => { return x.Id == validationId; });
      if (item == null)
      {
        item = new ValidationItem(validationId, messages, tags);
        Items.Add(item);
      }
    }

    public bool ContainItem(string validationId)
    {
      return Items.Any(x => { return x.Id == validationId; });
    }

    public void RemoveItem(string validationId)
    {
      int index = Items.FindIndex(x => { return x.Id == validationId; });
      if (index > -1)
      {
        Items.RemoveAt(index);
      }
    }
  }
}
