namespace Docx.Automation;

/// <summary>
/// Interface for a text run.
/// </summary>
public interface Run: _TextElement
{
  /// <summary>
  /// Properties of the run.
  /// </summary>
  public RunProperties Properties { get; }

}
