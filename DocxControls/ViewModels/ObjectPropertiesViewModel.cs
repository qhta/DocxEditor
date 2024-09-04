using System.Windows.Input;

using Qhta.MVVM;

namespace DocxControls;
/// <summary>
/// Collection of object properties.
/// Creates a new properties view model for the collection when the type and value of the member are entered by the user.
/// </summary>
public class ObjectPropertiesViewModel : ViewModel, INotifyCollectionChanged
{
  /// <summary>
  /// Default constructor.
  /// </summary>
  public ObjectPropertiesViewModel()
  {
    SelectItemCommand = new RelayCommand<ObjectPropertyViewModel>(SelectItem);
  }


  ///// <summary>
  ///// New member type.
  ///// </summary>
  //public Type? NewPropertyType
  //{
  //  get => _newPropertyType;
  //  set
  //  {
  //    if (value != _newPropertyType)
  //    {
  //      _newPropertyType = value;
  //      NotifyPropertyChanged(nameof(NewPropertyType));
  //      TryAddOrChangeProperty();
  //    }
  //  }
  //}
  //private Type? _newPropertyType;

  ///// <summary>
  ///// New member value.
  ///// </summary>
  //public object? NewPropertyValue
  //{
  //  get => _newPropertyValue;
  //  set
  //  {
  //    if (value != _newPropertyValue)
  //    {
  //      _newPropertyValue = value;
  //      NotifyPropertyChanged(nameof(NewPropertyValue));
  //      TryAddOrChangeProperty();
  //    }
  //  }
  //}
  //private object? _newPropertyValue;

  ///// <summary>
  ///// New member instance.
  ///// </summary>
  //public ObjectPropertyViewModel? NewPropertyInstance
  //{
  //  get => _newPropertyInstance;
  //  set
  //  {
  //    if (value != _newPropertyInstance)
  //    {
  //      _newPropertyInstance = value;
  //      NotifyPropertyChanged(nameof(NewPropertyInstance));
  //    }
  //  }
  //}
  //private ObjectPropertyViewModel? _newPropertyInstance = new ObjectPropertyViewModel();

  //private bool TryAddOrChangeProperty()
  //{
  //  if (NewPropertyType != null)
  //  {
  //    if (NewPropertyInstance == null)
  //    {
  //      NewPropertyInstance = new ObjectPropertyViewModel { PropertyType = NewPropertyType, Value = NewPropertyValue };
  //      AddNewItem();
  //    }
  //    else
  //    {
  //      ((ObjectPropertyViewModel)NewPropertyInstance).PropertyType = NewPropertyType;
  //      ((ObjectPropertyViewModel)NewPropertyInstance).Value = NewPropertyValue;
  //    }
  //    return true;
  //  }
  //  return false;
  //}

  //private void AddNewItem()
  //{
  //  if (NewPropertyInstance != null)
  //  {
  //    Add(NewPropertyInstance);
  //    NewPropertyInstance = null;
  //    NewPropertyType = null;
  //    NewPropertyValue = null;
  //  }
  //}


  /// <summary>
  /// Delegates <c>Add</c> member method to the internal collection.
  /// </summary>
  /// <param name="item"></param>
  public void Add(ObjectPropertyViewModel item)
  {
    item.Collection = this;
    Items.Add(item);
  }

  /// <summary>
  /// Internal collection of object members.
  /// </summary>
  public CustomObservableCollection<ObjectPropertyViewModel> Items { get; } = new();

  /// <summary>
  /// Delegates <c>CollectionChanged</c> event to the internal collection.
  /// </summary>
  public event NotifyCollectionChangedEventHandler? CollectionChanged
  {
    add => Items.CollectionChanged += value;
    remove => Items.CollectionChanged -= value;
  }

  /// <summary>
  /// Command to select an item.
  /// </summary>
  public ICommand SelectItemCommand { get; }

  private void SelectItem(ObjectPropertyViewModel? item)
  {
    if (item == null)
      return;
    if (SelectedItem!=null)
      SelectedItem.IsSelected = false;
    SelectedItem = item;
    SelectedItem.IsSelected = true;
  }

  /// <summary>
  /// Selected item.
  /// </summary>
  public ObjectPropertyViewModel? SelectedItem
  {
    get => _selectedItem;
    set
    {
      _selectedItem = value;
      NotifyPropertyChanged(nameof(SelectedItem));
    }
  }
  private ObjectPropertyViewModel? _selectedItem;
}
