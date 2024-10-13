namespace Docx.Automation;

/// <summary>
/// Specifies the numbering format. It can be one of the predefined formats or a custom format.
/// </summary>
public interface NumberingFormat
{
  /// <summary>
  /// <para>Numbering Format Type</para>
  /// </summary>
  public NumberFormat? Preset { get; set; }
  /// <summary>
  /// <para>Custom format, this property is only available in Office 2010 and later.</para>
  /// </summary>
  public string? Format { get; set; }
}