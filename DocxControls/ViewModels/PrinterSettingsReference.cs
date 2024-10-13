using Qhta.MVVM;

namespace DocxControls.ViewModels;

/// <summary>
/// Specifies an explicit relationship to a PrinterSettings containing information about the printer settings used for this section.
/// </summary>
public class PrinterSettingsReference: ElementReference<string, PrinterSettings>, DA.PrinterSettingsReference
{
  /// <summary>
  /// Constructor with owner object and modeled element.
  /// </summary>
  /// <param name="owner"></param>
  /// <param name="modeledElement"></param>
  public PrinterSettingsReference(ViewModel owner, DXW.PrinterSettingsReference modeledElement) : base(owner, modeledElement)
  {
  }

  internal DXW.PrinterSettingsReference OpenXmlElement => (DXW.PrinterSettingsReference)ModeledElement!;
  /// <summary>
  /// Identifier of the printer settings relationship.
  /// </summary>
  public override string? Id
  {
    get => OpenXmlElement.Id;
    set => OpenXmlElement.Id = value;
  }

  DA.PrinterSettings? DA.IElementReference<string, DA.PrinterSettings>.GetElement() => GetElement();
  /// <summary>
  /// Gets the printer settings from the document.
  /// </summary>
  /// <returns></returns>
  /// <exception cref="NotImplementedException"></exception>
  public override PrinterSettings? GetElement()
  {
    throw new NotImplementedException();
  }
}