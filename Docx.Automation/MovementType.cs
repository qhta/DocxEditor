namespace Docx.Automation;

/// <summary>
/// Specifies the way the range or selection is moved.
/// </summary>
public enum MovementType
{
  /// <summary>
  /// The range is collapsed to an insertion point and moved to the end of the specified unit. Default.
  /// </summary>
  Move = 0,
  /// <summary>
  /// The end of the range is extended to the end of the specified unit.
  /// </summary>
  Extend = 1,
}
