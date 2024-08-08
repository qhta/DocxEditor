using Qhta.MVVM;

namespace DocxControls;

public class PropertyViewModel: ViewModel

{
  public string Caption { get; init; } = null!;

  public string Name { get; init; } = null!;

  public Type Type { get; init; } = null!;

  public object? Value
  {
    get => _Value;
    set
    {
      if (value != _Value)
      {
        _Value = value;
        NotifyPropertyChanged(Name);
      }
    }
  }

  private object? _Value;
}
