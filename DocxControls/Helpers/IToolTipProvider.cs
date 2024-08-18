namespace DocxControls;

/// <summary>
/// Interface for providing a tooltip.
/// Needs to be implemented by a class that provides data for a <see cref="CustomToolTipView"/>
/// </summary>
public interface IToolTipProvider
{
  /// <summary>
  /// 
  /// </summary>
  public string? TooltipTitle { get; }

  /// <summary>
  /// Description displayed in the <see cref="CustomToolTipView"/>
  /// </summary>
  public string? TooltipDescription { get; }
}
