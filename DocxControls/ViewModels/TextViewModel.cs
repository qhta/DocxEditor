namespace DocxControls;

/// <summary>
/// View model for a run text element
/// </summary>
public class TextViewModel : ElementViewModel
{
  /// <summary>
  /// Initializing constructor.
  /// </summary>
  /// <param name="runViewModel">Parent view model. Must be <see cref="RunViewModel"/></param>
  /// <param name="text"></param>
  public TextViewModel(RunViewModel runViewModel, DXW.Text text): base (runViewModel, text)
  {
    DoubleClickCommand = null;
    LeftMouseDownCommand = null;
    RightMouseUpCommand = null;
  }

  /// <summary>
  /// Run element of the paragraph
  /// </summary>
  public DXW.Text TextElement => (DXW.Text)Element!;

  /// <summary>
  /// Text of the element
  /// </summary>
  public string Text => TextElement.Text;


  /// <summary>
  /// Check if the parent run is bold
  /// </summary>
  public bool? IsBold => (TextElement.Parent as DXW.Run)?.IsBold();

  /// <summary>
  /// Check if the parent run is italic
  /// </summary>
  public bool? IsItalic => (TextElement.Parent as DXW.Run)?.IsItalic();

  /// <summary>
  /// Check if the parent run is underlined
  /// </summary>
  public bool? IsUnderline => (TextElement.Parent as DXW.Run)?.IsUnderline();

}
