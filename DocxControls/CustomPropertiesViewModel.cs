using System.Collections.ObjectModel;
using System.ComponentModel;

using DocumentFormat.OpenXml.Packaging;

namespace DocxControls;

/// <summary>
/// Defined to allow the collection to be refreshed.
/// </summary>
/// <typeparam name="T"></typeparam>
public class CustomObservableCollection<T> : ObservableCollection<T>
{
  /// <summary>
  /// Raises the CollectionChanged event with the provided arguments.
  /// </summary>
  public void Refresh()
  {
    OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
  }
}

/// <summary>
/// View model for the custom properties
/// </summary>
public class CustomPropertiesViewModel
{

  /// <summary>
  /// Internal Wordprocessing document
  /// </summary>
  public WordprocessingDocument WordDocument { get; init; }

  /// <summary>
  /// Observable collection of properties
  /// </summary>
  public CustomObservableCollection<CustomPropertyViewModel> Properties { get; } = new();

  /// <summary>
  /// Default constructor
  /// </summary>
  public CustomPropertiesViewModel()
  {
    WordDocument = null!;
    CustomProperties = null!;
  }

  /// <summary>
  /// Gets the count of the custom properties in the document.
  /// </summary>
  public int Count => WordDocument.GetCustomFileProperties().Elements().Count();

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
      var propertyViewModel = new CustomPropertyViewModel
      {
        Name = name,
        Type = CustomProperties.GetType(name),
        Value = CustomProperties.GetValue(name),

      };
      propertyViewModel.PropertyChanged += PropertiesViewModel_PropertyChanged;
      Debug.WriteLine("Property added");
      Properties.Add(propertyViewModel);
    }
    Properties.Refresh();
    Properties.CollectionChanged += Properties_CollectionChanged;
  }

  /// <summary>
  /// Checks if the name does not exist in the properties.
  /// </summary>
  /// <param name="name"></param>
  /// <returns></returns>
  public bool IsValidName(string name)
  {
    return CustomProperties.Elements<DXCP.CustomDocumentProperty>().All(item => item.Name != name);
  }

  ///// <summary>
  ///// Collection view for the properties
  ///// </summary>
  //public CollectionView PropertiesCollectionView
  //{
  //  get
  //  {
  //    if (_propertiesCollectionView == null)
  //    {
  //      _propertiesCollectionView = (CollectionView)CollectionViewSource.GetDefaultView(Properties);
  //      (_propertiesCollectionView as IEditableCollectionView)!.NewItemPlaceholderPosition = NewItemPlaceholderPosition.AtEnd;
  //    }
  //    return _propertiesCollectionView;
  //  }
  //}
  //private CollectionView? _propertiesCollectionView;

  private void Properties_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
  {
    Debug.WriteLine($"Properties collection changed {e.Action}");
    if (e.Action == NotifyCollectionChangedAction.Add)
    {
      foreach (CustomPropertyViewModel propertyViewModel in e.NewItems!)
      {
        if (propertyViewModel.Name != null &&
            propertyViewModel.Type != null)
          CustomProperties.Add(propertyViewModel.Name, propertyViewModel.Type, propertyViewModel.Value);
        propertyViewModel.PropertyChanged += PropertiesViewModel_PropertyChanged;
      }
    }
    else if (e.Action == NotifyCollectionChangedAction.Remove)
    {
      foreach (CustomPropertyViewModel propertyViewModel in e.OldItems!)
      {
        if (propertyViewModel.Name != null)
          CustomProperties.Remove(propertyViewModel.Name);
      }
    }
    else if (e.Action == NotifyCollectionChangedAction.Reset)
    {
      // do nothing
      //CustomProperties.Clear();
    }
    else if (e.Action == NotifyCollectionChangedAction.Replace)
    {
      var propertyViewModel = (CustomPropertyViewModel)e.NewItems![0]!;
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
    if (sender is CustomPropertyViewModel propertyViewModel)
    {
      if (e.PropertyName == nameof(CustomPropertyViewModel.Value))
      {
        CustomProperties.SetValue(propertyViewModel.Name!, propertyViewModel.Value);
      }
      else if (e.PropertyName == nameof(CustomPropertyViewModel.Name))
      {
        if (propertyViewModel.Name != null)
        {
          if (!CustomProperties.Rename(propertyViewModel.Name!, propertyViewModel.Name))
          {
            if (propertyViewModel.Type != null)
              CustomProperties.Add(propertyViewModel.Name, propertyViewModel.Type);
          }
        }
      }
      else if (e.PropertyName == nameof(CustomPropertyViewModel.Type))
      {
        if (propertyViewModel.Type != null)
        {
          var name = propertyViewModel.Name;
          if (!string.IsNullOrEmpty(name))
          {
            var type = propertyViewModel.Type;
            if (!CustomProperties.ChangeType(name, type))
            {
              if (propertyViewModel.Name != null)
                CustomProperties.Add(name, type);
            }
            CustomProperties.SetValue(name, new PropertyValueConverter().ConvertBack(propertyViewModel.Value, type, null, CultureInfo.CurrentCulture));
          }
        }
      }
    }
  }

  private readonly DocumentFormat.OpenXml.CustomProperties.Properties CustomProperties;

}
