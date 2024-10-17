namespace DocxControls.ViewModels;

/// <summary>
/// ViewModel for Wordprocessing Color
/// </summary>
public class WordColor: ElementViewModel<DXW.Color>, DA.WordColor
{
  /// <summary>
  /// Constructor with owner and modeled element
  /// </summary>
  /// <param name="owner"></param>
  /// <param name="modeledElement"></param>
  public WordColor(ViewModel owner, DXW.Color? modeledElement) : base(owner, modeledElement)
  {
  }

  /// <summary>
  /// Hexadecimal value of the color.
  /// </summary>
  public new string? Value
  {
    get => OpenXmlElement.Val?.Value;
    set
    {
      if (value == Value) return;
      OpenXmlElement.Val = (value != null) ? new DX.StringValue(value) : null;
      NotifyPropertyChanged(nameof(Value));
    }
  }

  /// <summary>
  /// Enumerated theme color
  /// </summary>
  public DA.ThemeColor? ThemeColor
  {
    get => EnumValuesConverter.Convert<DA.ThemeColor>(OpenXmlElement.ThemeColor?.Value);
    set
    {
      if (value == ThemeColor) return;
      OpenXmlElement.ThemeColor = (value != null) ? 
        EnumValuesConverter.ConvertBack<DXW.ThemeColorValues>(value) : null;
      NotifyPropertyChanged(nameof(ThemeColor));
    }
  }

  /// <summary>
  /// Tint modifier for theme color
  /// </summary>
  public string? ThemeTint
  {
    get => OpenXmlElement.ThemeTint?.Value;
    set
    {
      if (value == ThemeTint) return;
      OpenXmlElement.ThemeTint = (value != null) ? new DX.StringValue(value) : null;
      NotifyPropertyChanged(nameof(ThemeTint));
    }
  }

  /// <summary>
  /// Shade modifier for theme color
  /// </summary>
  public string? ThemeShade
  {
    get => OpenXmlElement.ThemeTint?.Value;
    set
    {
      if (value == ThemeShade) return;
      OpenXmlElement.ThemeShade = (value != null) ? new DX.StringValue(value) : null;
      NotifyPropertyChanged(nameof(ThemeShade));
    }
  }
}