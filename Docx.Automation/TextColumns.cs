namespace Docx.Automation;

/// <summary>
/// Represents settings of text columns.
/// </summary>
public interface TextColumns: IElementCollection<TextColumn>
{
  /// <summary>
  /// <para>Equal Column Widths</para>
  /// </summary>
  public bool? EqualWidth { get; set; }
  /// <summary>
  /// <para>Spacing Between Equal Width Columns</para>
  /// </summary>
  public string? Space { get; set; }
  /// <summary>
  /// <para>Number of Equal Width Columns</para>
  /// </summary>
  public int? ColumnCount { get; set; }
  /// <summary>
  /// <para>Draw Line Between Columns</para>
  /// </summary>
  public bool? Separator { get; set; }

}