using ConsoleApp.Helpers;
using ConsoleApp.Models;
using Microsoft.Extensions.Logging;
using System.Reflection;

namespace ConsoleApp.Tasks;

public class FieldsLimitedExpressionBackendTask : ExpressionBackendTask {
  private readonly FieldsObjectMapper _fieldsObjectMapper;

  private readonly ILogger<FieldsLimitedExpressionBackendTask> _logger;

  public FieldsLimitedExpressionBackendTask(ILogger<FieldsLimitedExpressionBackendTask> logger, FieldsObjectMapper fieldsObjectMapper) {
    _logger = logger ?? throw new ArgumentNullException();
    _fieldsObjectMapper = fieldsObjectMapper ?? throw new ArgumentNullException();
  }

  protected override void WriteRecord<T>(T record, params string[] fields) {
    _logger.LogInformation($"Record {record.Id}:");
    var fieldMessages = fields.Select(field => {
        var recordStringValue = GetRecordValueByField(record, field);
        var messageFieldValue = recordStringValue != null ? recordStringValue : "record[field]";
        return $"{field} = {messageFieldValue}";
    });
    _logger.LogInformation(string.Join("; ", fieldMessages));
  }

  protected override async Task<IQueryable<T>> LoadRecords<T>(params string[] fields) {
    var records = await base.LoadRecords<T>(fields);
    if (fields == null || !fields.Any())
    {
      return records;
    }
    var mapped = _fieldsObjectMapper.Map(records, fields);
    return mapped;
  }

  private string GetRecordValueByField(object record, string field) {
    if (record is IndexedEntity) {
      return (record as IndexedEntity)[field].ToString();
    } else {
      PropertyInfo property = typeof(Product).GetProperty(field);
      if (property == null) {
        return null;
      }
      return property.GetValue(record).ToString();
    }
  }
}

