namespace Docx.Automation;
/// <summary>
/// Represents a selection in the document.
/// </summary>
public interface Selection: IEnumerable<IElement>
{
  /// <summary>
  /// Gets the application object that represents the DocxControls application.
  /// </summary>
  public Application Application { get; }

  /// <summary>
  /// Gets the owner document.
  /// </summary>
  public Document Document { get; }

  /// <summary>
  /// Checks if the selection is empty
  /// </summary>
  public bool IsEmpty { get; }

  /// <summary>
  /// Collection of selected paragraphs.
  /// </summary>
  public IEnumerable<Paragraph> Paragraphs { get; }
}
