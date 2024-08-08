using System.Collections.ObjectModel;

using DocumentFormat.OpenXml.Packaging;

using Qhta.MVVM;

namespace DocxControls;
public class PropertiesViewModel: ViewModel
{
  public WordprocessingDocument WordDocument { get; init; } = null!;

  public ObservableCollection<PropertyViewModel> Properties { get; } = new();

  public bool CanAddAndDeleteProperties { get; init; }
}
