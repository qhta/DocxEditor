namespace Docx.Automation;

/// <summary>
/// Represents a story in a document.
/// </summary>
public interface _Story
{
  /// <summary>
  /// Specifies the story type of this story.
  /// </summary>
  public StoryType StoryType { get; }
}
