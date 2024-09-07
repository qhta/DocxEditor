﻿using System.Collections.ObjectModel;

namespace DocxControls;

/// <summary>
/// View model for a paragraph run element
/// </summary>
public class RunViewModel : ElementViewModel
{
  /// <summary>
  /// Initializing constructor.
  /// </summary>
  /// <param name="propertiesViewModel">Owner view model. Must be <see cref="ParagraphViewModel"/></param>
  /// <param name="run">Modeled run element</param>
  public RunViewModel(ParagraphViewModel propertiesViewModel, DXW.Run run) : base(propertiesViewModel, run)
  {
    foreach (var element in run.Elements())
    {
      if (element is DXW.RunProperties properties)
        RunProperties = new RunPropertiesViewModel(this, properties);
      else
      {
        ElementViewModel? runViewModel = element switch
        {
          DXW.Text text => new TextViewModel(this, text),
          DXW.LastRenderedPageBreak lastRenderedPageBreak => new LastRenderedPageBreakViewModel(this, lastRenderedPageBreak),
          _ => null
        };
        if (runViewModel == null)
        {
          Debug.WriteLine($"RunViewModel: Element {element.GetType().Name} not supported");
          runViewModel = new UnknownElementViewModel(this, element);
        }
        Elements.Add(runViewModel);
      }
    }
    RunProperties ??= new RunPropertiesViewModel(this, run.GetProperties());
    InitMoreObjectProperties();
  }

  /// <summary>
  /// Run element of the paragraph
  /// </summary>
  public DXW.Run Run => (DXW.Run)Element!;

  /// <summary>
  /// Run properties view model
  /// </summary>
  public RunPropertiesViewModel? RunProperties { get; set; }

  /// <summary>
  /// Observable collection of element view models
  /// </summary>
  public ObservableCollection<ElementViewModel> Elements { get; } = new();

  /// <summary>
  /// Check if the run is bold
  /// </summary>
  public bool IsBold => Run.IsBold();

  /// <summary>
  /// Check if the run is italic
  /// </summary>
  public bool IsItalic => Run.IsItalic();

  /// <summary>
  /// Check if the run is underlined
  /// </summary>
  public bool IsUnderline => Run.IsItalic();

  /// <summary>
  /// Initializes the object properties
  /// </summary>
  protected override void InitObjectProperties()
  {
    ObjectProperties.Add(new ObjectPropertyViewModel(this, nameof(DXW.Run.RsidRunAddition)));
    ObjectProperties.Add(new ObjectPropertyViewModel(this, nameof(DXW.Run.RsidRunDeletion)));
    ObjectProperties.Add(new ObjectPropertyViewModel(this, nameof(DXW.Run.RsidRunProperties)));
  }

  /// <summary>
  /// Initializes the object properties
  /// </summary>
  protected void InitMoreObjectProperties()
  {
    if (RunProperties != null)
    {
      foreach (var property in RunProperties.ObjectProperties.Items)
      {
        ObjectProperties.Add(property);
      }
    }
  }
}
