namespace DocxControls.ViewModels;

/// <summary>
/// View model for a paragraph
/// </summary>
public class Paragraph : CompoundElementViewModel, DA.Paragraph
{

  /// <summary>
  /// Initializing constructor.
  /// </summary>
  /// <param name="ownerViewModel">Owner view model. Must be <see cref="ElementViewModel"/></param>
  /// <param name="paragraph"></param>
  public Paragraph
    (ElementViewModel ownerViewModel, DXW.Paragraph paragraph) : base(ownerViewModel, paragraph)
  {
    LoadAllElements();
    ParagraphProperties ??= new ParagraphProperties(this, paragraph.GetProperties());
  }



  /// <summary>
  /// Paragraph element of the document
  /// </summary>
  public DXW.Paragraph ParagraphElement => (DXW.Paragraph)Element!;


  DA.ParagraphProperties DA.Paragraph.Properties => ParagraphProperties!;
  /// <summary>
  /// Paragraph properties view model
  /// </summary>
  public ParagraphProperties? ParagraphProperties { get; set; }

  ///// <summary>
  ///// Initializes the object properties
  ///// </summary>
  //protected override ObjectPropertiesViewModel InitObjectProperties()
  //{
  //  var objectProperties = base.InitObjectProperties();
  //  objectProperties.Remove("ParagraphProperties");
  //  AddMoreObjectProperties(objectProperties);
  //  return objectProperties;
  //}

  ///// <summary>
  ///// Initializes the object properties
  ///// </summary>
  //protected void AddMoreObjectProperties(ObjectPropertiesViewModel objectProperties)
  //{
  //  if (ParagraphProperties != null)
  //  {
  //    foreach (var property in ParagraphProperties.ObjectProperties.Items)
  //    {
  //      objectProperties.Add(property);
  //    }
  //  }
  //}

  /// <summary>
  /// Captures find view model for DXW.ParagraphProperties.
  /// </summary>
  /// <param name="element"></param>
  /// <returns></returns>
  public override ElementViewModel? FindViewModel(DX.OpenXmlElement element)
  {
    if (element is DXW.ParagraphProperties paragraphPropertiesElement)
    {
      if (ParagraphProperties?.Element == paragraphPropertiesElement)
        return ParagraphProperties;
      return null;
    }
    return base.FindViewModel(element);
  }

  /// <summary>
  /// Captures create view model for DXW.ParagraphProperties.
  /// </summary>
  /// <param name="element"></param>
  public override void CreateChildViewModel(DX.OpenXmlElement element)
  {
    if (element is DXW.ParagraphProperties paragraphPropertiesElement)
    {
      ParagraphProperties = new ParagraphProperties(this, paragraphPropertiesElement);
      return;
    }
    base.CreateChildViewModel(element);
  }

}
