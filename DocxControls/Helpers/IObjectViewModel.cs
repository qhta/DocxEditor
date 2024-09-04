using System.Collections.ObjectModel;

namespace DocxControls;

/// <summary>
/// Interface of a view model for a complex object
/// </summary>
public interface IObjectViewModel
{
  /// <summary>
  ///  Type of the object which properties are modeled
  /// </summary>
  public Type? ObjectType { get; }


  /// <summary>
  /// Determines if the modeled object can contain members.
  /// </summary>
  public bool IsContainer { get; }

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
}
