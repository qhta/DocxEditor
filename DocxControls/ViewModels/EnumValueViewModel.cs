using Qhta.MVVM;
using DocxControls.Helpers;

namespace DocxControls.ViewModels;

/// <summary>
/// Class for providing data for <c>ComboBox</c> items.
/// </summary>
public class EnumValueViewModel: ViewModel, IEnumValueModel
{
  /// <summary>
  /// Caption to display in the <c>ComboBox</c>
  /// </summary>
  public string? Caption { get; init; }

  /// <summary>
  /// Object value of the item.
  /// </summary>
  public object? Value { get; set; }

  /// <summary>
  /// Determines if the item is selected in the <c>ComboBox</c>
  /// </summary>
  public bool IsSelected { get; set; }

  /// <summary>
  /// Tooltip to display in the <c>ComboBox</c>
  /// </summary>
  public string? Tooltip { get; init; }

  /// <summary>
  /// Description to display in the <c>ComboBox</c> tooltip
  /// </summary>
  public string? Description { get; init; }


}