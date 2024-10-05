namespace Docx.Automation;

/// <summary>
/// Represents the DocxEditor application.
/// </summary>
public interface Application
{
  #region properties

  /// <summary>
  /// Returns an active document from the <see cref="ActiveWindow"/>.
  /// May be null if no window is active.
  /// </summary>
  public Document? ActiveDocument { get; }

  /// <summary>
  /// Returns a Window object that represents the active window (the window with the focus).
  /// May be null if no window is active.
  /// </summary>
  public DocumentWindow? ActiveWindow { get; }

  /// <summary>
  /// Collection of documents.
  /// </summary>
  public Documents Documents { get; }

  /// <summary>
  /// Collection of document windows.
  /// </summary>
  public DocumentWindows Windows { get; }

  /// <summary>
  /// Collection of plugins.
  /// </summary>
  public IEnumerable<Plugin> Plugins { get; }

  //ActiveDocument
  //ActiveEncryptionSession
  //  ActivePrinter
  //  ActiveProtectedViewWindow

  //  AddIns
  //  Application
  //  ArbitraryXMLSupportAvailable
  //  Assistance
  //  AutoCaptions
  //  AutoCorrect
  //  AutoCorrectEmail
  //  AutomationSecurity
  //  BackgroundPrintingStatus
  //  BackgroundSavingStatus
  //  Bibliography
  //  BrowseExtraFileTypes
  //  Browser
  //  Build
  //  CapsLock
  //  Caption
  //  CaptionLabels
  //  ChartDataPointTrack
  //  CheckLanguage
  //  COMAddIns
  //  CommandBars
  //  Creator
  //  CustomDictionaries
  //  CustomizationContext
  //  DefaultLegalBlackline
  //  DefaultSaveFormat
  //  DefaultTableSeparator
  //  Dialogs
  //  DisplayAlerts
  //  DisplayAutoCompleteTips
  //  DisplayDocumentInformationPanel
  //  DisplayRecentFiles
  //  DisplayScreenTips
  //  DisplayScrollBars
  //  DontResetInsertionPointProperties
  //  EmailOptions
  //  EmailTemplate
  //  EnableCancelKey
  //  FeatureInstall
  //  FileConverters
  //  FileDialog
  //  FileValidation
  //  FindKey
  //  FocusInMailHeader
  //  FontNames
  //  HangulHanjaDictionaries
  //  Height
  //  International
  //  IsObjectValid
  //  IsSandboxed
  //  KeyBindings
  //  KeysBoundTo
  //  LandscapeFontNames
  //  Language
  //  Languages
  //  LanguageSettings
  //  Left
  //  ListGalleries
  //  MacroContainer
  //  MailingLabel
  //  MailMessage
  //  MailSystem
  //  MAPIAvailable
  //  MathCoprocessorAvailable
  //  MouseAvailable
  //  Name
  //  NewDocument
  //  NormalTemplate
  //  NumLock
  //  OMathAutoCorrect
  //  OpenAttachmentsInFullScreen
  //  Options
  //  Owner
  //  Path
  //  PathSeparator
  //  PickerDialog
  //  PortraitFontNames
  //  PrintPreview
  //  ProtectedViewWindows
  //  RecentFiles
  //  RestrictLinkedStyles
  //  ScreenUpdating
  //  Selection
  //  SensitivityLabelPolicy
  //  ShowAnimation
  //  ShowStartupDialog
  //  ShowStylePreviews
  //  ShowVisualBasicEditor
  //  SmartArtColors
  //  SmartArtLayouts
  //  SmartArtQuickStyles
  //  SpecialMode
  //  StartupPath
  //  StatusBar
  //  SynonymInfo
  //  System
  //  TaskPanes
  //  Tasks
  //  Templates
  //  Top
  //  UndoRecord
  //  UsableHeight
  //  UsableWidth
  //  UserAddress
  //  UserControl
  //  UserInitials
  //  UserName
  //  VBE
  //  Version
  //  Visible
  //  Width
  //  WindowState
  //  WordBasic
  //  XMLNamespaces
  #endregion

  #region methods

  /// <summary>
  /// Closes the application.
  /// </summary>
  public void Exit();

  /// <summary>
  /// Activates the specified window.
  /// </summary>
  /// <param name="window"></param>
  public void Activate(DocumentWindow window);

  /// <summary>
  /// Creates a new document in a new window.
  /// </summary>
  /// <returns></returns>
  public Document NewDocument();

  /// <summary>
  /// Creates a new window for existing document.
  /// </summary>
  public DocumentWindow CreateNewWindow(Document document);

  /// <summary>
  /// Closes all the documents in the application. Returns true if successful.
  /// </summary>
  /// <returns></returns>
  public bool CloseAllDocuments();

  /// <summary>
  /// Registers plugin commands with the application. Returns true if successful.
  /// </summary>
  public bool LoadPlugins();

  //  AddAddress
  //AutomaticChange
  //  BuildKeyCode
  //CentimetersToPoints
  //  ChangeFileOpenDirectory
  //CheckGrammar
  //  CheckSpelling
  //CleanString
  //  CompareDocuments
  //DDEExecute
  //  DDEInitiate
  //DDEPoke
  //  DDERequest
  //DDETerminate
  //  DDETerminateAll
  //DefaultWebOptions
  //  GetAddress
  //GetDefaultTheme
  //  GetSpellingSuggestions
  //GoBack
  //  GoForward
  //Help
  //  HelpTool
  //InchesToPoints
  //  Keyboard
  //KeyboardBidi
  //  KeyboardLatin
  //KeyString
  //  LinesToPoints
  //ListCommands
  //  LoadMasterList
  //LookupNameProperties
  //  MergeDocuments
  //MillimetersToPoints
  //  Move
  //  NextLetter
  //OnTime
  //  OrganizerCopy
  //OrganizerDelete
  //  OrganizerRename
  //PicasToPoints
  //  PixelsToPoints
  //PointsToCentimeters
  //  PointsToInches
  //PointsToLines
  //  PointsToMillimeters
  //PointsToPicas
  //  PointsToPixels
  //PrintOut
  //  ProductCode
  //PutFocusInMailHeader
  //  BeforeExit
  //Repeat
  //  ResetIgnoreAll
  //Resize
  //  Run
  //ScreenRefresh
  //  SetDefaultTheme
  //ShowClipboard
  //  ShowMe
  //SubstituteFont
  //  ToggleKeyboard
  #endregion

  #region events
  /// <summary>
  /// Occurs when the application is about to quit.
  /// </summary>
  public event EventHandler? BeforeExit;

  /// <summary>
  /// Occurs when a new document is created.
  /// </summary>
  public event DocumentEventHandler? DocumentCreated;

  /// <summary>
  /// Occurs when a document is opened.
  /// </summary>
  public event DocumentEventHandler? DocumentOpened;

  /// <summary>
  /// Occurs when a new document is created, when an existing document is opened,
  /// or when another document is made the active document.
  /// </summary>
  public event EventHandler? DocumentChanged;

  /// <summary>
  /// Occurs immediately before any open document is saved.
  /// </summary>
  public event DocumentSaveEventHandler? DocumentBeforeSave;

  /// <summary>
  /// Occurs immediately before any open document closes.
  /// </summary>
  public event DocumentCloseEventHandler? DocumentBeforeClose;

  //  DocumentBeforePrint
  //  DocumentSync

  /// <summary>
  /// Occurs when any document window is activated.
  /// </summary>
  public event DocumentWindowEventHandler? WindowActivated;

  /// <summary>
  /// Occurs when any document window is deactivated.
  /// </summary>
  public event DocumentWindowEventHandler? WindowDeactivated;

  //WindowBeforeDoubleClick
  //WindowBeforeRightClick
  //WindowSelectionChange
  //WindowSize

  //EPostageInsert
  //  EPostageInsertEx
  //EPostagePropertyDialog
  //  MailMergeAfterMerge
  //MailMergeAfterRecordMerge
  //  MailMergeBeforeMerge
  //MailMergeBeforeRecordMerge
  //  MailMergeDataSourceLoad
  //MailMergeDataSourceValidate
  //  MailMergeDataSourceValidate2
  //MailMergeWizardSendToCustom
  //  MailMergeWizardStateChange
  //  ProtectedViewWindowActivate
  //ProtectedViewWindowBeforeClose
  //  ProtectedViewWindowBeforeEdit
  //ProtectedViewWindowDeactivate
  //  ProtectedViewWindowOpen
  //ProtectedViewWindowSize


  //XMLSelectionChange
  //  XMLValidationError

  #endregion
}
