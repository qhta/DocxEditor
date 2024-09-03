namespace DocxControls;

/// <summary>
/// Interface for providing an enum property (i.e. property that value is as enum type).
/// Needs to be implemented by a class that provides data for <c>ComboBox</c>
/// </summary>
public interface IEnumProvider
{
  /// <summary>
  ///  Determines if the property type is an enum.
  /// </summary>
  public bool IsEnum { get; }

  /// <summary>
  ///  Determines if the property type is an enum treated as separate bits.
  /// </summary>
  public bool IsFlags { get; }

  ///// <summary>
  ///// Gets or sets the value of the property as integer.
  ///// </summary>
  //public int? AsInteger { get; set; }

  /// <summary>
  /// Selected enum value of the property.
  /// </summary>
  public object? SelectedEnum { get; set; }

  /// <summary>
  /// Enum values of the property.
  /// </summary>
  public IEnumerable<object> EnumValues { get; }
}
