using System.Collections.ObjectModel;

namespace DocxControls.ViewModels;

/// <summary>
/// A set of selected items.
/// </summary>
public class Selection : IEnumerable<ElementViewModel>, DA.Selection
{
  /// <summary>
  /// Initializing constructor.
  /// </summary>
  /// <param name="document"></param>
  public Selection(VM.Document document)
  {
    Application = document.Application;
    Document = document;
  }

  /// <summary>
  /// Returns an Application object that represents the DocxControls application.
  /// </summary>
  public DA.Application Application { get; }

  DA.Document DA.Selection.Document => Document;
  /// <summary>
  /// Owner document.
  /// </summary>
  public VM.Document Document { get; }

  IEnumerator<ElementViewModel> IEnumerable<ElementViewModel>.GetEnumerator()
  {
    return Document.Body.GetSelectedItems().GetEnumerator();
  }

  /// <summary>
  /// Enumerates the selected items.
  /// </summary>
  /// <returns></returns>
  public IEnumerator<DA._Element> GetEnumerator()
  {
    return Document.Body.GetSelectedItems().GetEnumerator();
  }

  /// <summary>
  /// Checks if the selection is empty
  /// </summary>
  public bool IsEmpty => Document.Body.IsEmpty;

  IEnumerator IEnumerable.GetEnumerator()
  {
    return GetEnumerator();
  }

  IEnumerable<DA.Paragraph> DA.Selection.Paragraphs => Paragraphs.Cast<DA.Paragraph>();
  /// <summary>
  /// Gets the selected paragraphs.
  /// </summary>
  public IEnumerable<VM.Paragraph> Paragraphs
  {
    get
    {
      foreach (var paragraph in Document.Body.GetDescendants<VM.Paragraph>())
      {
          yield return paragraph;
      }
    }
  }
}
