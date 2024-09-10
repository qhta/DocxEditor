using System.Collections.ObjectModel;

using DocumentFormat.OpenXml.Packaging;

using Qhta.MVVM;

namespace DocxControls;

/// <summary>
/// Abstract class for the properties view model
/// </summary>
public abstract class PropertiesViewModel : ViewModel
{
  /// <summary>
  /// Internal Wordprocessing document
  /// </summary>
  public WordprocessingDocument WordDocument { get; init; } = null!;

  /// <summary>
  /// Observable collection of properties
  /// </summary>
  public ObservableCollection<PropertyViewModel> Items { get; } = new();

  /// <summary>
  /// Width of the data grid in the view
  /// </summary>
  public double DataGridWidth
  {
    get => _dataGridWidth;
    set
    {
      if (_dataGridWidth != value)
      {
        _dataGridWidth = value;
        //Debug.WriteLine($"{GetType().Name}.SetDataGridWidth({value})");
        NotifyPropertyChanged(nameof(DataGridWidth));
      }
    }
  }
  private double _dataGridWidth;

}
