namespace Docx.Automation;

/// <summary>
/// Collection of documents.
/// </summary>
public interface Documents: IElementCollection<Document>
{

  /// <summary>
  /// Adds a new Document to the Documents collection.
  /// </summary>
  /// <param name="template">The full filename of the template to be used for the new document. </param>
  /// <param name="newTemplate">True to open the document as a template. The default value is False.</param>
  /// <param name="visible">True to open the document in a visible window. The default value is True.</param>
  public void Add(string? template = null, bool newTemplate = false, bool visible = true);

  /// <summary>
  /// Opens a Document and adds it to the Documents collection.
  /// </summary>
  /// <param name="fileName">The full filename of the document.</param>
  /// <param name="readOnly">True to open the document as read-only. The default value is False.</param>
  /// <param name="visible">True to open the document in a visible window. The default value is True.</param>
  public void Open(string fileName, bool readOnly = false, bool visible = true);

  /// <summary>
  /// Saves all the documents in the Documents collection.
  /// </summary>
  /// <param name="noPropmt">True to automatically save all documents. False to prompt the user to save each document that has changed since it was last saved.</param>
  public void Save(bool noPropmt = false);

  /// <summary>
  /// Closes all the documents in the Documents collection.
  /// </summary>
  /// <param name="SaveChanges">Specifies the save action for the document. Can be one of the following WdSaveOptions constants: wdDoNotSaveChanges, wdPromptToSaveChanges, or wdSaveChanges.</param>
  public void Close(SaveOptions SaveChanges = SaveOptions.PromptToSaveChanges);

}

