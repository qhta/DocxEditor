namespace Docx.Automation;

/// <summary>
/// Specifies the properties for endnotes.
/// </summary>
public interface EndnoteProperties
{
  /// <summary>
  /// <para>Endnote Placement.</para>
  /// </summary>
  public EndnotePosition? EndnotePosition { get; set; }
  
  /// <summary>
  /// <para>Endnote Numbering Format.</para>
  /// </summary>
  public NumberingFormat? NumberingFormat { get; set; }

  /// <summary>
  /// <para>Endnote and Endnote Numbering Starting Value.</para>
  /// </summary>
  public int? NumberingStart { get; set; }

  /// <summary>
  /// <para>Endnote and Endnote Numbering Restart Location.</para>
  /// </summary>
  public RestartNumber? NumberingRestart { get; set; }
}