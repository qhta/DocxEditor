  namespace DocxControls.ViewModels;

/// <summary>
/// Specifies the properties for footnotes in the section.
/// </summary>
public class FootnoteProperties: ElementViewModel<DXW.FootnoteProperties>, DA.FootnoteProperties
{
  /// <summary>
  /// Constructor with owner and modeled element.
  /// </summary>
  /// <param name="owner"></param>
  /// <param name="modeledElement"></param>
  public FootnoteProperties(ElementViewModel owner, DXW.FootnoteProperties? modeledElement) : base(owner, modeledElement)
  {
  }

  /// <summary>
  /// Specifies position for footnotes in the section.
  /// </summary>
  public DA.FootnotePosition? FootnotePosition 
  { 
    get => EnumValuesConverter.Convert<DA.FootnotePosition>(OpenXmlElement.GetFootnotePosition());
    set
    {
      if (value == FootnotePosition) return;
      OpenXmlElement.SetFootnotePosition(EnumValuesConverter.ConvertBack<DXW.FootnotePositionValues>(value)); NotifyPropertyChanged(nameof(FootnotePosition));
    }
  }

  DA.NumberingFormat? DA.FootnoteProperties.NumberingFormat
  {
    get => NumberingFormat;
    set => NumberingFormat = (NumberingFormat?)value;
  }

  /// <summary>
  /// <para>Footnote Numbering Format.</para>
  /// </summary>
  public NumberingFormat? NumberingFormat
  {
    get
    {
      var numberingFormatElement = OpenXmlElement.GetNumberingFormat();
      if (numberingFormatElement == null)
        return null;
      return new NumberingFormat(this, numberingFormatElement);
    }
    set
    {
      if (value == NumberingFormat) return;
      OpenXmlElement.SetNumberingFormat(value!.OpenXmlElement);
      NotifyPropertyChanged(nameof(NumberingFormat));
    }
  }

  /// <summary>
  /// Specifies the starting value used for the first footnote whenever the numbering is restarted.
  /// </summary>
  public int? NumberingStart
  {
    get => OpenXmlElement.GetNumberingStart();
    set
    { 
      if (value == NumberingStart) return;
      OpenXmlElement.SetNumberingStart(value);
      NotifyPropertyChanged(nameof(NumberingStart));
    }
  }

  /// <summary>
  /// Specifies when the footnote numbering in this section shall be reset to the footnote number specified by the start attribute's value.
  /// </summary>
  public DA.RestartNumber? NumberingRestart
  {
    get => EnumValuesConverter.Convert<DA.RestartNumber>(OpenXmlElement.GetNumberingRestart());
    set
    {
      if (value == NumberingRestart) return;
      OpenXmlElement.SetNumberingRestart(EnumValuesConverter.ConvertBack<DXW.RestartNumberValues>(value));
      NotifyPropertyChanged(nameof(FootnotePosition));
    }
  }
}