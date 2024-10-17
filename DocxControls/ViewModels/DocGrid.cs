namespace DocxControls.ViewModels;

/// <summary>
/// Specifies the settings for the document grid, which enables precise layout of full-width East Asian language characters within a document
/// by specifying the desired number of characters per line and lines per page for all East Asian text content in this section.
/// </summary>
public class DocGrid : ElementViewModel<DXW.DocGrid>, DA.DocGrid
{
  /// <summary>
  /// Constructor with owner object and modeled ModeledElement.
  /// </summary>
  /// <param name="owner">owner ViewModel</param>
  /// <param name="modeledElement">Modeled ModeledElement</param>
  public DocGrid(ElementViewModel owner, DXW.DocGrid? modeledElement) : base(owner, modeledElement)
  {
  }

  /// <summary>
  /// <para>Document Grid Type</para>
  /// </summary>
  public DA.DocGridType? Type
  {
    get => EnumValuesConverter.Convert<DA.DocGridType>(OpenXmlElement.Type);
    set
    {
      if (value == Type) return;
      OpenXmlElement.Type = (value is not null) ?
        EnumValuesConverter.ConvertBack<DXW.DocGridValues>(value) : null;
      NotifyPropertyChanged(nameof(Type));
    }
  }

  /// <summary>
  /// <para>Document Grid Line Pitch</para>
  /// </summary>
  public int? LinePitch
  {
    get => OpenXmlElement.LinePitch?.Value;
    set
    {
      if (value == LinePitch) return;
      OpenXmlElement.LinePitch = value == null ? null : new DX.Int32Value(value);
      NotifyPropertyChanged(nameof(LinePitch));
    }
  }

  /// <summary>
  /// <para>Document Grid Character Pitch</para>
  /// </summary>
  public int? CharacterSpace
  {
    get => OpenXmlElement.CharacterSpace?.Value;
    set
    {
      if (value == CharacterSpace) return;
      OpenXmlElement.CharacterSpace = value == null ? null : new DX.Int32Value(value);
      NotifyPropertyChanged(nameof(CharacterSpace));
    }
  }
}