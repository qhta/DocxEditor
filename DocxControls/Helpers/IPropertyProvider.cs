namespace DocxControls;

/// <summary>
/// Interface for providing a property info to display in a <c>DataGrid</c>
/// </summary>
public interface IPropertyProvider
{
  /// <summary>
  /// Is the property read-only?
  /// </summary>
  public bool IsReadOnly{ get; }

  /// <summary>
  /// Value of the property
  /// </summary>
  public object? Value { get; }

  /// <summary>
  /// Is the property obsolete?
  /// </summary>
  public bool IsObsolete { get; }
}
