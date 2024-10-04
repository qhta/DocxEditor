namespace Docx.Automation;

/// <summary>
/// View model for a document.
/// </summary>
public interface Document: IElement
{
  #region properties ---------------------------------------------------------------------------------------------------------------------------
  /// <summary>
  /// Properties stored in CoreProperties part of the document.
  /// </summary>
  public DocumentProperties CoreProperties { get; }

  /// <summary>
  /// Properties stored in ExtendedFileProperties part of the document (except of StatProperties).
  /// </summary>
  public DocumentProperties AppProperties { get; }

  /// <summary>
  /// Properties stored in ExtendedFileProperties part of the document that represent document statistics.
  /// </summary>
  public DocumentProperties StatProperties { get; }

  /// <summary>
  /// Properties stored in CustomProperties part of the document.
  /// </summary>
  public CustomDocumentProperties CustomDocumentProperties { get;}

  /// <summary>
  /// Returns a Window object that represents the active window (the window with the focus) that contains the document.
  /// May be null if no window is active or the document is not opened in the active window.
  /// </summary>
  public DocumentWindow? ActiveWindow { get; }

  /// <summary>
  /// Returns a Range object that represents all the elements in the document.
  /// </summary>
  public Range Range { get; }

  /// <summary>
  /// Returns a Selection object that represents all selected elements in the document.
  /// </summary>
  public Selection Selection { get; }

  //ActiveTheme
  //ActiveThemeDisplayName

  //  ActiveWritingStyle

  //  AttachedTemplate
  //  AutoFormatOverride
  //  AutoHyphenation
  //  Background
  //  Bibliography
  //  Bookmarks
  //  Broadcast
  //  Characters
  //  ChartDataPointTrack
  //  ClickAndTypeParagraphStyle
  //  CoAuthoring
  //  CodeName
  //  CommandBars
  //  Comments
  //  Compatibility
  //  CompatibilityMode
  //  ConsecutiveHyphensLimit
  //  Container
  //  Content
  //  ContentControls
  //  ContentTypeProperties

  //  CurrentRsid

  //  CustomXMLParts
  //  DefaultTableStyle
  //  DefaultTabStop
  //  DefaultTargetFrame
  //  DisableFeatures
  //  DisableFeaturesIntroducedAfter
  //  DocumentInspectors
  //  DocumentLibraryVersions
  //  DocumentTheme
  //  DoNotEmbedSystemFonts
  //  Email
  //  EmbedLinguisticData
  //  EmbedTrueTypeFonts
  //  EncryptionProvider
  //  Endnotes
  //  EnforceStyle
  //  Envelope
  //  FarEastLineBreakLanguage
  //  FarEastLineBreakLevel
  //  Fields
  //  Final
  //  Footnotes
  //  FormattingShowClear
  //  FormattingShowFilter
  //  FormattingShowFont
  //  FormattingShowNextLevel
  //  FormattingShowNumbering
  //  FormattingShowParagraph
  //  FormattingShowUserStyleName
  //  FormFields
  //  FormsDesign
  //  Frames
  //  Frameset
  //  FullName
  //  GrammarChecked
  //  GrammaticalErrors
  //  GridDistanceHorizontal
  //  GridDistanceVertical
  //  GridOriginFromMargin
  //  GridOriginHorizontal
  //  GridOriginVertical
  //  GridSpaceBetweenHorizontalLines
  //  GridSpaceBetweenVerticalLines
  //  HasPassword
  //  HasVBProject
  //  HTMLDivisions
  //  Hyperlinks
  //  HyphenateCaps
  //  HyphenationZone
  //  Indexes
  //  InlineShapes
  //  IsInAutosave
  //  IsMasterDocument
  //  IsSubdocument
  //  JustificationMode
  //  KerningByAlgorithm
  //  Kind
  //  LanguageDetected
  //  ListParagraphs
  //  Lists
  //  ListTemplates
  //  LockQuickStyleSet
  //  LockTheme
  //  MailEnvelope
  //  MailMerge
  //  Name
  //  NoLineBreakAfter
  //  NoLineBreakBefore
  //  OMathBreakBin
  //  OMathBreakSub
  //  OMathFontName
  //  OMathIntSubSupLim
  //  OMathJc
  //  OMathLeftMargin
  //  OMathNarySupSubLim
  //  OMathRightMargin
  //  OMaths
  //  OMathSmallFrac
  //  OMathWrap
  //  OpenEncoding
  //  OptimizeForWord97
  //  OriginalDocumentTitle
  //  PageSetup
  //  Paragraphs
  //  Owner
  //  Password
  //  PasswordEncryptionAlgorithm
  //  PasswordEncryptionFileProperties
  //  PasswordEncryptionKeyLength
  //  PasswordEncryptionProvider
  //  Path
  //  Permission
  //  PrintFormsData
  //  PrintPostScriptOverText
  //  PrintRevisions
  //  ProtectionType
  //  ReadabilityStatistics
  //  ReadingLayoutSizeX
  //  ReadingLayoutSizeY
  //  ReadingModeLayoutFrozen
  //  ReadOnly
  //  ReadOnlyRecommended
  //  RemoveDateAndTime
  //  RemovePersonalInformation
  //  Research
  //  RevisedDocumentTitle
  //  Revisions
  //  Saved
  //  SaveEncoding
  //  SaveFormat
  //  SaveFormsData
  //  SaveSubsetFonts
  //  Scripts
  //  Sections
  //  SensitivityLabel
  //  Sentences
  //  ServerPolicy
  //  Shapes
  //  ShowGrammaticalErrors
  //  ShowSpellingErrors
  //  Signatures
  //  SmartDocument
  //  SnapToGrid
  //  SnapToShapes
  //  SpellingChecked
  //  SpellingErrors
  //  StoryRanges
  //  Styles
  //  StyleSheets
  //  StyleSortMethod
  //  Subdocuments
  //  Sync
  //  Tables
  //  TablesOfAuthorities
  //  TablesOfAuthoritiesCategories
  //  TablesOfContents
  //  TablesOfFigures
  //  TextEncoding
  //  TextLineEnding
  //  TrackFormatting
  //  TrackMoves
  //  TrackRevisions
  //  Type
  //  UpdateStylesOnOpen
  //  UseMathDefaults
  //  UserControl
  //  Variables
  //  VBASigned
  //  VBProject
  //  WebOptions
  //  Windows
  //  WordOpenXML
  //  Words
  //  WritePassword
  //  WriteReserved
  //  XMLSaveThroughXSLT
  //  XMLSchemaReferences
  //  XMLShowAdvancedErrors
  //  XMLUseXSLTWhenSaving
  #endregion

  #region methods ------------------------------------------------------------------------------------------------------------------------------

  /// <summary>
  /// Returns a Range object that represents all the elements in the document.
  /// </summary>
  /// <returns></returns>
  public Range GetRange();

  //AcceptAllRevisions
  //  AcceptAllRevisionsShown
  //Activate
  //  AddToFavorites
  //ApplyDocumentTheme
  //  ApplyQuickStyleSet2
  //ApplyTheme
  //  AutoFormat
  //CanCheckin
  //  CheckConsistency
  //CheckGrammar
  //  CheckIn
  //CheckInWithVersion
  //  CheckSpelling
  //Exit
  //  ClosePrintPreview
  //Compare
  //  ComputeStatistics
  //Convert
  //  ConvertAutoHyphens
  //ConvertNumbersToText
  //  ConvertVietDoc
  //CopyStylesFromTemplate
  //  CountNumberedItems
  //CreateLetterContent
  //  DataForm
  //DeleteAllComments
  //  DeleteAllCommentsShown
  //DeleteAllEditableRanges
  //  DeleteAllInkAnnotations
  //DetectLanguage
  //  DowngradeDocument
  //EndReview
  //  ExportAsFixedFormat
  //ExportAsFixedFormat2
  //  FitToPages
  //FollowHyperlink
  //  FreezeLayout
  //GetCrossReferenceItems
  //  GetLetterContent
  //GetWorkflowTasks
  //  GetWorkflowTemplates
  //GoTo
  //  LockServerFile
  //MakeCompatibilityDefault
  //  ManualHyphenation
  //Merge
  //  Post
  //PresentIt
  //  PrintOut
  //PrintPreview
  //  Protect

  //  Redo
  //RejectAllRevisions
  //  RejectAllRevisionsShown
  //Reload
  //  ReloadAs
  //RemoveDocumentInformation
  //  RemoveLockedStyles
  //RemoveNumbers
  //  RemoveTheme
  //Repaginate
  //  ReplyWithChanges
  //ResetFormFields
  //  ReturnToLastReadPosition
  //RunAutoMacro
  //  RunLetterWizard
  //Save
  //  SaveAs2
  //SaveAsQuickStyleSet
  //  Select
  //SelectAllEditableRanges
  //  SelectContentControlsByTag
  //SelectContentControlsByTitle
  //  SelectLinkedControls
  //SelectNodes
  //  SelectSingleNode
  //SelectUnlinkedControls
  //  SendFax
  //SendFaxOverInternet
  //  SendForReview
  //SendMail
  //  SetCompatibilityMode
  //SetDefaultTableStyle
  //  SetLetterContent
  //SetPasswordEncryptionOptions
  //  ToggleFormsDesign
  //TransformDocument
  //  Undo
  //UndoClear
  //  Unprotect
  //UpdateStyles
  //  ViewCode
  //ViewPropertyBrowser
  //  WebPagePreview
  #endregion

  #region events -------------------------------------------------------------------------------------------------------------------------------
  /// <summary>
  /// Occurs when a new document based is created.
  /// </summary>
  public event EventHandler Created;

  /// <summary>
  /// Occurs when the document is opened.
  /// </summary>
  public event EventHandler Opened;

  /// <summary>
  /// Occurs when the document is closed.
  /// </summary>
  public event EventHandler Closed;
  //  Open
  //  Exit

  //BuildingBlockInsert
  //ContentControlAfterAdd
  //  ContentControlBeforeContentUpdate
  //ContentControlBeforeDelete
  //  ContentControlBeforeStoreUpdate
  //ContentControlOnEnter
  //  ContentControlOnExit
  //Sync
  //  XMLAfterInsert
  //XMLBeforeDelete
  #endregion
}
