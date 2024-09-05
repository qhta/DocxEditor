using System.Collections.ObjectModel;

namespace DocxControls;

/// <summary>
/// View model for a paragraph run element
/// </summary>
public class RunViewModel : ElementViewModel
{
  /// <summary>
  /// Default constructor. Creates a new <see cref="Run"/>
  /// </summary>
  public RunViewModel(): this(new DXW.Run())
  {
  }

  /// <summary>
  /// Initializing constructor.
  /// </summary>
  /// <param name="run"></param>
  public RunViewModel(DXW.Run run): base (run)
  {
    foreach (var element in run.Elements())
    {
      if (element is DXW.RunProperties properties)
        RunProperties = new RunPropertiesViewModel(properties);
      else
      {
        ElementViewModel? runViewModel = element switch
        {
          DXW.Text text => new TextViewModel(text),
          _ => null
        };
        if (runViewModel != null)
          Elements.Add(runViewModel);
      }
    }
    RunProperties ??= new RunPropertiesViewModel(run.GetProperties());
  }

  /// <summary>
  /// Run element of the paragraph
  /// </summary>
  public DXW.Run Run => (DXW.Run)Element;

  /// <summary>
  /// Run properties view model
  /// </summary>
  public RunPropertiesViewModel RunProperties { get; set; }

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
  }
}
