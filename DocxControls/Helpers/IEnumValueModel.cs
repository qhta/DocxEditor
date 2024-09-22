namespace DocxControls.Helpers;

/// <summary>
/// Interface an object that provides data for <c>ComboBox</c> items.
/// </summary>
public interface IEnumValueModel
{
  /// <summary>
  /// Caption to display in the <c>ComboBox</c>
  /// </summary>
  public string? Caption { get; }

  /// <summary>
  /// Object value of the item.
  /// </summary>
  public object? Value { get; }

  /// <summary>
  /// Determines if the item is selected in the <c>ComboBox</c>
  /// </summary>
  public bool IsSelected { get; set; }

  /// <summary>
  /// Tooltip to display in the <c>ComboBox</c>
  /// </summary>
  public string? Tooltip { get; }

  /// <summary>
  /// Description to display in the <c>ComboBox</c> tooltip
  /// </summary>
  public string? Description { get; }
}