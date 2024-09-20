namespace Docx.Automation;

/// <summary>
/// Collection of document properties.
/// </summary>
public interface DocumentProperties : IElementCollection<DocumentProperty>
{
  /// <summary>
  /// Returns available built-in properties.
  /// </summary>
  public string[] AvailableProperties { get; }

  /// <summary>
  /// Returns a value of the property.
  /// </summary>
  /// <param name="name">Name of the property. Must be one of AvailableProperties names.</param>
  public object? Get(string name);

  /// <summary>
  /// Sets a value of the property.
  /// </summary>
  /// <param name="name">Name of the property. Must be one of AvailableProperties names.</param>
  /// <param name="value">Value of the property. If it is null then property is deleted, otherwise Value must be assignable to the property</param>
  public void Set(string name, object? value);
}
