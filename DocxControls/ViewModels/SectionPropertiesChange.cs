namespace DocxControls.ViewModels;

/// <summary>
/// Represents a change in section properties.
/// </summary>
public class SectionPropertiesChange : ElementViewModel, DA.SectionPropertiesChange
{
  /// <summary>
  /// Constructor with owner and modeled element.
  /// </summary>
  /// <param name="owner"></param>
  /// <param name="modeledElement"></param>
  public SectionPropertiesChange(ElementViewModel owner, DXW.SectionPropertiesChange modeledElement) : base(owner, modeledElement)
  {
  }

  internal DXW.SectionPropertiesChange OpenXmlElement => (DXW.SectionPropertiesChange)ModeledElement!;

  /// <summary>
  /// <para>author</para>
  /// </summary>
  public string? Author
  {
    get => OpenXmlElement.Author;
    set
    {
      if (OpenXmlElement.Author == value) return;
      OpenXmlElement.Author = value;
      NotifyPropertyChanged(nameof(Author));
    }
  }

  /// <summary>
  /// <para>date</para>
  /// </summary>
  public DateTime? Date
  {
    get => OpenXmlElement.Date?.Value;
    set
    {
      if (OpenXmlElement.Date?.Value == value) return;
      OpenXmlElement.Date = value;
      NotifyPropertyChanged(nameof(Date));
    }
  }

  /// <summary>
  /// <para>dateUtc, this property is only available in Microsoft 365 and later.</para>
  /// </summary>
  public DateTime? DateUtc
  {
    get => OpenXmlElement.DateUtc?.Value;
    set
    {
      if (OpenXmlElement.DateUtc?.Value == value) return;
      OpenXmlElement.DateUtc = value;
      NotifyPropertyChanged(nameof(DateUtc));
    }
  }

  /// <summary>
  /// <para>Annotation Identifier</para>
  /// </summary>
  public String? Id
  {
    get => OpenXmlElement.Id;
    set
    {
      if (OpenXmlElement.Id == value) return;
      OpenXmlElement.Id = value;
      NotifyPropertyChanged(nameof(Id));
    }
  }

  /// <summary>
  /// Previous section properties.
  /// </summary>
  public DA.PreviousSectionProperties? PreviousSectionProperties
  {
    get
    {
      if (_PreviousSectionProperties==null)
      {
        var element = OpenXmlElement.GetPreviousSectionProperties();
        if (element != null) _PreviousSectionProperties = new PreviousSectionProperties(this, element);
      }
      return _PreviousSectionProperties;
    }
    set
    {
      if (value == PreviousSectionProperties) return;
      if (value == null)
      {
        OpenXmlElement.SetPreviousSectionProperties(null);
        _PreviousSectionProperties = null;
      }
      else
      {
        if (value is not PreviousSectionProperties previousSectionProperties) throw new ArgumentException("Invalid type.", nameof(PreviousSectionProperties));
        _PreviousSectionProperties = previousSectionProperties;
        OpenXmlElement.SetPreviousSectionProperties(previousSectionProperties.OpenXmlElement);
        PreviousSectionProperties = previousSectionProperties;
      }
      NotifyPropertyChanged(nameof(PreviousSectionProperties));
    }
  }

  private PreviousSectionProperties? _PreviousSectionProperties;
}