namespace DocxControls;

/// <summary>
/// Interface for providing a tooltip.
/// Needs to be implemented by a class that provides data for a <see cref="CustomToolTipView"/>
/// </summary>
public interface IToolTipProvider
{
  /// <summary>
  /// Should the <see cref="CustomToolTipView"/> be displayed?
  /// </summary>
  public bool HasTooltip{ get; }

  /// <summary>
  /// Title to display in the <see cref="CustomToolTipView"/>
  /// </summary>
  public string? TooltipTitle { get; }

  /// <summary>
  /// Description to display in the <see cref="CustomToolTipView"/>
  /// </summary>
  public string? TooltipDescription { get; }
}
