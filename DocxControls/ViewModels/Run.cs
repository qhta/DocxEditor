namespace DocxControls.ViewModels;

/// <summary>
/// View model for a paragraph run element
/// </summary>
public sealed class Run : CompoundElementViewModel, DA.Run
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
  /// OpenXmlElement element of the run
  /// </summary>
  internal DXW.Run OpenXmlElement => (DXW.Run)ModeledElement!;


  DA.RunProperties DA.Run.Properties => RunProperties!;
  /// <summary>
  /// OpenXmlElement properties view model
  /// </summary>
  public RunProperties? RunProperties { get; set; }

  /// <summary>
  /// Check if the run is bold
  /// </summary>
  public bool IsBold => OpenXmlElement.IsBold();

  /// <summary>
  /// Check if the run is italic
  /// </summary>
  public bool IsItalic => OpenXmlElement.IsItalic();

  /// <summary>
  /// Check if the run is underlined
  /// </summary>
  public bool IsUnderline => OpenXmlElement.IsItalic();

  /// <summary>
  /// Text of the run
  /// </summary>
  public string? Text
  {
    get => OpenXmlElement.GetText(GetTextOptions.Default);
    set
    {
      value ??= "";
      if (value == OpenXmlElement.GetText(GetTextOptions.Default)) return;
      for (int i = Elements.Count - 1; i > 0; i--)
        Elements.RemoveAt(i);
      if (Elements.Count == 0)
        Elements.Add(new RunText(this, new DXW.Text(value)));
      if (Elements[0] is not RunText runText)

        Elements[0] = runText = new RunText(this, new DXW.Text(value));
      runText.Text = value;
      OpenXmlElement.SetText(value);
      NotifyPropertyChanged(nameof(Text));
    }
  }

  ///// <summary>
  ///// Initializes the object properties
  ///// </summary>
  //protected override ObjectPropertiesViewModel InitObjectProperties()
  //{
  //  var objectProperties = new ObjectPropertiesViewModel();
  //  objectProperties.Add(new ObjectPropertyViewModel(this, nameof(DXW.OpenXmlElement.RsidRunAddition)));
  //  objectProperties.Add(new ObjectPropertyViewModel(this, nameof(DXW.OpenXmlElement.RsidRunDeletion)));
  //  objectProperties.Add(new ObjectPropertyViewModel(this, nameof(DXW.OpenXmlElement.RsidRunProperties)));
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
      if (RunProperties?.ModeledElement == runPropertiesElement)
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

}
