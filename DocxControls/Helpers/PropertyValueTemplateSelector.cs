using System.Windows;
using System.Windows.Controls;

namespace DocxControls.Helpers;

/// <summary>
/// Selects a <c>DataTemplate</c> for a value of a property  based on the <see cref="VM.PropertyViewModel"/> property type
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
  /// Template for object properties.
  /// </summary>
  public DataTemplate? ObjectViewComboBoxTemplate { get; set; }

  ///// <summary>
  ///// Template for a new member value field.
  ///// </summary>
  //public DataTemplate? NewMemberValueTemplate { get; set; }

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
    //  if ((dataGrid?.DataContext as PropertyViewModel)?.Parent?.ObjectMembers != null)
    //    return NewMemberValueTemplate ?? TextTemplate;
    //}
    if (item is IEnumProvider enumProvider )
    {
      if (enumProvider.IsEnum)
      {
        if (enumProvider.IsFlags)
        {
          return FlagsComboBoxTemplate ?? ComboBoxTemplate ?? TextTemplate;
        }
        return ComboBoxTemplate ?? TextTemplate;
      }
    }
    if (item is IBooleanProvider booleanProvider && booleanProvider.IsBoolean)
    {
      return CheckBoxTemplate ?? TextTemplate;
    }
    if (item is IObjectViewModelProvider objectPropertiesProvider && objectPropertiesProvider.IsObject)
    {
      return ObjectViewComboBoxTemplate ?? TextTemplate;
    }
    return TextTemplate;
  }
}