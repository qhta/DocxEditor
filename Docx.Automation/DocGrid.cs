namespace Docx.Automation;

/// <summary>
/// Specifies the settings for the document grid, which enables precise layout of full-width East Asian language characters within a document
/// by specifying the desired number of characters per line and lines per page for all East Asian text content in this section.
/// </summary>
public interface DocGrid: IElement
{
  /// <summary>
  /// <para>Document Grid Type</para>
  /// </summary>
  public DocGridType? Type { get; set; }
  /// <summary>
  /// <para>Document Grid Line Pitch</para>
  /// </summary>
  public int? LinePitch { get; set; }
  /// <summary>
  /// <para>Document Grid Character Pitch</para>
  /// </summary>
  public int? CharacterSpace { get; set; }
}