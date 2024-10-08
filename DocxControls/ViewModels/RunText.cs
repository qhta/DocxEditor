namespace DocxControls.ViewModels;

/// <summary>
/// View model for a run text element
/// </summary>
public class RunText : ElementViewModel
{
  /// <summary>
  /// Initializing constructor.
  /// </summary>
  /// <param name="runViewModel">Owner view model. Must be <see cref="Run"/></param>
  /// <param name="text"></param>
  public RunText(ElementViewModel runViewModel, DXW.Text text): base (runViewModel, text)
  {
    DoubleClickCommand = null;
    LeftMouseDownCommand = null;
    RightMouseUpCommand = null;
  }

  /// <summary>
  /// RunElement element of the paragraph
  /// </summary>
  public DXW.Text TextElement => (DXW.Text)OpenXmlElement!;

  /// <summary>
  /// Text of the element
  /// </summary>
  public string Text => TextElement.Text;


  /// <summary>
  /// Check if the owner run is bold
  /// </summary>
  public bool? IsBold => (TextElement.Parent as DXW.Run)?.IsBold();

  /// <summary>
  /// Check if the owner run is italic
  /// </summary>
  public bool? IsItalic => (TextElement.Parent as DXW.Run)?.IsItalic();

  /// <summary>
  /// Check if the owner run is underlined
  /// </summary>
  public bool? IsUnderline => (TextElement.Parent as DXW.Run)?.IsUnderline();

}
