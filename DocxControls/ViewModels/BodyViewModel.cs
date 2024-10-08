﻿using Qhta.MVVM;

namespace DocxControls.ViewModels;

/// <summary>
/// View model for a body element
/// </summary>
public class BodyViewModel: CompoundElementViewModel, DA.IStory
{
  /// <summary>
  /// Initializing constructor.
  /// </summary>
  /// <param name="documentViewModel">Owner view model. Must be <see cref="Document"/></param>
  /// <param name="body">Modeled body element</param>
  public BodyViewModel(ViewModel documentViewModel, DXW.Body body) : base(documentViewModel, body)
  {
  }

  /// <inheritdoc/>
  public DA.StoryType StoryType { get; } = DA.StoryType.MainTextStory;

}
