using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

namespace DocxControls;

/// <summary>
/// Selects a <c>DataTemplate</c> for a property name (caption) based on the type of <see cref="PropertyViewModel"/> 
/// </summary>
public class PropertyNameTemplateSelector : DataTemplateSelector
{
  /// <summary>
  /// Template for caption display only.
  /// </summary>
  public DataTemplate CaptionTemplate { get; set; } = null!;

  /// <summary>
  /// Template for name edit.
  /// </summary>
  public DataTemplate NameTemplate { get; set; } = null!;

  /// <summary>
  /// Template selection logic.
  /// </summary>
  /// <param name="item"></param>
  /// <param name="container"></param>
  /// <returns></returns>
  public override DataTemplate SelectTemplate(object? item, DependencyObject container)
  {
    if (item is null)
    {
      Debug.WriteLine("item is null");
      return NameTemplate;
    }
    if (item is PropertyViewModel data)
    {
      Debug.WriteLine("item is PropertyViewModel");
      if (data.Name == null)
      {
        Debug.WriteLine("item Name == null");
        return NameTemplate;
      }
      if (data.IsCustomProperty)
      {
        Debug.WriteLine("item IsCustomProperty");
        return NameTemplate;
      }
    }
    return CaptionTemplate;
  }
}