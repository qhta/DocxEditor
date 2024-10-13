namespace Docx.Automation;

/// <summary>
/// The mode of numbering restart.
/// </summary>
public enum RestartNumber
{
  /// <summary>
  /// Continue Numbering From Previous Section.
  /// </summary>
  Continuous, 
  
  /// <summary>
  /// Restart Numbering For Each Section.
  /// </summary>
  EachSection,

  /// <summary>
  /// Restart Numbering On Each Page.
  /// </summary>
  EachPage,
}