namespace Docx.Automation;

/// <summary>
/// Specifies the presence of a comment content reference mark, which links the comment content with the contents of a document story.
/// </summary>
public interface CommentReference : IElementReference<int, Comment>, IRunItem
{

}