using System.Windows;
using System.Windows.Controls;

namespace DocxControls;

/// <summary>
/// Selects a DataTemplate based on the <see cref="PropertyViewModel"/> type
/// </summary>
public class PropertyViewTemplateSelector : DataTemplateSelector
{
  /// <summary>
  /// Template for string properties.
  /// </summary>
  public DataTemplate TextTemplate { get; set; } = null!;
  /// <summary>
  /// Template for boolean properties.
  /// </summary>
  public DataTemplate CheckBoxTemplate { get; set; } = null!;
  //public DataTemplate ComboBoxTemplate { get; set; }

  public override DataTemplate SelectTemplate(object? item, DependencyObject container)
  {
    // Example logic to choose the template based on the item
    if (item is PropertyViewModel data)
    {
      if (data.PropType == typeof(bool))
        return CheckBoxTemplate;
      return TextTemplate;
    }
    return TextTemplate;
  }
}