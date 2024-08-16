namespace DocxControls;

/// <summary>
/// View model for a document.
/// </summary>
public class DocumentViewModel
{
  /// <summary>
  /// Initializes a new instance of the <see cref="DocumentViewModel"/> class.
  /// </summary>
  /// <param name="filePath"></param>
  /// <param name="isEditable"></param>
  public DocumentViewModel(string filePath, bool isEditable)
  {
    Open(filePath, isEditable);
  }

  /// <summary>
  /// Internal WordprocessingDocument object
  /// </summary>
  public DocumentFormat.OpenXml.Packaging.WordprocessingDocument WordDocument { get; set; } = null!;

  /// <summary>
  /// Open a document for viewing/editing.
  /// </summary>
  /// <param name="filePath"></param>
  /// <param name="isEditable"></param>
  public void Open(string filePath, bool isEditable)
  {
    WordDocument = DocumentFormat.OpenXml.Packaging.WordprocessingDocument.Open(filePath, isEditable);
  }

  /// <summary>
  /// Access to the body elements of the document
  /// </summary>
  public BodyElementsViewModel BodyElements
  {
    get
    {
      if (_BodyElements == null)
      {
        _BodyElements = new BodyElementsViewModel(WordDocument);
      }
      return _BodyElements;
    }
  }
  private BodyElementsViewModel? _BodyElements;

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
        _CoreProperties = new CorePropertiesViewModel(WordDocument);
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
        _AppProperties = new AppPropertiesViewModel(WordDocument);
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
        _StatProperties = new StatPropertiesViewModel(WordDocument);
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
        _CustomProperties = new CustomPropertiesViewModel(WordDocument);
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

}
