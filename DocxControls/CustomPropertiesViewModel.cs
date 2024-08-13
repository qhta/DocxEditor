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
      Type type = typeof(string);
      object? value = null;
      try
      {
        type = CustomProperties.GetType(name);
        value = CustomProperties.GetValue(name);
      }
      catch (Exception e)
      {
        Debug.WriteLine(e);
      }
      var propertyViewModel = new CustomPropertyViewModel
      {
        Name = name,
        Type = type,
        Value = value,

      };
      propertyViewModel.PropertyChanged += PropertiesViewModel_PropertyChanged;
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
  public bool IsUniqueName(string name)
  {
    return CustomProperties.Elements<DXCP.CustomDocumentProperty>().All(item => item.Name != name);
  }

  private bool ValidateName(CustomPropertyViewModel viewModel)
  {
    var name = viewModel.Name;
    if (name == null)
      return false;
    if (name.Length == 0)
      return false;
    if (IsUniqueName(name))
    {
      viewModel.RemoveError(nameof(CustomPropertyViewModel.Name), Strings.NameMustBeUnique);
      return true;
    }
    else
    {
      viewModel.AddError(nameof(CustomPropertyViewModel.Name), Strings.NameMustBeUnique);
      return false;
    }
  }

  /// <summary>
  /// Initializes a new property name and type.
  /// </summary>
  /// <param name="viewModel"></param>
  public void Initialize(CustomPropertyViewModel viewModel)
  {
    var name0 = "New property";
    var name = name0;
    int i = 1;
    while (!IsUniqueName(name))
      name = name0 + (++i);
    viewModel.Name = name;
    viewModel.Type = typeof(string);
  }

  private void Properties_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
  {
    if (e.Action == NotifyCollectionChangedAction.Add)
    {
      foreach (CustomPropertyViewModel propertyViewModel in e.NewItems!)
      {
        if (propertyViewModel.IsEmpty)
          Initialize(propertyViewModel);
        if (propertyViewModel.Name != null &&
            propertyViewModel.Type != null && propertyViewModel.Validate() && ValidateName(propertyViewModel))
        {
          CustomProperties.Add(propertyViewModel.Name, propertyViewModel.Type, propertyViewModel.Value);
        }
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
      if (propertyViewModel.Name != null && propertyViewModel.Validate())
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
        if (propertyViewModel.Name != null && propertyViewModel.Type != null && propertyViewModel.Validate())
          CustomProperties.SetValue(propertyViewModel.Name!, propertyViewModel.Value);
      }
      else if (e.PropertyName == nameof(CustomPropertyViewModel.Name))
      {
        if (ValidateName(propertyViewModel))
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

  private readonly DXCP.Properties CustomProperties;

}
