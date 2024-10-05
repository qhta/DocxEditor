namespace Docx.Automation;

/// <summary>
/// Interface for a paragraph
/// </summary>
public interface Paragraph
{
  /// <summary>
  /// Properties of the paragraph
  /// </summary>
  public ParagraphProperties Properties { get; }

  /// <summary>
  /// Runs in the paragraph
  /// </summary>
  public IEnumerable<Run> Runs { get; }
}
