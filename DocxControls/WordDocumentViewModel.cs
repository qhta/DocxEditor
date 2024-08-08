namespace DocxControls;

public class WordDocumentViewModel: IDisposable
{
  public WordDocumentViewModel(string filePath, bool isEditable)
  {
    Open(filePath, isEditable);
  }

  public DocumentFormat.OpenXml.Packaging.WordprocessingDocument WordDocument { get; set; } = null!;

  public void Open(string filePath, bool isEditable)
  {
    WordDocument = DocumentFormat.OpenXml.Packaging.WordprocessingDocument.Open(filePath, isEditable);
  }

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

  public PropertiesViewModel CustomProperties
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
  private PropertiesViewModel? _CustomProperties;

  #region Dispose pattern
  private bool _disposed = false;

  public void Dispose()
  {
    Dispose(true);
    GC.SuppressFinalize(this);
  }

  protected virtual void Dispose(bool disposing)
  {
    if (_disposed) return;

    if (disposing)
    {
      // Dispose managed resources here
    }

    // Dispose unmanaged resources here

    _disposed = true;
  }

  ~WordDocumentViewModel()
  {
    Dispose(false);
  }
  #endregion
}
