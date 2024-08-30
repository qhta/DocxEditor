namespace DocxControls;

/// <summary>
/// View model for an object member. Replaces <see cref="PropertyViewModel"/> in the properties view.
/// </summary>
public class ObjectMemberViewModel : ObjectViewModel
{
  /// <summary>
  /// Default constructor.
  /// </summary>
  public ObjectMemberViewModel() : base()
  {
  }

  /// <summary>
  /// Initializing
  /// </summary>
  /// <param name="container"></param>
  /// <param name="member"></param>
  public ObjectMemberViewModel(ObjectViewModel? container, object? member) : base(member)
  {
    Container = container;
  }

  /// <summary>
  ///  Type of the member object which properties are modeled
  /// </summary>
  public Type? MemberType

  {
    get => base.ObjectType;
    set
    {
      if (value != base.ObjectType && value != null)
      {
        base.ModeledObject = Activator.CreateInstance(value);
        NotifyPropertyChanged(nameof(ObjectType));
      }
    }
  }

  /// <summary>
  /// Gets or sets the container of the object member.
  /// </summary>
  public ObjectViewModel? Container { get; set; }

  /// <summary>
  /// Collection of object members.
  /// </summary>
  public ObjectMembersViewModel? Collection { get; internal set; }

  /// <summary>
  /// Value of the property.
  /// </summary>
  public override object? Value
  {
    get
    {
      if (IsNew)
        return null;
      return (ModeledObject as DX.OpenXmlElement)?.ToSystemValue(ObjectType) ?? ModeledObject!;
    }
    set
    {
      bool updated = false;
      if (ModeledObject is DX.OpenXmlElement element && Container != null)
      {
        updated = element.UpdateFromSystemValue(value);
        if (updated)
          NotifyPropertyChanged(nameof(Value));
      }
      if (!updated)
      {
        var val = value.ToOpenXmlValue(ObjectType);
        if (val != ModeledObject)
        {
          ModeledObject = val;
          NotifyPropertyChanged(nameof(Value));
        }
      }
      if (IsNew)
      {
        var newValue = ModeledObject;
        var newMember = new ObjectMemberViewModel(Container, newValue);
        Collection?.Add(newMember);

      }
    }
  }

  /// <summary>
  /// Checks if the object value is null.
  /// </summary>
  public bool IsEmpty => Value == null;
}