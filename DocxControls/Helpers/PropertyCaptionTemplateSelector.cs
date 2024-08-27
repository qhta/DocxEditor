using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

using Qhta.WPF.Utils;

namespace DocxControls;

/// <summary>
/// Selects a <c>DataTemplate</c> for a caption of the property based on the <see cref="DocxControls.PropertyViewModel"/> property type
/// </summary>
public class PropertyCaptionTemplateSelector : DataTemplateSelector
{
  /// <summary>
  /// Template for all but object properties.
  /// </summary>
  public DataTemplate CaptionTemplate { get; set; } = null!;

  /// <summary>
  /// Template for object properties.
  /// </summary>
  public DataTemplate? ObjectTemplate { get; set; } = null!;


  /// <summary>
  /// Template for object properties.
  /// </summary>
  public DataTemplate? NewMemberTemplate { get; set; }

  /// <summary>
  /// Template selection logic.
  /// </summary>
  /// <param name="item"></param>
  /// <param name="container"></param>
  /// <returns></returns>
  public override DataTemplate SelectTemplate(object? item, DependencyObject container)
  {
    Debug.WriteLine($"PropertyCaptionTemplateSelector.SelectTemplate({item}, {container})");
    if (item?.ToString() == "{DataGrid.NewItemPlaceholder}")
    {
      var dataGrid = VisualTreeHelperExt.FindAncestor<DataGrid>(container);
      if ((dataGrid?.DataContext as PropertyViewModel)?.ObjectViewModel?.ObjectMembers!=null)
        return NewMemberTemplate ?? CaptionTemplate;
    }
    if (item is ObjectMemberViewModel)
    {
      return ObjectTemplate ?? CaptionTemplate;
    }
    if (item is PropertyViewModel propertyViewModel)
    {
      Type type = propertyViewModel.Type!;
      if (type.IsClass && type != typeof(string))
        return ObjectTemplate ?? CaptionTemplate;
      return CaptionTemplate;
    }
    return CaptionTemplate;
  }
}