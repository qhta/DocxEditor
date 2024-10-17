namespace Docx.Automation;

/// <summary>
/// interface for selectable elements
/// </summary>
public interface _Selectable
{
  /// <summary>
  /// Gets a value indicating whether the element is selected.
  /// </summary>
  public bool IsSelected { get; }

  /// <summary>
  /// Selects the element.
  /// </summary>
  public void Select();

  /// <summary>
  /// Selects the element and all its children.
  /// </summary>
  public void SelectAll();
}
