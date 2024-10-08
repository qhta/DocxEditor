namespace DocxControls.ViewModels;

/// <summary>
/// View model for a paragraph run element
/// </summary>
public class Run : CompoundElementViewModel, DA.Run
{
  /// <summary>
  /// Initializing constructor.
  /// </summary>
  /// <param name="propertiesViewModel">Owner view model. Must be <see cref="Paragraph"/></param>
  /// <param name="run">Modeled run element</param>
  public Run(ElementViewModel propertiesViewModel, DXW.Run run) : base(propertiesViewModel, run)
  {
    LoadAllElements();
    RunProperties ??= new RunProperties(this, run.GetProperties());
  }

  /// <summary>
  /// RunElement element of the paragraph
  /// </summary>
  public DXW.Run RunElement => (DXW.Run)OpenXmlElement!;


  DA.RunProperties DA.Run.Properties => RunProperties!;
  /// <summary>
  /// RunElement properties view model
  /// </summary>
  public RunProperties? RunProperties { get; set; }

  /// <summary>
  /// Check if the run is bold
  /// </summary>
  public bool IsBold => RunElement.IsBold();

  /// <summary>
  /// Check if the run is italic
  /// </summary>
  public bool IsItalic => RunElement.IsItalic();

  /// <summary>
  /// Check if the run is underlined
  /// </summary>
  public bool IsUnderline => RunElement.IsItalic();

  /// <summary>
  /// Text of the run
  /// </summary>
  public string Text
  {
    get => RunElement.GetText(GetTextOptions.Default);
    set
    {
      RunElement.SetText(value);
      NotifyPropertyChanged(nameof(Text));
    }

  }

  ///// <summary>
  ///// Initializes the object properties
  ///// </summary>
  //protected override ObjectPropertiesViewModel InitObjectProperties()
  //{
  //  var objectProperties = new ObjectPropertiesViewModel();
  //  objectProperties.Add(new ObjectPropertyViewModel(this, nameof(DXW.RunElement.RsidRunAddition)));
  //  objectProperties.Add(new ObjectPropertyViewModel(this, nameof(DXW.RunElement.RsidRunDeletion)));
  //  objectProperties.Add(new ObjectPropertyViewModel(this, nameof(DXW.RunElement.RsidRunProperties)));
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
      if (RunProperties?.OpenXmlElement == runPropertiesElement)
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
      RunProperties = new RunProperties(this, runPropertiesElement);
      return;
    }
    base.CreateChildViewModel(element);
  }

  /// <summary>
  /// Remove the run from the paragraph
  /// </summary>
  /// <returns></returns>
  public bool Remove()
  {
    (Owner as CompoundElementViewModel)?.Elements.Remove(this);
    RunElement.Remove();
    return true;
  }
}
