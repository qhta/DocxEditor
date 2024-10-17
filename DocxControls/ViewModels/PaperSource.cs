namespace DocxControls.ViewModels;

/// <summary>
/// Specifies printer-specific settings for the printer tray(s) that shall be used to print different pages in this section in the document.
/// </summary>
public class PaperSource: ElementViewModel<DXW.PaperSource>, DA.PaperSource
{
  /// <summary>
  /// Constructor with owner object and modeled element.
  /// </summary>
  /// <param name="owner"></param>
  /// <param name="modeledElement"></param>
  public PaperSource(ElementViewModel owner, DXW.PaperSource? modeledElement) : base(owner, modeledElement)
  {
  }

  /// <summary>
  /// Specific code that uniquely identifies a specific printer tray to be used to print the first page of this section in the document.
  /// A first value of 1 (the default) is specifically used to indicate that the printer shall automatically select the appropriate printer tray based on the printed page size.
  /// </summary>
  public ushort? First
  {
    get => OpenXmlElement.First?.Value;
    set
    {
      if (value == First) return;
      if (value == null)
        OpenXmlElement.First = null;
      else
      {
        if (value < 1)
          throw new ArgumentOutOfRangeException(nameof(First), "Value must be greater than 0.");
        OpenXmlElement.First = new DX.UInt16Value((ushort)value);
      }
      NotifyPropertyChanged(nameof(First));
    }
  }

  /// <summary>
  /// Specifies a printer-specific code that uniquely identifies a specific printer tray to be used to print each subsequent (non-first) page of this section in the document.
  /// A value of 1 (the default) is specifically used to indicate that the printer shall automatically select the appropriate printer tray based on the printed page size.
  /// </summary>
  public ushort? Other
  {
    get => OpenXmlElement.Other?.Value;
    set
    {
      if (value == Other) return;
      if (value == null)
        OpenXmlElement.Other = null;
      else
      {
        if (value < 1)
          throw new ArgumentOutOfRangeException(nameof(Other), "Value must be greater than 0.");
        OpenXmlElement.First = new DX.UInt16Value((ushort)value);
      }
      NotifyPropertyChanged(nameof(Other));
    }
  }

}