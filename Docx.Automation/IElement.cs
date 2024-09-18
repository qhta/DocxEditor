namespace Docx.Automation;

/// <summary>
/// Collection of elements.
/// </summary>
public interface IElement
{
  /// <summary>
  /// Returns an Application object that represents the DocxControls application.
  /// </summary>
  public Application Application { get; }

  /// <summary>
  /// Returns the parent object for the specified object.
  /// </summary>
  public object? Parent { get; }

}
