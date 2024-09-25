namespace DocxControls.ViewModels;

/// <summary>
/// View model for a paragraph run element
/// </summary>
public class RunViewModel : CompoundElementViewModel
{
  /// <summary>
  /// Initializing constructor.
  /// </summary>
  /// <param name="propertiesViewModel">Owner view model. Must be <see cref="ParagraphViewModel"/></param>
  /// <param name="run">Modeled run element</param>
  public RunViewModel(ElementViewModel propertiesViewModel, DXW.Run run) : base(propertiesViewModel, run)
  {
    LoadAllElements();
    RunProperties ??= new RunPropertiesViewModel(this, run.GetProperties());
  }

  /// <summary>
  /// Run element of the paragraph
  /// </summary>
  public DXW.Run Run => (DXW.Run)Element!;

  /// <summary>
  /// Run properties view model
  /// </summary>
  public RunPropertiesViewModel? RunProperties { get; set; }

  /// <summary>
  /// Check if the run is bold
  /// </summary>
  public bool IsBold => Run.IsBold();

  /// <summary>
  /// Check if the run is italic
  /// </summary>
  public bool IsItalic => Run.IsItalic();

  /// <summary>
  /// Check if the run is underlined
  /// </summary>
  public bool IsUnderline => Run.IsItalic();

  ///// <summary>
  ///// Initializes the object properties
  ///// </summary>
  //protected override ObjectPropertiesViewModel InitObjectProperties()
  //{
  //  var objectProperties = new ObjectPropertiesViewModel();
  //  objectProperties.Add(new ObjectPropertyViewModel(this, nameof(DXW.Run.RsidRunAddition)));
  //  objectProperties.Add(new ObjectPropertyViewModel(this, nameof(DXW.Run.RsidRunDeletion)));
  //  objectProperties.Add(new ObjectPropertyViewModel(this, nameof(DXW.Run.RsidRunProperties)));
  //  AddMoreObjectProperties(objectProperties);
  //  return objectProperties;
  //}

  ///// <summary>
  ///// Initializes the object properties
  ///// </summary>
  //protected void AddMoreObjectProperties(ObjectPropertiesViewModel objectProperties)
  //{
  //  if (RunProperties != null)
  //  {
  //    foreach (var property in RunProperties.ObjectProperties.Items)
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
    if (element is DXW.RunProperties runPropertiesElement)
    {
      if (RunProperties?.Element == runPropertiesElement)
        return RunProperties;
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
    if (element is DXW.RunProperties runPropertiesElement)
    {
      RunProperties = new RunPropertiesViewModel(this, runPropertiesElement);
      return;
    }
    base.CreateChildViewModel(element);
  }
}
