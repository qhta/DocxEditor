namespace Docx.Automation;

/// <summary>
/// Represents a reference to an element.
/// </summary>
/// <typeparam name="ID">type of identifier</typeparam>
/// <typeparam name="T">type of element</typeparam>
public interface IElementReference<ID, out T>: _Element where T : _Element
{
  /// <summary>
  /// Identifier of the element.
  /// </summary>
  public ID? Id { get; set; }

  /// <summary>
  /// Gets the element.
  /// </summary>
  /// <returns></returns>
  public T? GetElement();
}