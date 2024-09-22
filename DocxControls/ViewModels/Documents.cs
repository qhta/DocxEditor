namespace DocxControls.ViewModels;

/// <summary>
/// Collection of documents.
/// </summary>
public class Documents: ElementCollection<Document>, DA.Documents
{
  /// <summary>
  /// Default constructor.
  /// </summary>
  public Documents(): base(null)
  {
  }

  /// <summary>
  /// Initializes a new instance of the Documents class.
  /// </summary>
  /// <param name="parent"></param>
  public Documents(object? parent) : base(parent)
  {

  }

  /// <summary>
  /// Adds a new document to the Documents collection.
  /// </summary>
  /// <param name="template">The full filename of the template to be used for the new document. If this argument is omitted, the Normal.dotm template is used.</param>
  /// <param name="newTemplate">True to open the document as a template. The default value is False.</param>
  /// <param name="visible">True to open the document in a visible window. The default value is True.</param>
  public void Add(string? template = null, bool newTemplate = false, bool visible = true)
  {

  }

  /// <summary>
  /// Opens a document and add it to the Documents collection.
  /// </summary>
  /// <param name="fileName">The full filename of the document.</param>
  /// <param name="readOnly">True to open the document as read-only. The default value is False.</param>
  /// <param name="visible">True to open the document in a visible window. The default value is True.</param>
  public void Open(string fileName, bool readOnly = false, bool visible = true)
  {
    var documentViewModel = new Document();
    documentViewModel.OpenDocument(fileName, !readOnly);
    Items.Add(documentViewModel);
  }

  /// <summary>
  /// Saves all the documents in the Documents collection.
  /// </summary>
  /// <param name="noPropmt">True to automatically save all documents. False to prompt the user to save each document that has changed since it was last saved.</param>
  public void Save(bool noPropmt = false)
  {

  }

  /// <summary>
  /// Closes all the documents in the Documents collection.
  /// </summary>
  /// <param name="SaveChanges">Specifies the save action for the document. Can be one of the following WdSaveOptions constants: wdDoNotSaveChanges, wdPromptToSaveChanges, or wdSaveChanges.</param>
  public void Close(DA.SaveOptions SaveChanges = DA.SaveOptions.PromptToSaveChanges)
  {

  }

  // ReSharper disable once NotDisposedResourceIsReturned
  IEnumerator<DA.Document> IEnumerable<DA.Document>.GetEnumerator() => Items.Cast<DA.Document>().GetEnumerator();
}

