namespace Docx.Automation;

/// <summary>
/// Represents a contiguous area in a document. Each Range object is defined by a starting and ending element.
/// </summary>
public interface Range
{
  /// <summary>
  /// Element that starts the range.
  /// </summary>
  public IElement Start { get; set; }

  /// <summary>
  /// Element that ends the range.
  /// </summary>
  public IElement End { get; set; }

  #region properties
  /// <summary>
  /// Returns an object that represents the DocxControls application.
  /// </summary>
  public Application Application { get; }

  /// <summary>
  /// Returns the parent object for the specified object.
  /// </summary>
  public object? Parent { get; }

  //  Bold
  //BoldBi
  //  BookmarkID
  //Bookmarks
  //  Borders
  //Case
  //  Cells
  //Characters
  //  CharacterStyle
  //CharacterWidth
  //  Columns
  //CombineCharacters
  //  Comments
  //Conflicts
  //  ContentControls
  //Creator
  //  DisableCharacterSpaceGrid
  //Document
  //  Duplicate
  //Editors
  //  EmphasisMark
  //  EndnoteOptions
  //Endnotes
  //  EnhMetaFileBits
  //Fields
  //  Find
  //FitTextWidth
  //  Font
  //FootnoteOptions
  //  Footnotes
  //FormattedText
  //  FormFields
  //Frames
  //  GrammarChecked
  //GrammaticalErrors
  //  HighlightColorIndex
  //HorizontalInVertical
  //  HTMLDivisions
  //Hyperlinks
  //  ID
  //Information
  //  InlineShapes
  //IsEndOfRowMark
  //  Italic
  //ItalicBi
  //  Kana
  //LanguageDetected
  //  LanguageID
  //LanguageIDFarEast
  //  LanguageIDOther
  //ListFormat
  //  ListParagraphs
  //ListStyle
  //  Locks
  //NextStoryRange
  //  NoProofing
  //OMaths
  //  Orientation
  //PageSetup
  //  ParagraphFormat
  //Paragraphs
  //  ParagraphStyle
  //  ParentContentControl
  //PreviousBookmarkID
  //  ReadabilityStatistics
  //Revisions
  //  Rows
  //Scripts
  //  Sections
  //Sentences
  //  Shading
  //ShapeRange
  //  ShowAll
  //SpellingChecked
  //  SpellingErrors
  //  StoryLength
  //StoryType
  //  Style
  //Subdocuments
  //  SynonymInfo
  //Tables
  //  TableStyle
  //Text
  //  TextRetrievalMode
  //TextVisibleOnScreen
  //  TopLevelTables
  //TwoLinesInOne
  //  Underline
  //Updates
  //  WordOpenXML
  //Words
  //  XML

  #endregion

  #region methods

  //AutoFormat
  //  Calculate
  //CheckGrammar
  //  CheckSpelling
  //CheckSynonyms
  //  Collapse
  //ComputeStatistics
  //  ConvertHangulAndHanja
  //ConvertToTable
  //  Copy
  //CopyAsPicture
  //  Cut
  //Delete
  //  DetectLanguage
  //EndOf
  //  Expand
  //ExportAsFixedFormat
  //  ExportAsFixedFormat2
  //ExportFragment
  //  GetSpellingSuggestions
  //GoTo
  //  GoToEditableRange
  //GoToNext
  //  GoToPrevious
  //ImportFragment
  //  InRange
  //InsertAfter
  //  InsertAlignmentTab
  //InsertAutoText
  //  InsertBefore
  //InsertBreak
  //  InsertCaption
  //InsertCrossReference
  //  InsertDatabase
  //InsertDateTime
  //  InsertFile
  //InsertParagraph
  //  InsertParagraphAfter
  //InsertParagraphBefore
  //  InsertSymbol
  //InsertXML
  //  InStory
  //IsEqual
  //  LookupNameProperties
  //ModifyEnclosure
  //  Move
  //MoveEnd
  //  MoveEndUntil
  //MoveEndWhile
  //  MoveStart
  //MoveStartUntil
  //  MoveStartWhile
  //MoveUntil
  //  MoveWhile
  //Next
  //  NextSubdocument
  //Paste
  //  PasteAndFormat
  //PasteAppendTable
  //  PasteAsNestedTable
  //PasteExcelTable
  //  PasteSpecial
  //PhoneticGuide
  //  Previous
  //PreviousSubdocument
  //  Relocate
  //Select
  //  SetListLevel
  //SetRange
  //  Sort
  //SortAscending
  //  SortByHeadings
  //SortDescending
  //  StartOf
  //TCSCConverter
  //  WholeStory

  #endregion
}
