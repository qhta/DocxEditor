namespace Docx.Automation;

/// <summary>
/// Specifies the settings for line numbering.
/// </summary>
public interface LineNumberType : IElement
{
  /// <summary>
  /// <para>Line Number Increments to Display</para>
  /// </summary>
  public int? CountBy { get; set; }

  /// <summary>
  /// <para>Line Numbering Starting Value</para>
  /// </summary>
  public int? Start { get; set; }

  /// <summary>
  /// <para>Distance Between Text and Line Numbering (in points)</para>
  /// </summary>
  public Length? Distance { get; set; }

  /// <summary>
  /// <para>Line Numbering Restart Setting</para>
  /// </summary>
  public LineNumberRestart? Restart { get; set; }
}