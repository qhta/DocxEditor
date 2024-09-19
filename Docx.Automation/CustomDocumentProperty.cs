namespace Docx.Automation;
/// <summary>
/// Represents a single instance of a custom property.
/// </summary>
public interface CustomDocumentProperty: IElement
{
  /// <summary>
  /// Returns the name of the custom property.
  /// </summary>
  public string? Name { get; set; }


  /// <summary>
  /// Type of the property.
  /// </summary>
  public PropertyType? Type { get; set; }


  /// <summary>
  /// The data value of the Value property
  /// </summary>
  public object? Value { get; set; }
}
