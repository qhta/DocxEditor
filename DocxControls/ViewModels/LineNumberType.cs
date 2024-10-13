namespace DocxControls.ViewModels;

/// <summary>
/// Specifies the settings for line numbering.
/// </summary>
public class LineNumberType: ElementViewModel, DA.LineNumberType
{

  /// <summary>
  /// Constructor with owner object and modeled element.
  /// </summary>
  /// <param name="owner"></param>
  /// <param name="modeledElement"></param>
  public LineNumberType(ElementViewModel owner, DXW.LineNumberType modeledElement) : base(owner, modeledElement)
  {
  }

  internal DXW.LineNumberType OpenXmlElement => (DXW.LineNumberType)ModeledElement!;

  /// <summary>
  /// <para>Line Number Type</para>
  /// <para>Specifies the line number increments to be displayed in the current document.</para>
  /// </summary>
  public int? CountBy
  {
    get => OpenXmlElement.CountBy?.Value;
    set
    {
      if (value == CountBy) return;
      if (value == null)
        OpenXmlElement.CountBy = null;
      else
      {
        if (value < 1)
          throw new ArgumentOutOfRangeException(nameof(CountBy), "Value must be greater than 0.");
        if (value > 32767)
          throw new ArgumentOutOfRangeException(nameof(CountBy), "Value must be less than 32768.");
        OpenXmlElement.CountBy = new DX.Int16Value((short)value);
      }
      NotifyPropertyChanged(nameof(CountBy));
    }
  }
  /// <summary>
  /// <para>Line Numbering Starting Value</para>
  /// <para>Specifies the starting value used for the first line whenever the line numbering is restarted by use of the restart attribute.</para>
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
        if (value > 32767)
          throw new ArgumentOutOfRangeException(nameof(Start), "Value must be less than 32768.");
        OpenXmlElement.Start = new DX.Int16Value((short)value);
      }
      NotifyPropertyChanged(nameof(Start));
    }
  }

  DA.Length? DA.LineNumberType.Distance { get => Distance; set => Distance = (Length?)value; }

  /// <summary>
  /// <para>Distance between text and line numbers.</para>
  /// <para>Specifies the distance between the text margin and the edge of any line numbers appearing in that section.</para>
  /// </summary>
  public Length? Distance
  {
    get { 
      string? value = OpenXmlElement.Distance?.Value;
      if (value == null) return null;
      Twips twips = Twips.FromString(value);
      return new Length(twips.ToPoints());
    }
    set
    {
      Twips? oldValue = OpenXmlElement.Distance?.Value;
      Twips? newValue = value?.ToTwips();
      if (newValue == oldValue) return;
      OpenXmlElement.Distance = value is null ? null : new DX.StringValue(newValue);
      NotifyPropertyChanged(nameof(Distance));
    }
  }


  /// <summary>
  /// <para>Line Numbering Restart Setting</para>
  /// <para>Specifies when the line numbering in this section shall be reset to the line number specified by the start attribute's value.</para>
  /// </summary>
  public DA.LineNumberRestart? Restart
  {
    get => EnumValuesConverter.Convert<DA.LineNumberRestart>(OpenXmlElement.Restart);
    set
    {
      if (value == Restart) return;
      OpenXmlElement.Restart = (value is not null) ? 
        EnumValuesConverter.ConvertBack<DXW.LineNumberRestartValues>(value) : null;
      NotifyPropertyChanged(nameof(Restart));
    }
  }

}