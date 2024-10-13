namespace Docx.Automation;

/// <summary>
/// Specifies the direction of the text flow for the section. 
/// </summary>
public enum TextDirection
{
  /// <summary>
  /// Left to Right, Top to Bottom.
  /// </summary>
  LefToRightTopToBottom,
  /// <summary>
  /// Top to bottom. This item is only available in Office 2010 and later.
  /// </summary>
  LeftToRightTopToBottom2010,
  /// <summary>
  /// Top to Bottom, Right to Left.
  /// </summary>
  TopToBottomRightToLeft,
  /// <summary>
  /// Right to left. This item is only available in Office 2010 and later.
  /// </summary>
  TopToBottomRightToLeft2010,
  /// <summary>
  /// Bottom to Top, Left to Right.
  /// </summary>
  BottomToTopLeftToRight,
  /// <summary>
  /// Left to right. This item is only available in Office 2010 and later.
  /// </summary>
  BottomToTopLeftToRight2010,
  /// <summary>
  /// Left to Right, Top to Bottom Rotated.
  /// </summary>
  LeftToRightTopToBottomRotated,
  /// <summary>
  /// Top to bottom rotated. This item is only available in Office 2010 and later.
  /// </summary>
  LeftToRightTopToBottomRotated2010,
  /// <summary>
  /// Top to Bottom, Right to Left Rotated.
  /// </summary>
  TopToBottomRightToLeftRotated,
  /// <summary>
  /// Right to left rotated. This item is only available in Office 2010 and later.
  /// </summary>
  TopToBottomRightToLeftRotated2010,
  /// <summary>
  /// Top to Bottom, Left to Right Rotated.
  /// </summary>
  TopToBottomLeftToRightRotated,
  /// <summary>
  /// Left to right rotated This item is only available in Office 2010 and later.
  /// </summary>
  TopToBottomLeftToRightRotated2010,

}