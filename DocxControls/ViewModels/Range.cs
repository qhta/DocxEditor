using Docx.Automation;

using Qhta.MVVM;

namespace DocxControls.ViewModels;

/// <summary>
/// Represents a contiguous area in a document. Each Range object is defined by a starting and ending element.
/// </summary>
public class Range: ViewModel, DA.Range
{
  /// <summary>
  /// Initializing constructor.
  /// </summary>
  public Range(IElement start, IElement end)
  {
    Start = start;
    End = end;
  }

  /// <summary>
  /// Element that starts the range.
  /// </summary>
  public IElement Start { get; set; }

  /// <summary>
  /// Element that ends the range.
  /// </summary>
  public IElement End { get; set; }

  #region properties
  DA.Application DA.Range.Application => Application;

  /// <summary>
  /// Returns an object that represents the DocxControls application.
  /// </summary>
  public Application Application => DocxControls.Application.Instance;

  /// <summary>
  /// Returns the parent object for the specified object.
  /// </summary>
  public object? Parent =>Start.Parent;
  #endregion
}