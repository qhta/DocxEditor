namespace Docx.Automation;

/// <summary>
/// Options for saving a document.
/// </summary>
public enum SaveOptions
{
  /// <summary>
  /// Save the document without prompting the user.
  /// </summary>
  SaveChanges = -1,
  /// <summary>
  /// Do not save the document.
  /// </summary>
  DoNotSaveChanges = 0,
  /// <summary>
  /// Prompt the user to save the document.
  /// </summary>
  PromptToSaveChanges = 2,

}
