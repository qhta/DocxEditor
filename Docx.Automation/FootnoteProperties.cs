namespace Docx.Automation;

/// <summary>
/// Specifies the properties for footnotes.
/// </summary>
public interface FootnoteProperties
{
  /// <summary>
  /// <para>Footnote Placement.</para>
  /// </summary>
  public FootnotePosition? FootnotePosition { get; set; }
  
  /// <summary>
  /// <para>Footnote Numbering Format.</para>
  /// </summary>
  public NumberingFormat? NumberingFormat { get; set; }

  /// <summary>
  /// <para>Footnote and Endnote Numbering Starting Value.</para>
  /// </summary>
  public int? NumberingStart { get; set; }

  /// <summary>
  /// <para>Footnote and Endnote Numbering Restart Location.</para>
  /// </summary>
  public RestartNumber? NumberingRestart { get; set; }
}