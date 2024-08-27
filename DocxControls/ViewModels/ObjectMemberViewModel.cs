﻿using System.Collections.ObjectModel;

namespace DocxControls;

/// <summary>
/// View model for an object member. Replaces <see cref="PropertyViewModel"/> in the properties view.
/// </summary>
public class ObjectMemberViewModel : ObjectViewModel
{
  /// <summary>
  /// Default constructor.
  /// </summary>
  public ObjectMemberViewModel() : base(typeof(object), null)
  {
  }

  /// <summary>
  /// Initializing
  /// </summary>
  /// <param name="container"></param>
  /// <param name="memberType"></param>
  /// <param name="member"></param>
  public ObjectMemberViewModel(ObjectViewModel? container, Type memberType, object? member) : base(memberType, member)
  {
    Container = container;
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
  /// Acceptable types of members.
  /// </summary>
  public IEnumerable<Type>? MemberTypes => Collection?.MemberTypes;

  /// <summary>
  /// Value of the property.
  /// </summary>
  public override object? Value
  {
    get => (ModeledObject as DX.OpenXmlElement)?.ToSystemValue(ObjectType) ?? ModeledObject!;
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
    }
  }
}