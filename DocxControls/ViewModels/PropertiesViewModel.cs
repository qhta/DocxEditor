using System.Collections.ObjectModel;

using DocumentFormat.OpenXml.Packaging;

using Qhta.MVVM;
using Xceed.Wpf.Toolkit;

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

}
