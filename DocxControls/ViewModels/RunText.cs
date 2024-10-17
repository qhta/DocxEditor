namespace DocxControls.ViewModels;

/// <summary>
/// View model for a run text element
/// </summary>
public class RunText : ElementViewModel<DXW.Text>, DA.RunText
{
  /// <summary>
  /// Initializing constructor.
  /// </summary>
  /// <param name="runViewModel">Owner view model. Must be <see cref="Run"/></param>
  /// <param name="text"></param>
  public RunText(Run runViewModel, DXW.Text? text): base (runViewModel, text)
  {
    DoubleClickCommand = null;
    LeftMouseDownCommand = null;
    RightMouseUpCommand = null;
  }

  /// <summary>
  /// Parent run of the element
  /// </summary>
  public DA.Run ParentRun => (DA.Run)Owner!;

  /// <summary>
  /// Text of the element
  /// </summary>
  public string? Text
  {
    get
    {
      var text = OpenXmlElement.Text;
      //Debug.WriteLine($"RunText.Text: {text}");
      return text;
    }
    set
    {
      if (value == OpenXmlElement.Text) return;
      OpenXmlElement.Text = value ?? "";
      NotifyPropertyChanged(nameof(Text));
    }
  }


  /// <summary>
  /// Check if the owner run is bold
  /// </summary>
  public bool? IsBold => (OpenXmlElement.Parent as DXW.Run)?.IsBold();

  /// <summary>
  /// Check if the owner run is italic
  /// </summary>
  public bool? IsItalic => (OpenXmlElement.Parent as DXW.Run)?.IsItalic();

  /// <summary>
  /// Check if the owner run is underlined
  /// </summary>
  public bool? IsUnderline => (OpenXmlElement.Parent as DXW.Run)?.IsUnderline();

}
