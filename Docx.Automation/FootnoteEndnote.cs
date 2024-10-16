﻿namespace Docx.Automation;

/// <summary>
/// Represents a footnote or endnote in a document.
/// </summary>
public interface FootnoteEndnote: _Element, _Story
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