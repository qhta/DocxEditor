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
  public DataTemplate CheckBoxTemplate { get; set; } = null!;

  /// <summary>
  /// Template selection logic.
  /// </summary>
  /// <param name="item"></param>
  /// <param name="container"></param>
  /// <returns></returns>
  public override DataTemplate SelectTemplate(object? item, DependencyObject container)
  {
    if (item is PropertyViewModel propertyViewModel)
    {
      if (propertyViewModel.Type == typeof(bool))
        return CheckBoxTemplate;
      return TextTemplate;
    }
    return TextTemplate;
  }
}