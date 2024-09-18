using System.Windows;
using System.Windows.Controls;

namespace DocxControls;

/// <summary>
/// Selects a <c>DataTemplate</c> for a property value based on the <see cref="DocxControls.ElementViewModel"/> property type
/// </summary>
public class ElementTemplateSelector : DataTemplateSelector
{
  /// <summary>
  /// Template for a paragraph.
  /// </summary>
  public DataTemplate? ParagraphTemplate { get; set; }

  /// <summary>
  /// Template for a table.
  /// </summary>
  public DataTemplate? TableTemplate { get; set; }

  /// <summary>
  /// Template for a paragraph run.
  /// </summary>
  public DataTemplate? RunTemplate { get; set; }

  /// <summary>
  /// Template for a run text.
  /// </summary>
  public DataTemplate? TextTemplate { get; set; }

  /// <summary>
  /// Template for a bookmark start.
  /// </summary>
  public DataTemplate? BookmarkStartTemplate { get; set; }

  /// <summary>
  /// Template for a bookmark end.
  /// </summary>
  public DataTemplate? BookmarkEndTemplate { get; set; }

  /// <summary>
  /// Template for section properties.
  /// </summary>
  public DataTemplate? SectionPropertiesTemplate { get; set; }


  /// <summary>
  /// Template for last rendered page break.
  /// </summary>
  public DataTemplate? LastRenderedPageBreakTemplate { get; set; }

  /// <summary>
  /// Template for an unknown element.
  /// </summary>
  public DataTemplate? UnknownElementTemplate { get; set; }

  /// <summary>
  /// Template selection logic.
  /// </summary>
  /// <param name="item"></param>
  /// <param name="container"></param>
  /// <returns></returns>
  public override DataTemplate? SelectTemplate(object? item, DependencyObject container)
  {
    if (item is ParagraphViewModel)
      return ParagraphTemplate ?? UnknownElementTemplate;
    if (item is TableViewModel)
      return TableTemplate ?? UnknownElementTemplate;
    if (item is RunViewModel)
      return RunTemplate ?? UnknownElementTemplate;
    if (item is TextViewModel)
      return TextTemplate ?? UnknownElementTemplate;
    if (item is BookmarkStart)
      return BookmarkStartTemplate ?? UnknownElementTemplate;
    if (item is BookmarkEnd)
      return BookmarkEndTemplate ?? UnknownElementTemplate;
    if (item is SectionPropertiesViewModel)
      return SectionPropertiesTemplate ?? UnknownElementTemplate;
    if (item is LastRenderedPageBreakViewModel)
      return LastRenderedPageBreakTemplate ?? UnknownElementTemplate;
    return UnknownElementTemplate;
  }
}