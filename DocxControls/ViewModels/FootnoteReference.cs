namespace DocxControls.ViewModels;

/// <summary>
/// Specifies the presence of a footnote reference at the current location in the footnote.
/// </summary>
public class FootnoteReference : ElementViewModel<DXW.FootnoteReference>, DA.FootnoteEndnoteReference
{
  /// <summary>
  /// Constructor with owner and modeled element.
  /// </summary>
  /// <param name="owner"></param>
  /// <param name="modeledElement"></param>
  public FootnoteReference(Run owner, DXW.FootnoteReference? modeledElement) : base(owner, modeledElement)
  {
  }

  /// <summary>
  /// Parent run of this view model
  /// </summary>
  public DA.Run ParentRun => (DA.Run)Owner!;

  /// <summary>
  /// Identifier of the footnote.
  /// </summary>
  public long Id
  {
    get => OpenXmlElement.Id?.Value ?? 0;
    set
    {
      if (value == Id) return;
      OpenXmlElement.Id = new DX.IntegerValue(value);
      NotifyPropertyChanged(nameof(Id));
    }
  }

  /// <summary>
  /// Get footnote/endnote
  /// </summary>
  /// <returns></returns>
  /// <exception cref="NotImplementedException"></exception>
  public DA.FootnoteEndnote? GetElement()
  {
    throw new NotImplementedException(); // TODO: Implement GetFootnoteEndnote
  }
}