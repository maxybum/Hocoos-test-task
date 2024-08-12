using System.Reflection;

namespace ConsoleApp.Helpers;

public class FieldsObjectMapper {

  public IQueryable<T> Map<T>(IQueryable<T> source, params string[] fields) {
    var properties = typeof(T).GetProperties().Where(p => fields.Contains(p.Name)).ToArray();
    return source.Select(record => CreateObjectWithFields(record, properties)).Cast<T>().AsQueryable();
  }

  private object CreateObjectWithFields<T>(T record, PropertyInfo[] properties) {
    var result = Activator.CreateInstance(typeof(T));
    foreach (var property in properties)
    {
      var value = property.GetValue(record);
      property.SetValue(result, value);
    }
    return result;
  }
}

