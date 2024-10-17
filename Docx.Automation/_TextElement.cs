namespace Docx.Automation;

/// <summary>
/// Represents an element that have a text read/write property.
/// </summary>
public interface _TextElement: _Element
{
  /// <summary>
  /// Text content of the element.
  /// </summary>
  public string? Text { get; set; }
}