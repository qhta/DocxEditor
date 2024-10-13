namespace Docx.Automation;

/// <summary>
/// Specifies the properties for a single column of text within this section
/// </summary>
public interface TextColumn: IElement
{
  /// <summary>
  /// Specifies the width (in points) of this text column. 
  /// </summary>
  public Length? Width { get; set; }

  /// <summary>
  /// Specifies the spacing (in points) between the current column and the next column. 
  /// </summary>
  public Length? Space { get; set; }
}