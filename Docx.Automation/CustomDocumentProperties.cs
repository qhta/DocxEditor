namespace Docx.Automation;

/// <summary>
/// Collection of document custom properties.
/// </summary>
public interface CustomDocumentProperties: IElementCollection<CustomDocumentProperty>
{
  /// <summary>
  /// Adds a custom property to the collection.
  /// </summary>
  /// <param name="name">The string of the Name of the property.</param>
  /// <param name="linkToContent">Specifies whether the LinkToContent property is linked to the contents of the container document.
  /// If this argument is True, the LinkSource argument is required; if it's False, the Value argument is required.</param>
  /// <param name="type">The data type of the Type property.
  /// Can be one of the following: Boolean, Date, Float, Number, or String.</param>
  /// <param name="value"></param>
  /// <param name="linkSource"></param>
  public void Add(string name, bool linkToContent, PropertyType type, object? value, string? linkSource);
}
