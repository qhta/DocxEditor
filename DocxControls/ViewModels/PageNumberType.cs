namespace DocxControls.ViewModels;

/// <summary>
/// Specifies the type of page numbering.
/// </summary>
public class PageNumberType : ElementViewModel<DXW.PageNumberType>, DA.PageNumberType
{
  /// <summary>
  /// Initializes a new instance of the <see cref="PageNumberType"/> class with the owner and modeledElement.
  /// </summary>
  /// <param name="owner"></param>
  /// <param name="modeledElement"></param>
  public PageNumberType(ElementViewModel owner, DXW.PageNumberType? modeledElement) : base(owner, modeledElement)
  {
  }

  /// <summary>
  /// Specifies the number format that shall be used for all page numbering in this section.>
  /// </summary>
  public DA.NumberFormat? Format
  {

    get => EnumValuesConverter.Convert<DA.NumberFormat>(OpenXmlElement.Format);
    set
    {
      if (value == Format) return;
      OpenXmlElement.Format =
        (value is not null) ? EnumValuesConverter.ConvertBack<DXW.NumberFormatValues>(value) : null;
      NotifyPropertyChanged(nameof(Format));
    }
  }

  /// <summary>
  /// Specifies the page number that appears on the first page of the section.
  /// </summary>
  public int? Start
  {
    get => OpenXmlElement.Start?.Value;
    set
    {
      if (value == Start) return;
      if (value == null)
        OpenXmlElement.Start = null;
      else
      {
        if (value < 0)
          throw new ArgumentOutOfRangeException(nameof(Start), "Value must be greater than or equal 0.");
        OpenXmlElement.Start = new DX.Int32Value(value);
      }
      NotifyPropertyChanged(nameof(Start));
    }
  }

  /// <summary>
  /// Specifies the one-based index of the heading style applied to chapter titles in the document
  /// which shall be used as chapter headings in all page numbers for this section,
  /// by locating the nearest heading of that style and extracting the numbering information.
  /// </summary>
  public int? ChapterStyle
  {
    get => OpenXmlElement.ChapterStyle?.Value;
    set
    {
      if (value == ChapterStyle) return;
      if (value == null)
        OpenXmlElement.ChapterStyle = null;
      else
      {
        if (value < 1)
          throw new ArgumentOutOfRangeException(nameof(ChapterStyle), "Value must be greater than or equal 1.");
        if (value > 255)
          throw new ArgumentOutOfRangeException(nameof(Start), "Value must be less than 256.");
        OpenXmlElement.ChapterStyle = new DX.ByteValue((byte)value);
      }
      NotifyPropertyChanged(nameof(ChapterStyle));
    }
  }

  /// <summary>
  /// Specifies the separator character that shall appear between the chapter and page number,
  /// if a chapter style has been set for page numbers in this section.
  /// </summary>
  public DA.ChapterSeparator? ChapterSeparator
  {
    get => EnumValuesConverter.Convert<DA.ChapterSeparator>(OpenXmlElement.ChapterSeparator);
    set
    {
      if (value == ChapterSeparator) return;
      OpenXmlElement.ChapterSeparator =
        (value is not null) ? EnumValuesConverter.ConvertBack<DXW.ChapterSeparatorValues>(value) : null;
      NotifyPropertyChanged(nameof(Format));
    }
  }

}