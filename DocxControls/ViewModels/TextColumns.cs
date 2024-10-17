namespace DocxControls.ViewModels;

/// <summary>
/// Represents settings of text columns.
/// </summary>
public class TextColumns : ElementViewModelCollection<DXW.Columns, TextColumn>, DA.TextColumns
{

  /// <summary>
  /// Initializing constructor.
  /// </summary>
  /// <param name="parent"></param>
  /// <param name="columns"></param>
  public TextColumns(ElementViewModel parent, DXW.Columns columns) : base(parent, columns)
  {
  }

  /// <summary>
  /// Enumerates the collection.
  /// </summary>
  /// <returns></returns>
  public new IEnumerator<DA.TextColumn> GetEnumerator()
  {
    return Items.Cast<DA.TextColumn>().GetEnumerator();
  }

  /// <summary>
  /// <para>Equal Column Widths</para>
  /// </summary>
  public bool? EqualWidth
  {
    get => OpenXmlElement?.EqualWidth?.Value; 
    set
    {
      if (value == EqualWidth) return;
      OpenXmlElement.EqualWidth = value is null ? null : new DX.OnOffValue(value);
      NotifyPropertyChanged(nameof(EqualWidth));
    }
  }

  /// <summary>
  /// <para>Spacing Between Equal Width Columns</para>
  /// </summary>
  public string? Space
  {
    get => OpenXmlElement.Space?.Value;
    set
    {
      string? oldValue = OpenXmlElement.Space?.Value;
      if (value == oldValue) return;
      OpenXmlElement.Space = value is null ? null : new DX.StringValue(value);
      NotifyPropertyChanged(nameof(Space));
    }
  }

  /// <summary>
  /// <para>Number of Equal Width Columns</para>
  /// </summary>
  public int? ColumnCount
  {
    get => OpenXmlElement.ColumnCount?.Value;
    set
    {
      int? oldValue = OpenXmlElement.ColumnCount?.Value;
      if (value == oldValue) return;
      if (value < 1)
        throw new ArgumentOutOfRangeException(nameof(ColumnCount), "Value must be greater than 0.");
      if (value > 32767)
        throw new ArgumentOutOfRangeException(nameof(ColumnCount), "Value must be less than 32768.");
      OpenXmlElement.ColumnCount = value is null ? null : new DX.Int16Value((short)value);
      NotifyPropertyChanged(nameof(ColumnCount));
    }
  }

  /// <summary>
  /// <para>Draw Line Between Columns</para>
  /// </summary>
  public bool? Separator
  {
    get => OpenXmlElement.Separator?.Value;
    set
    {
      bool? oldValue = OpenXmlElement.Separator?.Value;
      if (value == oldValue) return;
      OpenXmlElement.Separator = value is null ? null : new DX.OnOffValue(value);
      NotifyPropertyChanged(nameof(Separator));
    }
  }

  public DA.Application Application { get; }
  public object? Parent { get; }
}