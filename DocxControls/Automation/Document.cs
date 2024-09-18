namespace DocxControls.Automation;

/// <summary>
/// View model for a document.
/// </summary>
public interface Document: IElement, IEditable
{

  //ActiveTheme
  //ActiveThemeDisplayName
  //  ActiveWindow
  //  ActiveWritingStyle

  //  AttachedTemplate
  //  AutoFormatOverride
  //  AutoHyphenation
  //  Background
  //  Bibliography
  //  Bookmarks
  //  Broadcast
  //  BuiltInDocumentProperties
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
  //  CustomDocumentProperties
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
  //  Parent
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
  //Close
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
  //Range
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

}
