namespace Docx.Automation;

/// <summary>
/// Interface for a run item.
/// </summary>
public interface IRunItem
{
  /// <summary>
  /// Parent must be a Run.
  /// </summary>
  public Run ParentRun { get; }
}