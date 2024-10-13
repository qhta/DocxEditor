
namespace DocxControls.ViewModels;

/// <summary>
/// Specifies the properties for endnotes in the section.
/// </summary>
public class EndnoteProperties: ElementViewModel, DA.EndnoteProperties
{
  /// <summary>
  /// Constructor with owner and modeled element.
  /// </summary>
  /// <param name="owner"></param>
  /// <param name="modeledElement"></param>
  public EndnoteProperties(ElementViewModel owner, DXW.EndnoteProperties modeledElement) : base(owner, modeledElement)
  {
  }

  internal DXW.EndnoteProperties OpenXmlElement => (DXW.EndnoteProperties)ModeledElement!;

  /// <summary>
  /// Specifies position for Endnotes in the section.
  /// </summary>
  public DA.EndnotePosition? EndnotePosition 
  { 
    get => EnumValuesConverter.Convert<DA.EndnotePosition>(OpenXmlElement.GetEndnotePosition());
    set
    {
      if (value == EndnotePosition) return;
      OpenXmlElement.SetEndnotePosition(EnumValuesConverter.ConvertBack<DXW.EndnotePositionValues>(value)); NotifyPropertyChanged(nameof(EndnotePosition));
    }
  }

  DA.NumberingFormat? DA.EndnoteProperties.NumberingFormat
  {
    get => NumberingFormat;
    set => NumberingFormat = (NumberingFormat?)value;
  }

  /// <summary>
  /// <para>Endnote Numbering Format.</para>
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
  /// Specifies the starting value used for the first Endnote whenever the numbering is restarted.
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
  /// Specifies when the Endnote numbering in this section shall be reset to the Endnote number specified by the start attribute's value.
  /// </summary>
  public DA.RestartNumber? NumberingRestart
  {
    get => EnumValuesConverter.Convert<DA.RestartNumber>(OpenXmlElement.GetNumberingRestart());
    set
    {
      if (value == NumberingRestart) return;
      OpenXmlElement.SetNumberingRestart(EnumValuesConverter.ConvertBack<DXW.RestartNumberValues>(value));
      NotifyPropertyChanged(nameof(EndnotePosition));
    }
  }
}