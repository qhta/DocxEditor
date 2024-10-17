namespace Docx.Automation;

/// <summary>
/// Represents a break in a document.
/// </summary>
public interface Break : _Element, _RunItem, _TextReadonlyElement
{
  /// <summary>
  /// Type of break.
  /// </summary>
  public BreakType? Type { get; set; }
}