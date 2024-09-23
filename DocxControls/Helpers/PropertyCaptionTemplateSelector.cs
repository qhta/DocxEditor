using System.Windows;
using System.Windows.Controls;
using Microsoft.VisualBasic.CompilerServices;

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
  /// Template for caption of existing compatibility setting.
  /// </summary>
  public DataTemplate? CompatibilitySettingTemplate { get; set; } = null!;

  /// <summary>
  /// Template for new compatibility setting caption.
  /// </summary>
  public DataTemplate? NewCompatibilitySettingTemplate { get; set; }

  /// <summary>
  /// Template selection logic.
  /// </summary>
  /// <param name="item"></param>
  /// <param name="container"></param>
  /// <returns></returns>
  public override DataTemplate SelectTemplate(object? item, DependencyObject container)
  {
    //Debug.WriteLine($"{this}.SelectTemplate({item}, {container}");
    //if (item?.ToString() == "{DataGrid.NewItemPlaceholder}")
    //{
    //  var dataGrid = VisualTreeHelperExt.FindAncestor<DataGrid>(owner);
    //  if ((dataGrid?.DataContext as PropertyViewModel)?.Owner?.ObjectMembers!=null)
    //    return NewMemberTypeTemplate ?? CaptionTemplate;
    //}
    if (item is VM.ObjectMemberViewModel objectMemberViewModel)
    {
      if (objectMemberViewModel.IsNew)
      {
        if (NewMemberTypeTemplate != null)
          return NewMemberTypeTemplate;
      }
      else
      {
        //if (objectMemberViewModel.ObjectType == typeof(DXW.CompatibilitySetting))
          return CaptionTemplate;
        //if (ObjectTemplate != null)
        //  return ObjectTemplate;
      }
    }
    if (item is VM.PropertyViewModel propertyViewModel)
    {
      Type type = propertyViewModel.Type!;
      if (type.IsClass && type != typeof(string))
        if (ObjectTemplate != null)
          return ObjectTemplate;
    }
    return CaptionTemplate;
  }
}