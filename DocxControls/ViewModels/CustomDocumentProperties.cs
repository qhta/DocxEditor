using System.ComponentModel;

using Docx.Automation;

namespace DocxControls;

/// <summary>
/// View model for the custom properties
/// </summary>
public class CustomDocumentProperties: PropertiesViewModel<CustomDocumentProperty>//, DA.CustomDocumentProperties, ICollection<CustomDocumentProperty>
{
  /// <summary>
  /// Initializing constructor.
  /// </summary>
  /// <param name="parent"></param>
  public CustomDocumentProperties(Document parent): base(parent)
  {
    IsModifiedInternal = true;
    WordDocument = parent.WordDocument;
    CustomPropertiesElement = WordDocument.GetCustomFileProperties();
    var names = CustomPropertiesElement.GetNames();
    foreach (var name in names)
    {
      Type type = typeof(string);
      object? value = null;
      try
      {
        type = CustomPropertiesElement.GetType(name);
        value = CustomPropertiesElement.GetValue(name);
      }
      catch (Exception e)
      {
        Debug.WriteLine(e);
      }
      var propertyViewModel = new CustomDocumentProperty(this)
      {
        Name = name,
        Type = type,
        Value = value,

      };
      propertyViewModel.PropertyChanged += PropertiesViewModel_PropertyChanged;
      Items.Add(propertyViewModel);

    }
    Items.CollectionChanged += ItemsCollectionChanged;
    IsModifiedInternal = false;
  }

  /// <summary>
  /// Checks if the name does not exist in the properties.
  /// </summary>
  /// <param name="name"></param>
  /// <returns></returns>
  public bool IsUniqueName(string name)
  {
    return CustomPropertiesElement.Elements<DXCP.CustomDocumentProperty>().All(item => item.Name != name);
  }

  private bool ValidateName(CustomDocumentProperty viewModel)
  {
    var name = viewModel.Name;
    if (name == null)
      return false;
    if (name.Length == 0)
      return false;
    if (IsUniqueName(name))
    {
      viewModel.RemoveError(nameof(CustomDocumentProperty.Name), Strings.NameMustBeUnique);
      return true;
    }
    else
    {
      viewModel.AddError(nameof(CustomDocumentProperty.Name), Strings.NameMustBeUnique);
      return false;
    }
  }

  /// <summary>
  /// Initializes a new property name and type.
  /// </summary>
  /// <param name="viewModel"></param>
  public void Initialize(CustomDocumentProperty viewModel)
  {
    var name0 = "New property";
    var name = name0;
    int i = 1;
    while (!IsUniqueName(name))
      name = name0 + (++i);
    viewModel.Name = name;
    viewModel.Type = typeof(string);
  }

  private void ItemsCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
  {
    if (e.Action == NotifyCollectionChangedAction.Add)
    {
      foreach (CustomDocumentProperty propertyViewModel in e.NewItems!)
      {
        if (propertyViewModel.IsEmpty)
          Initialize(propertyViewModel);
        if (propertyViewModel.Name != null &&
            propertyViewModel.Type != null && propertyViewModel.Validate() && ValidateName(propertyViewModel))
        {
          CustomPropertiesElement.Add(propertyViewModel.Name, propertyViewModel.Type, propertyViewModel.Value);
        }
        propertyViewModel.PropertyChanged += PropertiesViewModel_PropertyChanged;
      }
      IsModified = true;
    }
    else if (e.Action == NotifyCollectionChangedAction.Remove)
    {
      foreach (CustomDocumentProperty propertyViewModel in e.OldItems!)
      {
        if (propertyViewModel.Name != null)
          CustomPropertiesElement.Remove(propertyViewModel.Name);
      }
      IsModified = true;
    }
    else if (e.Action == NotifyCollectionChangedAction.Reset)
    {
      // do nothing
      //CustomPropertiesElement.Clear();
    }
    else if (e.Action == NotifyCollectionChangedAction.Replace)
    {
      var propertyViewModel = (CustomDocumentProperty)e.NewItems![0]!;
      if (propertyViewModel.Name != null && propertyViewModel.Validate())
        CustomPropertiesElement.SetValue(propertyViewModel.Name, propertyViewModel.Value);
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
    if (sender is CustomDocumentProperty propertyViewModel)
    {
      if (e.PropertyName == nameof(CustomDocumentProperty.Value))
      {
        if (propertyViewModel.Name != null && propertyViewModel.Type != null && propertyViewModel.Validate())
          CustomPropertiesElement.SetValue(propertyViewModel.Name!, propertyViewModel.Value);
      }
      else if (e.PropertyName == nameof(CustomDocumentProperty.Name))
      {
        if (ValidateName(propertyViewModel))
        {
          if (propertyViewModel.Name != null)
          {
            if (!CustomPropertiesElement.Rename(propertyViewModel.Name!, propertyViewModel.Name))
            {
              if (propertyViewModel.Type != null)
                CustomPropertiesElement.Add(propertyViewModel.Name, propertyViewModel.Type);
            }
          }
        }
      }
      else if (e.PropertyName == nameof(CustomDocumentProperty.Type))
      {
        if (propertyViewModel.Type != null)
        {
          var name = propertyViewModel.Name;
          if (!string.IsNullOrEmpty(name))
          {
            var type = propertyViewModel.Type;
            if (!CustomPropertiesElement.ChangeType(name, type))
            {
              if (propertyViewModel.Name != null)
                CustomPropertiesElement.Add(name, type);
            }
            CustomPropertiesElement.SetValue(name, new PropertyValueConverter().ConvertBack(propertyViewModel.Value, type, null, CultureInfo.CurrentCulture));
          }
        }
      }
    }
  }

  private readonly DXCP.Properties CustomPropertiesElement;

  /// <summary>
  /// Enumerates the custom properties.
  /// </summary>
  /// <returns></returns>
  // ReSharper disable once NotDisposedResourceIsReturned
  public IEnumerator<CustomDocumentProperty> GetEnumerator() => Items.Cast<CustomDocumentProperty>().GetEnumerator();


  //IEnumerator<DA.CustomDocumentProperty> IEnumerable<DA.CustomDocumentProperty>.GetEnumerator() => Items.Cast<DA.CustomDocumentProperty>().GetEnumerator();

  /// <summary>
  /// Adds a custom property to the collection.
  /// </summary>
  /// <param name="name">The string of the Name of the property.</param>
  /// <param name="linkToContent">Specifies whether the LinkToContent property is linked to the contents of the container document.
  /// If this argument is True, the LinkSource argument is required; if it's False, the Value argument is required.</param>
  /// <param name="type">The data type of the Type property.
  /// Can be one of the following: Boolean, Date, Float, Number, or String.</param>
  /// <param name="value"></param>
  /// <param name="linkSource"></param>
  public void Add(string name, bool linkToContent, PropertyType type, object? value, string? linkSource)
  {
    var propertyViewModel = new CustomDocumentProperty(this)
    {
      Name = name,
      Type = TypeMap[type],
      Value = value,
      LinkToContent = linkToContent,
    };
    Items.Add(propertyViewModel);
  }

  /// <summary>
  /// Adds a custom property to the collection.
  /// </summary>
  /// <param name="item"></param>
  public void Add(CustomDocumentProperty item)
  {
    Items.Add(item);
  }

  //public new void Clear() => Items.Clear();


  //public bool Contains(CustomDocumentProperty item) => Items.Contains(item);


  //public void CopyTo(CustomDocumentProperty[] array, int arrayIndex)
  //{
  //  throw new NotImplementedException();
  //}

  //public bool Remove(CustomDocumentProperty item)
  //{
  //  throw new NotImplementedException();
  //}

  //public bool IsReadOnly { get; }

  private static readonly Dictionary<PropertyType, Type> TypeMap = new()
  {
    { PropertyType.Boolean, typeof(bool) },
    { PropertyType.Date, typeof(DateTime) },
    { PropertyType.Float, typeof(float) },
    { PropertyType.Number, typeof(int) },
    { PropertyType.String, typeof(string) },
  };
}
