namespace Docx.Automation;

/// <summary>
/// Interface for a text run.
/// </summary>
public interface Run: ITextElement
{
  /// <summary>
  /// Properties of the run.
  /// </summary>
  public RunProperties Properties { get; }

}
