using System.Windows;
using System.Windows.Controls;

namespace DocxControls.Helpers;

/// <summary>
/// Selects a <c>DataTemplate</c> for a caption of the property based on the <see cref="VM.PropertyViewModel"/> property type
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
  /// Template for a new member type selector.
  /// </summary>
  public DataTemplate? NewMemberTypeTemplate { get; set; }

  /// <summary>
  /// Template selection logic.
  /// </summary>
  /// <param name="item"></param>
  /// <param name="container"></param>
  /// <returns></returns>
  public override DataTemplate SelectTemplate(object? item, DependencyObject container)
  {
    //if (item?.ToString() == "{DataGrid.NewItemPlaceholder}")
    //{
    //  var dataGrid = VisualTreeHelperExt.FindAncestor<DataGrid>(parent);
    //  if ((dataGrid?.DataContext as PropertyViewModel)?.Parent?.ObjectMembers!=null)
    //    return NewMemberTypeTemplate ?? CaptionTemplate;
    //}
    if (item is VM.ObjectMemberViewModel objectMemberViewModel)
    {
      return ((objectMemberViewModel.IsNew) ? NewMemberTypeTemplate : ObjectTemplate) ?? CaptionTemplate;
    }
    if (item is VM.PropertyViewModel propertyViewModel)
    {
      Type type = propertyViewModel.Type!;
      if (type.IsClass && type != typeof(string))
        return ObjectTemplate ?? CaptionTemplate;
      return CaptionTemplate;
    }
    return CaptionTemplate;
  }
}