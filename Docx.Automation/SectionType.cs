namespace Docx.Automation;

/// <summary>
/// Specifies the type of section break.
/// </summary>
public enum SectionType
{
  /// <summary>
  /// Next Page Section Break.
  /// </summary>
   NextPage,
  /// <summary>
  /// Column Section Break.
  /// </summary>
  NextColumn,
  /// <summary>
  /// Continuous Section Break.
  /// </summary>
  Continuous,
  /// <summary>
  /// Even Page Section Break.
  /// </summary>
  EvenPage,
  /// <summary>
  /// Odd Page Section Break.
  /// </summary>
OddPage,
}