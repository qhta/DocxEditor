namespace DocxControls;

/// <summary>
/// Interface for providing a model view for an object.
/// Used in <see cref="PropertiesView"/>.
/// </summary>
public interface IObjectViewModelProvider
{
  /// <summary>
  ///  Determines if the property type is an object.
  /// </summary>
  public bool IsObject { get; }

  /// <summary>
  /// Provided view model for the object.
  /// </summary>
  public IObjectViewModel ObjectViewModel { get; }

}
