using System.IO;
using Qhta.MVVM;

namespace DocxControls;

/// <summary>
/// View model for a document.
/// </summary>
public class DocumentViewModel: ViewModel, IEditable
{
  /// <summary>
  /// Default constructor.
  /// </summary>
  public DocumentViewModel()
  {
  }

  /// <summary>
  /// Internal WordprocessingDocument object
  /// </summary>
  public DocumentFormat.OpenXml.Packaging.WordprocessingDocument WordDocument { get; set; } = null!;

  /// <summary>
  /// OpenDocument a wordprocessing document for viewing/editing.
  /// </summary>
  /// <param name="filePath"></param>
  /// <param name="isEditable"></param>
  public void OpenDocument(string filePath, bool isEditable)
  {
    FilePath = filePath;
    IsEditable = isEditable;
    if (isEditable)
    {
      TempFilePath = System.IO.Path.GetTempFileName();
      File.Copy(FilePath, TempFilePath, true);
      WordDocument = DocumentFormat.OpenXml.Packaging.WordprocessingDocument.Open(TempFilePath, isEditable);
    }
    else
    {
      WordDocument = DocumentFormat.OpenXml.Packaging.WordprocessingDocument.Open(filePath, isEditable);
    }
  }

  /// <summary>
  /// Creates a new wordprocessing document.
  /// </summary>
  public void NewDocument()
  {
    IsEditable = true;
    TempFilePath = System.IO.Path.GetTempFileName();
    WordDocument = DocumentFormat.OpenXml.Packaging.WordprocessingDocument.Create(TempFilePath, DX.WordprocessingDocumentType.Document);
  }

  /// <summary>
  /// Close the document.
  /// </summary>
  /// <param name="saveChanges">If <c>true</c> and <see cref="IsEditable"/> then temporary file is copied to the original file path</param>
  public void Close(bool saveChanges)
  {
    WordDocument.Dispose();
    if (TempFilePath != null)
    {
      if (IsEditable && FilePath!=null)
      {
        File.Copy(TempFilePath, FilePath, true);
      }
      File.Delete(TempFilePath);
    }
  }

  /// <summary>
  /// File path of the document.
  /// </summary>
  public string? FilePath
  {
    get => _filePath;
    private set
    {
      if (_filePath != value)
      {
        _filePath = value;
        NotifyPropertyChanged(nameof(FilePath));
        NotifyPropertyChanged(nameof(WindowTitle));
      }
    }
  }
  private string? _filePath = null;

  /// <summary>
  /// Flag indicating if the document is editable.
  /// </summary>
  public bool IsEditable { get; private set; }

  /// <summary>
  /// Flag indicating if the document is editable.
  /// </summary>
  public bool IsModified
  {
    get => _isModified;
    set
    {
      if (_isModified != value)
      {
        _isModified = value;
        NotifyPropertyChanged(nameof(IsModified));
        NotifyPropertyChanged(nameof(WindowTitle));
      }
    }
  }
  private bool _isModified;

  private string? TempFilePath;

  /// <summary>
  /// Title for the window
  /// </summary>
  public string WindowTitle
  {
    get
    {
      var result = FilePath ?? TempFilePath ?? String.Empty;
      if (!IsEditable)
      {
        result += $" [{Strings.ReadOnly}]";
      }
      else if (IsModified)
      {
        result += $" [{Strings.Modified}]";
      }
      return result;
    }
  }

  /// <summary>
  /// Access to the body elements of the document
  /// </summary>
  public BodyViewModel Body
  {
    get
    {
      if (_BodyElements == null)
      {
        _BodyElements = new BodyViewModel(this, WordDocument.GetBody());
      }
      return _BodyElements;
    }
  }

  private BodyViewModel? _BodyElements;

  /// <summary>
  /// Access to the core properties of the document
  /// </summary>
  // ReSharper disable once UnusedMember.Global
  public PropertiesViewModel CoreProperties
  {
    get
    {
      if (_CoreProperties == null)
      {
        _CoreProperties = new CorePropertiesViewModel(this);
      }
      return _CoreProperties;
    }
  }
  private PropertiesViewModel? _CoreProperties;

  /// <summary>
  /// Access to the application-specific properties of the document
  /// </summary>
  // ReSharper disable once UnusedMember.Global
  public PropertiesViewModel AppProperties
  {
    get
    {
      if (_AppProperties == null)
      {
        _AppProperties = new AppPropertiesViewModel(this);
      }
      return _AppProperties;
    }
  }
  private PropertiesViewModel? _AppProperties;


  /// <summary>
  /// Access to the statistics properties of the document
  /// </summary>
  // ReSharper disable once UnusedMember.Global
  public PropertiesViewModel StatProperties
  {
    get
    {
      if (_StatProperties == null)
      {
        _StatProperties = new StatPropertiesViewModel(this);
      }
      return _StatProperties;
    }
  }
  private PropertiesViewModel? _StatProperties;

  /// <summary>
  /// Access to the custom properties of the document
  /// </summary>
  // ReSharper disable once UnusedMember.Global
  public CustomPropertiesViewModel CustomProperties
  {
    get
    {
      if (_CustomProperties == null)
      {
        _CustomProperties = new CustomPropertiesViewModel(this);
      }
      return _CustomProperties;
    }
  }
  private CustomPropertiesViewModel? _CustomProperties;

  /// <summary>
  /// Access to the document settings in the LayoutAndUI categories
  /// </summary>
  // ReSharper disable once UnusedMember.Global
  public DocumentSettingsViewModel LayoutAndUISettings
  {
    get
    {
      if (_LayoutAndUISettings == null)
      {
        _LayoutAndUISettings = new DocumentSettingsViewModel(WordDocument, SettingCategoryGroup.LayoutAndUI);
      }
      return _LayoutAndUISettings;
    }
  }
  private DocumentSettingsViewModel? _LayoutAndUISettings;

  /// <summary>
  /// Access to the document settings in the Save, Load and Security categories
  /// </summary>
  // ReSharper disable once UnusedMember.Global
  public DocumentSettingsViewModel LoadSaveAndSecuritySettings
  {
    get
    {
      if (_SaveLoadAndSecuritySettings == null)
      {
        _SaveLoadAndSecuritySettings = new DocumentSettingsViewModel(WordDocument, SettingCategoryGroup.LoadSaveAndSecurity);
      }
      return _SaveLoadAndSecuritySettings;
    }
  }
  private DocumentSettingsViewModel? _SaveLoadAndSecuritySettings;

  /// <summary>
  /// Access to the document settings in the Proofing, Hyphenation and Automation categories
  /// </summary>
  // ReSharper disable once UnusedMember.Global
  public DocumentSettingsViewModel ProofingHyphenationAndAutomationSettings
  {
    get
    {
      if (_ProofingHyphenationAndAutomationSettings == null)
      {
        _ProofingHyphenationAndAutomationSettings = new DocumentSettingsViewModel(WordDocument, SettingCategoryGroup.ProofingHyphenationAndAutomation);
      }
      return _ProofingHyphenationAndAutomationSettings;
    }
  }
  private DocumentSettingsViewModel? _ProofingHyphenationAndAutomationSettings;

  /// <summary>
  /// Access to the document settings in the MailMerge and Printing categories
  /// </summary>
  // ReSharper disable once UnusedMember.Global
  public DocumentSettingsViewModel MailMergeAndPrintingSettings
  {
    get
    {
      if (_MailMergeAndPrintingSettings == null)
      {
        _MailMergeAndPrintingSettings = new DocumentSettingsViewModel(WordDocument, SettingCategoryGroup.MailMergeAndPrinting);
      }
      return _MailMergeAndPrintingSettings;
    }
  }
  private DocumentSettingsViewModel? _MailMergeAndPrintingSettings;

  /// <summary>
  /// Access to the document settings in the Theming and Styles categories
  /// </summary>
  // ReSharper disable once UnusedMember.Global
  public DocumentSettingsViewModel ThemingAndStylesSettings
  {
    get
    {
      if (_ThemingAndStylesSettings == null)
      {
        _ThemingAndStylesSettings = new DocumentSettingsViewModel(WordDocument, SettingCategoryGroup.ThemingAndStyles);
      }
      return _ThemingAndStylesSettings;
    }
  }
  private DocumentSettingsViewModel? _ThemingAndStylesSettings;

  /// <summary>
  /// Access to the document settings in the Revisions and Tracking categories
  /// </summary>
  // ReSharper disable once UnusedMember.Global
  public DocumentSettingsViewModel RevisionsAndTrackingSettings
  {
    get
    {
      if (_RevisionsAndTrackingSettings == null)
      {
        _RevisionsAndTrackingSettings = new DocumentSettingsViewModel(WordDocument, SettingCategoryGroup.RevisionsAndTracking);
      }
      return _RevisionsAndTrackingSettings;
    }
  }
  private DocumentSettingsViewModel? _RevisionsAndTrackingSettings;

  /// <summary>
  /// Access to the document settings in the Identification category
  /// </summary>
  // ReSharper disable once UnusedMember.Global
  public DocumentSettingsViewModel IdentificationSettings
  {
    get
    {
      if (_IdentificationSettings == null)
      {
        _IdentificationSettings = new DocumentSettingsViewModel(WordDocument, SettingCategoryGroup.Identification);
      }
      return _IdentificationSettings;
    }
  }
  private DocumentSettingsViewModel? _IdentificationSettings;

  /// <summary>
  /// Access to the document settings in the CustomXml category
  /// </summary>
  // ReSharper disable once UnusedMember.Global
  public DocumentSettingsViewModel CustomXmlSettings
  {
    get
    {
      if (_CustomXmlSettings == null)
      {
        _CustomXmlSettings = new DocumentSettingsViewModel(WordDocument, SettingCategoryGroup.CustomXml);
      }
      return _CustomXmlSettings;
    }
  }
  private DocumentSettingsViewModel? _CustomXmlSettings;

  /// <summary>
  /// Access to the document settings in the Compatibility category
  /// </summary>
  // ReSharper disable once UnusedMember.Global
  public DocumentSettingsViewModel CompatibilitySettings
  {
    get
    {
      if (_CompatibilitySettings == null)
      {
        _CompatibilitySettings = new DocumentSettingsViewModel(WordDocument, SettingCategoryGroup.Compatibility);
      }
      return _CompatibilitySettings;
    }
  }
  private DocumentSettingsViewModel? _CompatibilitySettings;

  /// <summary>
  /// Access to the document bookmarks collection
  /// </summary>
  // ReSharper disable once UnusedMember.Global
  public BookmarksViewModel Bookmarks
  {
    get
    {
      if (_Bookmarks == null)
      {
        _Bookmarks = new BookmarksViewModel(WordDocument);
      }
      return _Bookmarks;
    }
  }
  private BookmarksViewModel? _Bookmarks;

}
