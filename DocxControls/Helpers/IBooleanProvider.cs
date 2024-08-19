namespace DocxControls;

/// <summary>
/// Interface for providing a boolean.
/// Needs to be implemented by a class that provides data for <c>CheckBox</c>
/// </summary>
public interface IBooleanProvider
{
  /// <summary>
  ///  Determines if the property is a boolean.
  /// </summary>
  public bool IsBoolean { get; }

  /// <summary>
  /// Determines if the property is a boolean with three states.
  /// </summary>
  public bool IsThreeState { get; }

  /// <summary>
  /// Gets or sets the value of the property as a boolean.
  /// </summary>
  public Boolean? AsBoolean { get; set; }
}
