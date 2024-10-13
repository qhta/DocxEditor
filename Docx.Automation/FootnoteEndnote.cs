namespace Docx.Automation;

/// <summary>
/// Represents a footnote or endnote in a document.
/// </summary>
public interface FootnoteEndnote: IElement, IStory
{
  /// <summary>
  /// <para>Footnote/Endnote Type</para>
  /// </summary>
  public FootnoteEndnoteType? Type { get; set; }
  /// <summary>
  /// <para>Footnote/Endnote ID</para>
  /// </summary>
  public int Id { get; set; }
}