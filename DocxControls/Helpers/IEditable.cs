namespace DocxControls.Helpers;

/// <summary>
/// View model that can be editable
/// </summary>
public interface IEditable
{
  /// <summary>
  /// Is the view model editable?
  /// </summary>
  public bool IsEditable { get;}

  /// <summary>
  /// Was the view model modified?
  /// </summary>
  public bool IsModified { get; set; }

  /// <summary>
  /// Is the view model modified internally? If so, do not set the IsModified property.
  /// </summary>
  public bool IsModifiedInternal { get; }
}
