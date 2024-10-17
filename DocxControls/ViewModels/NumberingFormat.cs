namespace DocxControls.ViewModels;

/// <summary>
/// Specifies the numbering format. It can be one of the predefined formats or a custom format.
/// </summary>
public class NumberingFormat: ElementViewModel<DXW.NumberingFormat>, DA.NumberingFormat
{
  /// <summary>
  /// Constructor with owner and modeled element.
  /// </summary>
  /// <param name="owner"></param>
  /// <param name="modeledElement"></param>
  public NumberingFormat(ElementViewModel owner, DXW.NumberingFormat? modeledElement) : base(owner, modeledElement)
  {
  }

  /// <summary>
  /// <para>Numbering Format Type</para>
  /// </summary>
  public DA.NumberFormat? Preset
  {
    get => EnumValuesConverter.Convert<DA.NumberFormat>(OpenXmlElement.Val?.Value);
    set
    {
      if (value == Preset) return;
      OpenXmlElement.Val = (value is not null) ?
        EnumValuesConverter.ConvertBack<DXW.NumberFormatValues>(value) : null;
      NotifyPropertyChanged(nameof(Preset));
    }
  }

  /// <summary>
  /// <para>Custom format, this property is only available in Office 2010 and later.</para>
  /// </summary>
  public string? Format
  {
    get => OpenXmlElement.Format?.Value;
    set
    {
      if (value == Format) return;
      OpenXmlElement.Format = (value is not null) ? new DX.StringValue(value) : null;
      NotifyPropertyChanged(nameof(Format));
    }
  }
}