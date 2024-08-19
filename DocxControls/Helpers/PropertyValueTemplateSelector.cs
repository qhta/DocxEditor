using System.Windows;
using System.Windows.Controls;

namespace DocxControls;

/// <summary>
/// Selects a <c>DataTemplate</c> for a value of a property  based on the <see cref="DocxControls.PropertyViewModel"/> property type
/// </summary>
public class PropertyValueTemplateSelector : DataTemplateSelector
{
  /// <summary>
  /// Template for string properties.
  /// </summary>
  public DataTemplate TextTemplate { get; set; } = null!;

  /// <summary>
  /// Template for boolean properties.
  /// </summary>
  public DataTemplate? CheckBoxTemplate { get; set; }

  /// <summary>
  /// Template for enum properties.
  /// </summary>
  public DataTemplate? ComboBoxTemplate { get; set; }

  /// <summary>
  /// Template for enum flags properties.
  /// </summary>
  public DataTemplate? FlagsComboBoxTemplate { get; set; }

  /// <summary>
  /// Template selection logic.
  /// </summary>
  /// <param name="item"></param>
  /// <param name="container"></param>
  /// <returns></returns>
  public override DataTemplate SelectTemplate(object? item, DependencyObject container)
  {
    if (item is IEnumProvider enumProvider && enumProvider.IsEnum)
    {
      if (enumProvider.IsFlags)
      {
        return FlagsComboBoxTemplate ?? ComboBoxTemplate ?? TextTemplate;
      }
      return ComboBoxTemplate ?? TextTemplate;
    }
    if (item is IBooleanProvider booleanProvider && booleanProvider.IsBoolean)
    {
      return CheckBoxTemplate ?? TextTemplate;
    }
    return TextTemplate;
  }
}