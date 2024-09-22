namespace Docx.Automation;

/// <summary>
/// Collection of elements.
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IElementCollection<T>: IEnumerable<T> where T : IElement
{
  /// <summary>
  /// Returns an Application object that represents the DocxControls application.
  /// </summary>
  public Application Application { get; }

  /// <summary>
  /// Returns the owner object for the specified object.
  /// </summary>
  public object? Parent { get; }

  /// <summary>
  /// Returns the number of items in the collection.
  /// </summary>
  public int Count { get; }
}
