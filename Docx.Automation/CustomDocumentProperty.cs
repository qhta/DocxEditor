namespace Docx.Automation;
/// <summary>
/// Represents a single instance of a custom document property.
/// </summary>
public interface CustomDocumentProperty: DocumentProperty
{
  /// <summary>
  /// Specifies whether the LinkToContent property is linked to the contents of the container document.
  /// </summary>
  bool LinkToContent { get; set; }

  /// <summary>
  /// Specifies the source of the link. Used when LinkToContent is true.
  /// </summary>
  string? LinkSource { get; set; }
}
