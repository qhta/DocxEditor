using Qhta.MVVM;

namespace DocxControls;

/// <summary>
/// View model for a custom tooltip.
/// </summary>
public class CustomToolTipViewModel: ViewModel
{
  /// <summary>
  /// Title of the tooltip.
  /// </summary>
  public string? Title
  {
    get => _title;
    set
    {
      if (value!= _title)
      {
        _title = value;
        NotifyPropertyChanged(nameof(Title));
      }
    }
  }

  private string? _title = "Title";

  /// <summary>
  /// Content of the tooltip.
  /// </summary>
  public string? Content
  {
    get => _content;
    set
    {
      if (value != _content)
      {
        _content = value;
        NotifyPropertyChanged(nameof(Content));
      }
    }
  }

  private string? _content = "Content";
}