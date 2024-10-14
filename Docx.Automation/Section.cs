namespace Docx.Automation;

/// <summary>
/// Represents a section in a document.
/// </summary>
public interface Section: _Element
{
  /// <summary>
  /// Gets the range in the section.
  /// </summary>
  public Range Range { get; }

  /// <summary>
  /// Properties of the section
  /// </summary>
  SectionProperties Properties { get; set; }

}
