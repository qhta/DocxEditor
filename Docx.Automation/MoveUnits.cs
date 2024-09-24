namespace Docx.Automation;

/// <summary>
/// Specifies a unit of measure to navigate in the document.
/// </summary>
public enum MoveUnits
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
  /// <summary>
  /// Move to the next/previous element. It may be a child, or a sibling, or the parent's sibling. Stops when the last/first element is reached.
  /// </summary>
  Element = 1,
  /// <summary>
  /// Move to the next/previous sibling element. Stops when the last/first sibling element is reached
  /// </summary>
  Sibling = 2,
  /// <summary>
  /// Move to the next/previous sibling element. If there is no sibling, then move to the parent's sibling. Stops when the last/first sibling element is reached
  /// </summary>
  SiblingOrParentSibling = 3,

  //Character = 1,
  //Word = 2,
  //Sentence = 3,
  //Paragraph = 4,
  //Line = 5,
  //Story = 6,
  //Screen = 7,
  //Section = 8,
  //Column = 9,
  //Row = 10,
  //Window = 11,
  //Cell = 12,
  //CharacterFormatting = 13,
  //ParagraphFormatting = 14,
  //Table = 15,
  //Item = 16,
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}