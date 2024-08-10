using System.Collections.Specialized;
using System.ComponentModel;

using DocumentFormat.OpenXml.Packaging;

using Qhta.OpenXmlTools;

namespace DocxControls;

/// <summary>
/// View model for the custom properties
/// </summary>
public class CustomPropertiesViewModel : PropertiesViewModel
{
  /// <summary>
  /// Initializes a new instance of the <see cref="CustomPropertiesViewModel"/> class.
  /// </summary>
  /// <param name="wordDocument"></param>
  public CustomPropertiesViewModel(WordprocessingDocument wordDocument)
  {
    WordDocument = wordDocument;
    CustomProperties = wordDocument.GetCustomFileProperties();
    var names = CustomProperties.GetNames();
    foreach (var name in names)
    {
      var propertyViewModel = new PropertyViewModel
      {
        Caption = name,
        Name = name,
        IsCustomProperty = true,
        PropType = typeof(string),
        Value = CustomProperties.GetValue(name),

      };
      propertyViewModel.PropertyChanged += PropertiesViewModel_PropertyChanged;
      Properties.Add(propertyViewModel);
    }
    CanAddAndDeleteProperties = true;
    Properties.CollectionChanged += Properties_CollectionChanged;
  }

  private void Properties_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
  {
    if (e.Action == NotifyCollectionChangedAction.Add)
    {
      foreach (PropertyViewModel propertyViewModel in e.NewItems!)
      {
        if (propertyViewModel.Name != null &&
            propertyViewModel.PropType != null)
          CustomProperties.Add(propertyViewModel.Name, propertyViewModel.PropType, propertyViewModel.Value);
      }
    }
    else if (e.Action == NotifyCollectionChangedAction.Remove)
    {
      foreach (PropertyViewModel propertyViewModel in e.OldItems!)
      {
        if (propertyViewModel.Name != null)
          CustomProperties.Remove(propertyViewModel.Name);
      }
    }
    else if (e.Action == NotifyCollectionChangedAction.Reset)
    {
      CustomProperties.Clear();
    }
    else if (e.Action == NotifyCollectionChangedAction.Replace)
    {
      var propertyViewModel = (PropertyViewModel)e.NewItems![0]!;
      if (propertyViewModel.Name != null)
        CustomProperties.SetValue(propertyViewModel.Name, propertyViewModel.Value);
    }
    else if (e.Action == NotifyCollectionChangedAction.Move)
    {
      // do nothing
    }
    else
    {
      throw new NotImplementedException();
    }
  }

  private void PropertiesViewModel_PropertyChanged(object? sender, PropertyChangedEventArgs e)
  {
    var propertyViewModel = (PropertyViewModel)sender!;
    var propertyName = e.PropertyName;
    if (propertyName != null)
      CustomProperties.SetValue(propertyName, propertyViewModel.Value);
  }


  private readonly DocumentFormat.OpenXml.CustomProperties.Properties CustomProperties;

}
