namespace Docx.Automation;

/// <summary>
/// Represents an element that have a text read/write property.
/// </summary>
public interface ITextElement: IElement
{
  /// <summary>
  /// Text content of the element.
  /// </summary>
  public string? Text { get; set; }
}