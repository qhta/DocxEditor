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

}
