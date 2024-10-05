namespace Docx.Automation;

/// <summary>
/// Interface for element that can be removed.
/// </summary>
public interface IRemovable
{
  /// <summary>
  /// Remove the element. Returns true if the operation was successful.
  /// </summary>
  public bool Remove();
}
