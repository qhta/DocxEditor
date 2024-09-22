namespace DocxControls.ViewModels;

/// <summary>
/// View model for a body element
/// </summary>
public class BodyViewModel: BlockElementViewModel, DA.IStory
{
  /// <summary>
  /// Initializing constructor.
  /// </summary>
  /// <param name="documentViewModel">Parent view model. Must be <see cref="Document"/></param>
  /// <param name="body">Modeled body element</param>
  public BodyViewModel(Document documentViewModel, DXW.Body body) : base(documentViewModel, body)
  {
  }

  /// <inheritdoc/>
  public DA.StoryType StoryType { get; } = DA.StoryType.MainTextStory;
}
