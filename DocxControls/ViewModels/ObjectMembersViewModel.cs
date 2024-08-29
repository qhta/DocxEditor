using System.Collections.ObjectModel;

using Qhta.MVVM;

namespace DocxControls;
/// <summary>
/// Collection of object members.
/// Creates a new member view model for the collection when the type and value of the member are entered by the user.
/// </summary>
public class ObjectMembersViewModel : ViewModel, INotifyCollectionChanged
{
  /// <summary>
  /// Default constructor.
  /// </summary>
  public ObjectMembersViewModel()
  {
  }

  /// <summary>
  /// Acceptable types of members.
  /// </summary>
  public IEnumerable<Type> MemberTypes { get; set; } = new Collection<Type>();

  /// <summary>
  /// New member type.
  /// </summary>
  public Type? NewMemberType
  {
    get => _newMemberType;
    set
    {
      if (value != _newMemberType)
      {
        _newMemberType = value;
        NotifyPropertyChanged(nameof(NewMemberType));
        TryAddOrChangeMember();
      }
    }
  }
  private Type? _newMemberType;

  /// <summary>
  /// New member value.
  /// </summary>
  public object? NewMemberValue
  {
    get => _newMemberValue;
    set
    {
      if (value != _newMemberValue)
      {
        _newMemberValue = value;
        NotifyPropertyChanged(nameof(NewMemberValue));
        TryAddOrChangeMember();
      }
    }
  }
  private object? _newMemberValue;

  /// <summary>
  /// New member instance.
  /// </summary>
  public ObjectMemberViewModel? NewMemberInstance
  {
    get => _newMemberInstance;
    set
    {
      if (value != _newMemberInstance)
      {
        _newMemberInstance = value;
        NotifyPropertyChanged(nameof(NewMemberInstance));
      }
    }
  }
  private ObjectMemberViewModel? _newMemberInstance;

  private bool TryAddOrChangeMember()
  {
    if (NewMemberType != null && NewMemberValue != null)
    {
      if (NewMemberInstance == null)
      {
        NewMemberInstance = new ObjectMemberViewModel { MemberType = NewMemberType, Value = NewMemberValue };
        AddNewItem();
      }
      else
      {
        ((ObjectMemberViewModel)NewMemberInstance).MemberType = NewMemberType;
        ((ObjectMemberViewModel)NewMemberInstance).Value = NewMemberValue;
      }
      return true;
    }
    return false;
  }

  private void AddNewItem()
  {
    if (NewMemberInstance != null)
    {
      Add(NewMemberInstance);
      NewMemberInstance = null;
      NewMemberType = null;
      NewMemberValue = null;
    }
  }


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
  public CustomObservableCollection<ObjectMemberViewModel> Items { get; } = new();

  /// <summary>
  /// Delegates <c>CollectionChanged</c> event to the internal collection.
  /// </summary>
  public event NotifyCollectionChangedEventHandler? CollectionChanged
  {
    add => Items.CollectionChanged += value;
    remove => Items.CollectionChanged -= value;
  }
}
