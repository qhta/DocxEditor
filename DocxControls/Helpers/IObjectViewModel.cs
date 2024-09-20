namespace DocxControls;

/// <summary>
/// Interface of a view model for a complex object
/// </summary>
public interface IObjectViewModel
{
  /// <summary>
  ///  ValueType of the object which properties are modeled
  /// </summary>
  public Type? ObjectType { get; }


  /// <summary>
  /// Determines if the modeled object can contain members.
  /// </summary>
  public bool IsContainer { get; }

  /// <summary>
  /// Determines if the object is just created.
  /// </summary>
  public bool IsNew { get; }

  /// <summary>
  /// Determines if the object is empty.
  /// </summary>
  public bool IsEmpty { get; }

  /// <summary>
  /// Object which properties are modeled
  /// </summary>
  public object? ModeledObject { get; }

  /// <summary>
  /// RunProperties of the object.
  /// </summary>
  public ObjectPropertiesViewModel ObjectProperties {get; }

  /// <summary>
  /// Members of the object.
  /// </summary>
  public ObjectMembersViewModel? ObjectMembers { get; }

  /// <summary>
  /// Width of the data grid in the view
  /// </summary>
  public double DataGridWidth { get; set; }
}
