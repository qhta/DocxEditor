namespace DocxControls.ViewModels;

/// <summary>
/// Represents a section in a document.
/// </summary>
public class Section : ElementViewModel, DA.Section
{
  /// <summary>
  /// Initializing constructor.
  /// </summary>
  /// <param name="owner"></param>
  /// <param name="properties"></param>
  public Section(Body owner, SectionProperties properties) : base(owner, properties.OpenXmlElement)
  {
    SectionProperties = properties;
  }

  /// <summary>
  /// Properties of the section
  /// </summary>
  public DA.SectionProperties Properties
  {
    get => SectionProperties;
    set
    {
      if (value == SectionProperties) return;
      SectionProperties = (SectionProperties)value;
      NotifyPropertyChanged(nameof(Properties));
    }
  }

  /// <summary>
  /// SectionProperties view model.
  /// </summary>
  public SectionProperties SectionProperties { get; set; }

  /// <summary>
  /// OpenXml ModeledElement element.
  /// </summary>
  public DXW.SectionProperties OpenXmlElement => (DXW.SectionProperties?)ModeledElement!;

  /// <summary>
  /// Gets the body that contains the section.
  /// </summary>
  public Body OwnerBody => (Body)Parent!;


  DA.Range DA.Section.Range => Range;
  /// <summary>
  /// Get the range of the section.
  /// </summary>
  public Range Range
  {
    get
    {
      var index = OwnerBody.Sections.IndexOf(this);
      ElementViewModel? firstElement = null;
      if (index > 0)
      {
        var previousSection = OwnerBody.Sections[index - 1];
        firstElement =
          (previousSection.SectionProperties?.Owner as Paragraph)?.NextSibling() ?? OwnerBody.Elements.LastOrDefault();
      }
      else
        firstElement = OwnerBody.Elements.FirstOrDefault();
      var lastElement =
        (SectionProperties?.Owner as Paragraph) ?? OwnerBody.Elements.LastOrDefault();

      var range = new Range(this, firstElement, lastElement);
      return range;

    }
  }
}
