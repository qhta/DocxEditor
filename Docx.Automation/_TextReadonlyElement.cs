namespace Docx.Automation;

/// <summary>
/// Represents an element that have a text read-only property.
/// </summary>
public interface _TextReadonlyElement: _Element
{
  /// <summary>
  /// Text content of the element.
  /// </summary>
  public string? Text { get; }
}