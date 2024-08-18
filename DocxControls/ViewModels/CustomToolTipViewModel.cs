using Qhta.MVVM;

namespace DocxControls;

/// <summary>
/// View model for a custom tooltip.
/// </summary>
public class CustomToolTipViewModel: ViewModel, IToolTipProvider
{
  /// <summary>
  /// Title of the tooltip.
  /// </summary>
  public string? TooltipTitle
  {
    get => _tooltip;
    set
    {
      if (value!= _tooltip)
      {
        _tooltip = value;
        NotifyPropertyChanged(nameof(TooltipTitle));
      }
    }
  }

  private string? _tooltip = "Title";

  /// <summary>
  /// Content of the tooltip.
  /// </summary>
  public string? TooltipDescription
  {
    get => _description;
    set
    {
      if (value != _description)
      {
        _description = value;
        NotifyPropertyChanged(nameof(TooltipDescription));
      }
    }
  }

  private string? _description = "Content";
}