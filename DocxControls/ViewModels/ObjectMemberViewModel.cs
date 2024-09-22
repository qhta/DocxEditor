namespace DocxControls.ViewModels;

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
  /// <param name="owner"></param>
  /// <param name="member"></param>
  public ObjectMemberViewModel(ObjectViewModel owner, object member) : base(owner, member)
  {
  }

  /// <summary>
  ///  ValueType of the member object which properties are modeled
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
      if (ModeledObject is DX.OpenXmlElement element && Owner != null)
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
        if (newValue == null)
          throw new InvalidOperationException("New value cannot be null");
        if (!(Owner is ObjectViewModel objectViewModel))
          throw new InvalidOperationException("Owner cannot be null");
        var newMember = new ObjectMemberViewModel(objectViewModel, newValue);
        Collection?.Add(newMember);

      }
    }
  }

  /// <summary>
  /// Checks if the object value is null.
  /// </summary>
  public new bool IsEmpty => Value == null;

  /// <summary>
  /// Allowed types for the member object.
  /// </summary>
  public IEnumerable<Type>? AllowedMemberTypes => Collection?.AllowedMemberTypes;
}