﻿using System.IO;
using DocxControls.Views;

namespace DocxControls.ViewModels;

/// <summary>
/// View model for a document.
/// </summary>
public class Document : ViewModel, DA.Document, IEditable
{
  /// <summary>
  /// Default constructor.
  /// </summary>
  public Document()
  {
    Selection = new Selection(this);
  }

  #region IElement implementation
  /// <summary>
  /// Returns the application object that represents the DocxEditor application.
  /// </summary>
  public DA.Application Application => DocxControls.Application.Instance;

  object? DA._Element.Parent => Owner;
  #endregion

  /// <summary>
  /// Returns the owner object for the specified object.
  /// </summary>
  public object? Owner => null;

  /// <summary>
  /// Internal WordprocessingDocument object
  /// </summary>
  public DXPack.WordprocessingDocument WordDocument { get; set; } = null!;

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
      TempFilePath = Path.GetTempFileName();
      File.Copy(FilePath, TempFilePath, true);
      WordDocument = DXPack.WordprocessingDocument.Open(TempFilePath, isEditable);
    }
    else
    {
      WordDocument = DXPack.WordprocessingDocument.Open(filePath, isEditable);
    }
    Opened?.Invoke(this, EventArgs.Empty);
  }

  /// <summary>
  /// Creates a new wordprocessing document.
  /// </summary>
  public void NewDocument()
  {
    IsEditable = true;
    TempFilePath = Path.GetTempFileName();
    WordDocument = DXPack.WordprocessingDocument.Create(TempFilePath, DX.WordprocessingDocumentType.Document);
    Created?.Invoke(this, EventArgs.Empty);
  }

  /// <summary>
  /// Exit the document.
  /// </summary>
  /// <param name="saveChanges">If <c>true</c> and <see cref="IsEditable"/> then temporary file is copied to the original file path</param>
  public void Close(bool saveChanges)
  {
    WordDocument.Dispose();
    if (TempFilePath != null && File.Exists(TempFilePath))
    {
      if (saveChanges && IsEditable && FilePath != null)
      {
        File.Copy(TempFilePath, FilePath, true);
      }
      File.Delete(TempFilePath);
    }
    Closed?.Invoke(this, EventArgs.Empty);
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
  private string? _filePath;

  #region IEditable implementation
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

  /// <summary>
  /// Flag indicating if the document is modified internally.
  /// </summary>
  public bool IsModifiedInternal { get; set; }
  #endregion

  private string? TempFilePath;

  DA.DocumentWindow? DA.Document.ActiveWindow => ActiveWindow;

  /// <summary>
  /// Returns a Window object that represents the active window (the window with the focus).
  /// </summary>
  public DocumentWindow? ActiveWindow
  {
    get
    {
      var window = DocxControls.Application.Instance.ActiveWindow;
      if (window?.Document == this)
        return window;
      return null;
    }
  }

  /// <summary>
  /// Selects the entire document.
  /// </summary>
  public void SelectAll()
  {
    Body.LoadAllElements();
    Body.SelectAll();
  }

  DA.Range DA.Document.Range => Range;
  /// <summary>
  /// Returns a Range object that represents all the elements in the document.
  /// </summary>
  public Range Range => GetRange();


  DA.Range DA.Document.GetRange() => GetRange();
  /// <summary>
  /// Returns a Range object that represents all the elements in the document.
  /// </summary>
  public Range GetRange()
  {
    var body = Body;
    return new Range(this, body!.FirstChild!, body!.LastChild!);
  }

  IEnumerable<DA.Section> DA.Document.Sections => Sections;
  /// <summary>
  /// Returns a collection of all the sections in the document.
  /// </summary>
  /// <returns></returns>
  public IEnumerable<Section> Sections => Body.Sections;

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

  DA.Selection DA.Document.Selection => Selection;
  /// <summary>
  /// Collection of selected items.
  /// </summary>
  public Selection Selection { get; }

  /// <summary>
  /// Access to the body elements of the document
  /// </summary>
  public Body Body
  {
    get
    {
      if (_BodyElements == null)
      {
        _BodyElements = new Body(this, WordDocument.GetBody());
      }
      return _BodyElements;
    }
  }

  private Body? _BodyElements;

  #region DocumentProperties
  DA.DocumentProperties DA.Document.AppProperties => AppProperties;

  DA.DocumentProperties DA.Document.StatProperties => StatProperties;

  DA.CustomDocumentProperties DA.Document.CustomDocumentProperties => CustomProperties;

  DA.DocumentProperties DA.Document.CoreProperties => CoreProperties;

  /// <summary>
  /// Access to the core properties of the document
  /// </summary>
  // ReSharper disable once UnusedMember.Global
  public CoreProperties CoreProperties
  {
    get
    {
      if (_CoreProperties == null)
      {
        _CoreProperties = new CoreProperties(this);
      }
      return _CoreProperties;
    }
  }
  private CoreProperties? _CoreProperties;

  /// <summary>
  /// Access to the application-specific properties of the document
  /// </summary>
  // ReSharper disable once UnusedMember.Global
  public AppProperties AppProperties
  {
    get
    {
      if (_AppProperties == null)
      {
        _AppProperties = new AppProperties(this);
      }
      return _AppProperties;
    }
  }
  private AppProperties? _AppProperties;


  /// <summary>
  /// Access to the statistics properties of the document
  /// </summary>
  // ReSharper disable once UnusedMember.Global
  public StatProperties StatProperties
  {
    get
    {
      if (_StatProperties == null)
      {
        _StatProperties = new StatProperties(this);
      }
      return _StatProperties;
    }
  }
  private StatProperties? _StatProperties;

  /// <summary>
  /// Access to the custom properties of the document
  /// </summary>
  // ReSharper disable once UnusedMember.Global
  public CustomDocumentProperties CustomProperties
  {
    get
    {
      if (_CustomProperties == null)
      {
        _CustomProperties = new CustomDocumentProperties(this);
      }
      return _CustomProperties;
    }
  }
  private CustomDocumentProperties? _CustomProperties;
  #endregion

  /// <summary>
  /// Access to the document settings in the LayoutAndUI categories
  /// </summary>
  // ReSharper disable once UnusedMember.Global
  public DocumentSettings LayoutAndUISettings
  {
    get
    {
      if (_LayoutAndUISettings == null)
      {
        _LayoutAndUISettings = new DocumentSettings(this, SettingCategoryGroup.LayoutAndUI);
      }
      return _LayoutAndUISettings;
    }
  }
  private DocumentSettings? _LayoutAndUISettings;

  /// <summary>
  /// Access to the document settings in the Save, Load and Security categories
  /// </summary>
  // ReSharper disable once UnusedMember.Global
  public DocumentSettings LoadSaveAndSecuritySettings
  {
    get
    {
      if (_SaveLoadAndSecuritySettings == null)
      {
        _SaveLoadAndSecuritySettings = new DocumentSettings(this, SettingCategoryGroup.LoadSaveAndSecurity);
      }
      return _SaveLoadAndSecuritySettings;
    }
  }
  private DocumentSettings? _SaveLoadAndSecuritySettings;

  /// <summary>
  /// Access to the document settings in the Proofing, Hyphenation and Automation categories
  /// </summary>
  // ReSharper disable once UnusedMember.Global
  public DocumentSettings ProofingHyphenationAndAutomationSettings
  {
    get
    {
      if (_ProofingHyphenationAndAutomationSettings == null)
      {
        _ProofingHyphenationAndAutomationSettings = new DocumentSettings(this, SettingCategoryGroup.ProofingHyphenationAndAutomation);
      }
      return _ProofingHyphenationAndAutomationSettings;
    }
  }
  private DocumentSettings? _ProofingHyphenationAndAutomationSettings;

  /// <summary>
  /// Access to the document settings in the MailMerge and Printing categories
  /// </summary>
  // ReSharper disable once UnusedMember.Global
  public DocumentSettings MailMergeAndPrintingSettings
  {
    get
    {
      if (_MailMergeAndPrintingSettings == null)
      {
        _MailMergeAndPrintingSettings = new DocumentSettings(this, SettingCategoryGroup.MailMergeAndPrinting);
      }
      return _MailMergeAndPrintingSettings;
    }
  }
  private DocumentSettings? _MailMergeAndPrintingSettings;

  /// <summary>
  /// Access to the document settings in the Theming and Styles categories
  /// </summary>
  // ReSharper disable once UnusedMember.Global
  public DocumentSettings ThemingAndStylesSettings
  {
    get
    {
      if (_ThemingAndStylesSettings == null)
      {
        _ThemingAndStylesSettings = new DocumentSettings(this, SettingCategoryGroup.ThemingAndStyles);
      }
      return _ThemingAndStylesSettings;
    }
  }
  private DocumentSettings? _ThemingAndStylesSettings;

  /// <summary>
  /// Access to the document settings in the Revisions and Tracking categories
  /// </summary>
  // ReSharper disable once UnusedMember.Global
  public DocumentSettings RevisionsAndTrackingSettings
  {
    get
    {
      if (_RevisionsAndTrackingSettings == null)
      {
        _RevisionsAndTrackingSettings = new DocumentSettings(this, SettingCategoryGroup.RevisionsAndTracking);
      }
      return _RevisionsAndTrackingSettings;
    }
  }
  private DocumentSettings? _RevisionsAndTrackingSettings;

  /// <summary>
  /// Access to the document settings in the Identification category
  /// </summary>
  // ReSharper disable once UnusedMember.Global
  public DocumentSettings IdentificationSettings
  {
    get
    {
      if (_IdentificationSettings == null)
      {
        _IdentificationSettings = new DocumentSettings(this, SettingCategoryGroup.Identification);
      }
      return _IdentificationSettings;
    }
  }
  private DocumentSettings? _IdentificationSettings;

  /// <summary>
  /// Access to the document settings in the CustomXml category
  /// </summary>
  // ReSharper disable once UnusedMember.Global
  public DocumentSettings CustomXmlSettings
  {
    get
    {
      if (_CustomXmlSettings == null)
      {
        _CustomXmlSettings = new DocumentSettings(this, SettingCategoryGroup.CustomXml);
      }
      return _CustomXmlSettings;
    }
  }
  private DocumentSettings? _CustomXmlSettings;

  /// <summary>
  /// Access to the document settings in the Compatibility category
  /// </summary>
  // ReSharper disable once UnusedMember.Global
  public DocumentSettings CompatibilitySettings
  {
    get
    {
      if (_CompatibilitySettings == null)
      {
        _CompatibilitySettings = new DocumentSettings(this, SettingCategoryGroup.Compatibility);
      }
      return _CompatibilitySettings;
    }
  }
  private DocumentSettings? _CompatibilitySettings;

  /// <summary>
  /// Access to the document bookmarks collection
  /// </summary>
  // ReSharper disable once UnusedMember.Global
  public Bookmarks Bookmarks
  {
    get
    {
      if (_Bookmarks == null)
      {
        _Bookmarks = new Bookmarks(Body);
      }
      return _Bookmarks;
    }
  }
  private Bookmarks? _Bookmarks;
  //private DA.DocumentWindow? _activeWindow;

  #region events implementation

  /// <inheritdoc />
  public event EventHandler? Created;

  /// <inheritdoc />
  public event EventHandler? Opened;

  /// <inheritdoc />
  public event EventHandler? Closed;
  #endregion

  /// <summary>
  /// Not implemented
  /// </summary>
  /// <returns></returns>
  /// <exception cref="NotImplementedException"></exception>
  public bool Remove()
  {
    return false;
    throw new NotImplementedException();
  }
}
