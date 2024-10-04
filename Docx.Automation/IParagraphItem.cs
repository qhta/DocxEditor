﻿namespace Docx.Automation;

/// <summary>
/// Interface for paragraph items (e.g. runs, tables, etc.)
/// </summary>
public interface IParagraphItem
{
  /// <summary>
  /// Owner paragraph of the item.
  /// </summary>
  public Paragraph Paragraph { get; }
}