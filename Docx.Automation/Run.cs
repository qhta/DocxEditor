namespace Docx.Automation;

/// <summary>
/// Interface for a text run.
/// </summary>
public interface Run: IRemovable
{
  /// <summary>
  /// Properties of the run.
  /// </summary>
  public RunProperties Properties { get; }

  /// <summary>
  /// Text of the run.
  /// </summary>
  public string Text { get; set; }
}
