using System.Reflection;

namespace ConsoleApp.Models;

public record IndexedEntity {
  public object this[string propertyName] {
    get
      {
        PropertyInfo property = GetType().GetProperty(propertyName);
        return property?.GetValue(this) ?? throw new ArgumentException($"Property '{propertyName}' not found.");
      }
  }
}
