using System.Collections.ObjectModel;
using Qhta.MVVM;

namespace DocxControls.ViewModels;

/// <summary>
/// View model for a body element
/// </summary>
public class Body: CompoundElementViewModel, DA.IStory
{
  /// <summary>
  /// Initializing constructor.
  /// </summary>
  /// <param name="documentViewModel">Owner view model. Must be <see cref="Document"/></param>
  /// <param name="body">Modeled body element</param>
  public Body(ViewModel documentViewModel, DXW.Body body) : base(documentViewModel, body)
  {
  }

  /// <inheritdoc/>
  public DA.StoryType StoryType { get; } = DA.StoryType.MainTextStory;

  /// <summary>
  /// Collection of sections in the body.
  /// </summary>
  public ObservableCollection<Section> Sections { get; } = new ();

  /// <summary>
  /// Load all child OpenXmlElements and create their view models.
  /// When section properties are encountered, create a new section view model.
  /// </summary>
  public override void LoadAllElements()
  {
    base.LoadAllElements();
    foreach (var element in Elements)
    {
      if (element is Paragraph paragraph && paragraph.ParagraphElement?.ParagraphProperties?.SectionProperties!=null)
      {
        var sectionProperties = new SectionProperties(paragraph, paragraph.ParagraphElement.ParagraphProperties.SectionProperties!);
        var section = new Section(this, sectionProperties);
        Sections.Add(section);
      }
      if (Elements.LastOrDefault() is SectionProperties lastSectionProperties)
      {
        var lastSection = new Section(this, lastSectionProperties);
        Sections.Add(lastSection);
      }
    }
  }
}
