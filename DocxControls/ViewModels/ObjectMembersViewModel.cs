using System.Collections.ObjectModel;
using System.Windows.Input;

using Qhta.MVVM;

namespace DocxControls;
/// <summary>
/// Collection of object members.
/// Creates a new members view model for the collection when the type and value of the member are entered by the user.
/// </summary>
public class ObjectMembersViewModel : ViewModel, INotifyCollectionChanged
{
  /// <summary>
  /// Default constructor.
  /// </summary>
  public ObjectMembersViewModel()
  {
    SelectItemCommand = new RelayCommand<ObjectMemberViewModel>(SelectItem);
  }

  /// <summary>
  /// Acceptable types of members.
  /// </summary>
  public IEnumerable<Type> MemberTypes { get; set; } = new Collection<Type>();


  /// <summary>
  /// Delegates <c>Add</c> member method to the internal collection.
  /// </summary>
  /// <param name="item"></param>
  public void Add(ObjectMemberViewModel item)
  {
    item.Collection = this;
    Items.Add(item);
  }

  /// <summary>
  /// Internal collection of object members.
  /// </summary>
  public ObservableCollection<ObjectMemberViewModel> Items { get; } = new();

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

  private void SelectItem(ObjectMemberViewModel? item)
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
  public ObjectMemberViewModel? SelectedItem
  {
    get => _selectedItem;
    set
    {
      _selectedItem = value;
      NotifyPropertyChanged(nameof(SelectedItem));
    }
  }
  private ObjectMemberViewModel? _selectedItem;
}
