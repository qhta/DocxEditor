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
  public Section(Body owner) : base(owner)
  {
  }

  /// <summary>
  /// Initializing constructor.
  /// </summary>
  /// <param name="owner"></param>
  /// <param name="properties"></param>
  public Section(Body owner, SectionProperties properties) : base(owner, properties.SectionPropertiesElement)
  {
    SectionProperties = properties;
  }

  /// <summary>
  /// SectionProperties view model.
  /// </summary>
  public SectionProperties? SectionProperties { get; }

  /// <summary>
  /// OpenXml SectionPropertiesElement element.
  /// </summary>
  public DXW.SectionProperties? SectionPropertiesElement => (DXW.SectionProperties?)OpenXmlElement;

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
