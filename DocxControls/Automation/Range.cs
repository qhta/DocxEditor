namespace DocxControls.Automation;

/// <summary>
/// A range of elements.
/// </summary>
public interface Range
{
  /// <summary>
  /// Element that starts the range.
  /// </summary>
  public IElement Start { get; }

  /// <summary>
  /// Element that ends the range.
  /// </summary>
  public IElement End { get; }
}
