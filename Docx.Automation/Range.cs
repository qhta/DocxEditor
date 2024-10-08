using System.Collections;
using System.ComponentModel;

namespace Docx.Automation;

/// <summary>
/// Represents a contiguous area in a document. Each Range object is defined by a starting and ending element.
/// </summary>
public interface Range
{
  /// <summary>
  /// Element that starts the range.
  /// </summary>
  public object Start { get; }

  /// <summary>
  /// Element that ends the range.
  /// </summary>
  public object End { get; }

  #region properties
  /// <summary>
  /// Returns an object that represents the DocxControls application.
  /// </summary>
  public Application Application { get; }

  /// <summary>
  /// Returns the parent object for the specified object.
  /// </summary>
  public object? Parent { get; }

  /// <summary>
  /// Return all the blocks in the range.
  /// </summary>
  IEnumerable<Block> Blocks { get; }

  /// <summary>
  /// Return the paragraphs in the range.
  /// </summary>
  IEnumerable<Paragraph> Paragraphs { get; }

  /// <summary>
  /// Return the tables in the range.
  /// </summary>
  IEnumerable<Table> Tables { get; }

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

  //Paragraphs
  //ParagraphFormat
  //ParagraphStyle

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

  /// <summary>
  /// Collapses a range or selection to the starting or ending position. After a range or selection is collapsed, the starting and ending points are equal.
  /// </summary>
  /// <param name="direction">
  /// The direction in which to collapse the range or selection. Can be either of the following MoveDirection constants: Forward or Backward. The default value is Backward.
  /// </param>
  public void Collapse(MoveDirection? direction = MoveDirection.Backward);

  /// <summary>
  /// Expands the specified range or selection. Returns the number of elements added to the range or selection.
  /// </summary>
  /// <param name="unit">
  /// The unit by which to expand the range. Can be Element or Sibling only.
  /// If unit is Element, the range is expanded to include the next element.
  /// If unit is Sibling, the range is expanded to include the last sibling element.
  /// </param>
  /// <returns></returns>
  public int Expand(MoveUnits unit);

  /// <summary>
  /// Collapses the specified range to its start or end position and then moves the collapsed object by the specified number of units.
  /// </summary>
  /// <param name="unit"> The unit by which to move the range.</param>
  /// <param name="count">
  /// The number of units by which the specified range is to be moved.
  /// If Count is a positive number, the object is collapsed to its end position and moved backward in the document by the specified number of units.
  /// If Count is a negative number, the object is collapsed to its start position and moved forward by the specified number of units.
  /// The default value is 1.
  /// You can also control the collapse direction by using the Collapse method before using the Move method.
  /// If the range is in the middle of a unit or isn't collapsed, moving it to the beginning or end of the unit counts as moving it one full unit.
  /// </param>
  /// <result>
  /// This method returns an integer that indicates the number of units by which the object was actually moved, or it returns 0 (zero) if the move was unsuccessful.
  /// </result>
  public int Move(MoveUnits? unit, int? count=1);

  /// <summary>
  /// Moves the ending position of a range or selection.
  /// </summary>
  /// <param name="unit">The unit by which to move the ending character position.</param>
  /// <param name="count">
  /// The number of units to move.
  /// If this number is positive, the ending character position is moved forward in the document.
  /// If this number is negative, the end is moved backward.
  /// If the ending position overtakes the starting position, the range collapses and both positions move together.
  /// The default value is 1.</param>
  /// <returns>
  /// This method returns an integer that indicates the number of units the range actually moved, or it returns 0 (zero) if the move was unsuccessful.
  /// </returns>
  public int MoveEnd(MoveUnits? unit, int? count = 1);

  /// <summary>
  /// Moves the start position of the specified range.
  /// </summary>
  /// <param name="unit">The unit by which start position of the specified range is to be moved.</param>
  /// <param name="count">
  /// The maximum number of units by which the specified range is to be moved.
  /// If Count is a positive number, the start position of the range is moved forward in the document.
  /// If it is a negative number, the start position is moved backward.
  /// If the start position is moved forward to a position beyond the end position, the range is collapsed and both the start and end positions move together.
  /// The default value is 1.
  /// </param>
  /// <returns>
  /// This method returns an integer that indicates the number of units by which the start position or the range actually moved,
  /// or it returns 0 (zero) if the move was unsuccessful.
  /// </returns>
  public int MoveStart(MoveUnits? unit, int? count = 1);

  /// <summary>
  /// Moves the end position of the specified range until any of the specified types elements are found in the document. If the movement is forward in the document, the range is expanded.
  /// </summary>
  /// <param name="types">
  /// A set of element types to move the end position of the specified range until one of them is found in the document.
  /// It must be one of the DocumentFormat.OpenXml.OpenXmlElement types.
  /// It must not be an empty array.
  /// </param>
  /// <param name="direction">
  /// The direction in which to move the range or selection. Can be either of the following MoveDirection constants: Forward or Backward. The default value is Forward.
  /// </param>
  /// <param name="Count">
  /// The maximum number of elements by which the specified range is to be moved. The default value is int.MaxValue (unlimited).
  /// If the ending position overtakes the starting position, the range collapses and both positions move together.
  /// </param>
  public int MoveEndUntil(Type[] types, MoveDirection? direction=MoveDirection.Forward, int? Count= int.MaxValue);

  /// <summary>
  /// Moves the start position of the specified range until one of the specified types elements is found in the document.
  /// </summary>
  /// <param name="types">
  /// A set of element types to move the start position of the specified range until one of them is found in the document.
  /// It must be one of the DocumentFormat.OpenXml.OpenXmlElement types.
  /// It must not be an empty array.
  /// </param>
  /// <param name="direction">
  /// The direction in which to move the range or selection. Can be either of the following MoveDirection constants: Forward or Backward. The default value is Backward.
  /// </param>
  /// <param name="Count">
  /// The maximum number of elements by which the specified range is to be moved. The default value is int.MaxValue (unlimited).
  ///  If the start position is moved forward to a position beyond the end position, the range is collapsed and both the start and end positions move together.
  /// </param>
  /// <returns>
  /// This method returns the number of element by which the start position of the specified range moved.
  /// </returns>
  public int MoveStartUntil(Type[] types, MoveDirection? direction = MoveDirection.Backward, int? Count = int.MaxValue);

  /// <summary>
  /// Moves the end position of the specified range while any of the specified types elements are found in the document. If the movement is forward in the document, the range is expanded.
  /// </summary>
  /// <param name="types">
  /// A set of element types to move the end position of the specified range while one of them is found in the document.
  /// It must be one of the DocumentFormat.OpenXml.OpenXmlElement types.
  /// It must not be an empty array.
  /// </param>
  /// <param name="direction">
  /// The direction in which to move the range or selection. Can be either of the following MoveDirection constants: Forward or Backward. The default value is Backward.
  /// </param>
  /// <param name="Count">
  /// The maximum number of elements by which the specified range is to be moved. The default value is int.MaxValue (unlimited).
  /// If the ending position overtakes the starting position, the range collapses and both positions move together.
  /// </param>
  public int MoveEndWhile(Type[] types, MoveDirection? direction = MoveDirection.Forward, int? Count = int.MaxValue);

  /// <summary>
  /// Moves the start position of the specified range while one of the specified types elements is found in the document.
  /// </summary>
  /// <param name="types">
  /// A set of element types to move the start position of the specified range while one of them is found in the document.
  /// It must be one of the DocumentFormat.OpenXml.OpenXmlElement types.
  /// It must not be an empty array.
  /// </param>
  /// <param name="direction">
  /// The direction in which to move the range or selection. Can be either of the following MoveDirection constants: Forward or Backward. The default value is Backward.
  /// </param>
  /// <param name="Count">
  /// The maximum number of elements by which the specified range is to be moved. The default value is int.MaxValue (unlimited).
  ///  If the start position is moved forward to a position beyond the end position, the range is collapsed and both the start and end positions move together.
  /// </param>
  /// <returns>
  /// This method returns the number of element by which the start position of the specified range moved.
  /// </returns>
  public int MoveStartWhile(Type[] types, MoveDirection? direction = MoveDirection.Backward, int? Count = int.MaxValue);

  /// <summary>
  /// Collapses the range and moves the range until any of the specified types elements are found in the document.
  /// </summary>
  /// <param name="types">
  /// A set of element types to move the end position of the specified range until one of them is found in the document.
  /// It must be one of the DocumentFormat.OpenXml.OpenXmlElement types.
  /// It must not be an empty array.
  /// </param>
  /// <param name="direction">
  /// The direction in which to move the range or selection. Can be either of the following MoveDirection constants: Forward or Backward. The default value is Forward.
  /// </param>
  /// <param name="Count">
  /// The maximum number of elements by which the specified range is to be moved. The default value is int.MaxValue (unlimited).
  /// </param>
  public int MoveUntil(Type[] types, MoveDirection? direction = MoveDirection.Forward, int? Count = int.MaxValue);

  /// <summary>
  /// Collapses the range and moves the range while any of the specified types elements are found in the document.
  /// </summary>
  /// <param name="types">
  /// A set of element types to move the end position of the specified range while one of them is found in the document.
  /// It must be one of the DocumentFormat.OpenXml.OpenXmlElement types.
  /// It must not be an empty array.
  /// </param>
  /// <param name="direction">
  /// The direction in which to move the range or selection. Can be either of the following MoveDirection constants: Forward or Backward. The default value is Forward.
  /// </param>
  /// <param name="Count">
  /// The maximum number of elements by which the specified range is to be moved. The default value is int.MaxValue (unlimited).
  /// </param>
  public int MoveWhile(Type[] types, MoveDirection? direction = MoveDirection.Forward, int? Count = int.MaxValue);

  ///// <summary>
  ///// Moves or extends the ending position of a range to the end of the nearest specified unit.
  ///// </summary>
  ///// <param name="extent">
  ///// Specifies whether to move or extend the end of the range.
  ///// If the value is Move, both ends of the range or selection object are moved to the end of the specified unit.
  ///// If Extend is used, the end of the range or selection is extended to the end of the specified unit.
  ///// The default value wdMove.
  ///// </param>
  ///// <param name="unit"></param>
  //public void EndOf(MovementType extent, MoveUnits unit);


  ///// <summary>
  ///// Moves or extends the start position of the specified range or selection to the beginning of the nearest specified unit.
  ///// This method returns an integer that indicates the number of element by which the range or selection was moved or extended.
  ///// The method returns a negative number if the movement is backward through the document.
  ///// </summary>
  ///// <param name="extent">
  ///// Specifies whether to move or extend the start of the range.
  ///// If you use Move, both ends of the range or selection are moved to the beginning of the specified unit.
  ///// If you use Extend, the beginning of the range or selection is extended to the beginning of the specified unit.
  ///// The default value is Move.
  ///// </param>
  ///// <param name="unit"></param>
  //public void StartOf(MovementType extent, MoveUnits unit);

  /// <summary>
  /// Enumerates all the elements in the specified range.
  /// </summary>
  public IEnumerable GetElements();

  /// <summary>
  /// Enumerates the elements of the specified type in the specified range.
  /// </summary>
  public IEnumerable<T> GetElements<T>();

  /// <summary>
  /// Selects the specified range.
  /// </summary>
  public void Select();

  //AutoFormat
  //  Calculate
  //CheckGrammar
  //  CheckSpelling
  //CheckSynonyms
  //ComputeStatistics
  //  ConvertHangulAndHanja
  //ConvertToTable
  //  Copy
  //CopyAsPicture
  //  Cut
  //Delete
  //  DetectLanguage

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
  //  SetListLevel
  //SetRange
  //  Sort
  //SortAscending
  //  SortByHeadings
  //SortDescending

  //TCSCConverter
  //  WholeStory

  #endregion
}
