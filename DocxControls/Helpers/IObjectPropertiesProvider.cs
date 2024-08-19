using Qhta.MVVM;

namespace DocxControls;

/// <summary>
/// Interface for providing a value as an object.
/// Used in <see cref="PropertiesView"/>.
/// </summary>
public interface IObjectPropertiesProvider
{
  /// <summary>
  ///  Determines if the property type is an object.
  /// </summary>
  public bool IsObject { get; }

  /// <summary>
  /// Gets the value as an object properties view model.
  /// </summary>
  public ObjectPropertiesViewModel? ObjectProperties { get; }

}
