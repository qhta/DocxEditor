using System.Windows;
using System.Windows.Controls;

namespace DocxControls;

/// <summary>
/// Selects a <c>DataTemplate</c> for a caption of the property based on the <see cref="DocxControls.PropertyViewModel"/> property type
/// </summary>
public class PropertyCaptionTemplateSelector : DataTemplateSelector
{
  /// <summary>
  /// Template for all but object properties.
  /// </summary>
  public DataTemplate NormalTemplate { get; set; } = null!;

  /// <summary>
  /// Template for object properties.
  /// </summary>
  public DataTemplate ObjectTemplate { get; set; } = null!;

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
      Type type = propertyViewModel.Type!;
      if (type.IsClass && type != typeof(string))
        return ObjectTemplate;
      return NormalTemplate;
    }
    return NormalTemplate;
  }
}