namespace Docx.Automation;

/// <summary>
/// Specifies when the line numbering in this section shall be reset to the line number specified by the start attribute's value.
/// </summary>
public enum LineNumberRestart
{
  /// <summary>
  /// Restart Line Numbering on Each Page.
  /// </summary>
  NewPage,
  /// <summary>
  /// Restart Line Numbering for Each Section.
  /// </summary>
  NewSection,
  /// <summary>
  /// Continue Line Numbering From Previous Section.
  /// </summary>
  Continuous,
}