using System.Windows.Input;

using Qhta.MVVM;

namespace DocxControls;
/// <summary>
/// Collection of object properties.
/// Creates a new properties view model for the collection when the type and value of the member are entered by the user.
/// </summary>
public class ObjectPropertiesViewModel : PropertiesViewModel
{
  /// <summary>
  /// Default constructor.
  /// </summary>
  public ObjectPropertiesViewModel()
  {
    SelectItemCommand = new RelayCommand<ObjectPropertyViewModel>(SelectItem);
  }

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
  /// Delegates <c>Remove</c> member method to the internal collection.
  /// </summary>
  /// <param name="propertyName"></param>
  public void Remove(string propertyName)
  {
    var item = Items.FirstOrDefault(i => i.Name == propertyName);
    if (item!=null)
      Items.Remove(item);
  }

  /// <summary>
  /// Internal collection of object members.
  /// </summary>
  public new CustomObservableCollection<ObjectPropertyViewModel> Items { get; } = new();

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
