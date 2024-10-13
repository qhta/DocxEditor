namespace Docx.Automation;

/// <summary>
/// Specifies the properties of a section.
/// </summary>
public interface SectionPropertiesBase
{
  /// <summary>
  /// Specifies a unique identifier used to track the editing session when the physical character representing this section mark was last formatted.
  /// </summary>
  public string? RsidRPr { get; set; }

  /// <summary>
  /// Specifies a unique identifier used to track the editing session when the section mark for this section was deleted from the document.
  /// </summary>
  public string? RsidDel { get; set; }

  /// <summary>
  /// Specifies a unique identifier used to track the editing session when the section mark for this section was added to the document.
  /// </summary>
  public string? RsidR { get; set; }

  /// <summary>
  /// <para>Section Type that describes section break.</para>
  /// </summary>
  public SectionType? SectionType { get; set; }

  /// <summary>
  /// Specifies that this section shall be presented using a right-to-left page direction.
  /// This property only affects section-level properties, and does not affect the layout of text within the contents of this section. 
  /// </summary>
  public bool? BiDi { get; set; }

  /// <summary>
  /// Specifies whether the parent section in this document shall have a different header and footer for its first page. 
  /// </summary>
  public bool? TitlePage { get; set; }

  /// <summary>
  /// Specifies the direction of the text flow for this section. 
  /// </summary>
  public TextDirection? TextDirection { get; set; }

  /// <summary>
  /// Specifies the vertical alignment for text on pages in the current section, relative to the top and bottom margins in the main document story on each page.
  /// </summary>
  public VerticalJustification? VerticalTextAlignmentOnPage { get; set; }

  /// <summary>
  /// Specifies that all endnotes in this document shall not  be displayed or printed.
  /// On any section break other than the first section break in the document, it shall be ignored. 
  /// </summary>
  public bool? NoEndnote { get; set; }

  /// <summary>
  /// Specifies that the page gutter shall be placed on the right side of the page for this section only.
  /// The page gutter defines the amount of extra space added to the specified margin, above any existing margin values. 
  /// </summary>
  public bool? GutterOnRight { get; set; }

  /// <summary>
  /// specifies that the contents of the current section shall be protected such that they cannot be edited by a user
  /// (if the consumer is displaying the document and allowing the user to make modification)
  /// except for the text contained in any form field or embedded control that is part of the current section. 
  /// </summary>
  public bool? FormProtection { get; set; }

  /// <summary>
  /// Specifies the settings for line numbering to be displayed before each column of text in this section in the document. 
  /// </summary>
  public LineNumberType? LineNumberType { get; set; }

  /// <summary>
  /// Specifies the settings for line numbering to be displayed before each column of text in this section in the document. 
  /// </summary>
  public PageNumberType? PageNumberType { get; set; }

  /// <summary>
  /// Specifies the set of columns defined for this section in the document.
  /// </summary>
  public TextColumns? Columns { get; set; }

  /// <summary>
  /// Specifies the number of columns of footnotes. This property is available in Office 2013 and above.
  /// </summary>
  public int? FootnoteColumns { get; set; }

  /// <summary>
  /// Specifies the properties for footnotes in this section.
  /// </summary>
  public FootnoteProperties? FootnoteProperties { get; set; }

  /// <summary>
  /// Specifies the properties for endnotes in this section.
  /// </summary>
  public EndnoteProperties? EndnoteProperties { get; set; }

  /// <summary>
  /// Collection of headers in this section.
  /// </summary>
  public HeadersFooters Headers { get; }

  /// <summary>
  /// Collection of footers in this section.
  /// </summary>
  public HeadersFooters Footers { get; }

  /// <summary>
  ///  Specifies the page margins for all pages in this section.
  /// </summary>
  public PageMargin? PageMargin { get; set; }

  /// <summary>
  ///  Specifies the properties (size and orientation) for all pages in the current section.
  /// </summary>
  public PageSize? PageSize { get; set; }

  /// <summary>
  /// Specifies printer-specific settings for the printer tray(s) that shall be used to print different pages in this section in the document.
  /// </summary>
  public PaperSource? PaperSource { get; set; }

  /// <summary>
  /// Specifies an explicit relationship to a PrinterSettings containing information about the printer settings used for this section.
  /// </summary>
  public PrinterSettingsReference? PrinterSettingsReference { get; set; }

  /// <summary>
  ///  Specifies the settings for the document grid, which enables precise layout of full-width East Asian language characters.
  /// </summary>
  public DocGrid? DocGrid { get; set; }

}