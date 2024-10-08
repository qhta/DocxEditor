namespace Docx.Automation;

/// <summary>
/// Represents a section in a document.
/// </summary>
public interface Section: IElement
{
  /// <summary>
  /// Gets the range in the section.
  /// </summary>
  public Range Range { get; }

}
