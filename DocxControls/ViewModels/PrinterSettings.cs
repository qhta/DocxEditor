using System.ComponentModel.DataAnnotations.Schema;

using Qhta.MVVM;

namespace DocxControls.ViewModels;

/// <summary>
/// Specifies an explicit relationship to a printer settings containing information about the printer settings used for this section.
/// </summary>
[NotMapped]
public class PrinterSettings: ElementViewModel, DA.PrinterSettings
{
  /// <summary>
  /// Constructor with owner object and modeled element.
  /// </summary>
  /// <param name="owner"></param>
  /// <param name="modeledElement"></param>
  public PrinterSettings(ViewModel owner, DX.OpenXmlElement modeledElement) : base(owner, modeledElement)
  {
  }
}