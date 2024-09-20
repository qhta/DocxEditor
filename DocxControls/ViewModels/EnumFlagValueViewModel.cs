namespace DocxControls;

/// <summary>
/// View model for an enum value of a <c>Flags</c> enumeration type.
/// </summary>
internal class EnumFlagValueViewModel: EnumValueViewModel
{
  /// <summary>
  /// Creates a new instance of the <see cref="EnumFlagValueViewModel"/> class.
  /// </summary>
  /// <param name="parent">ViewModel to which this model belongs</param>
  /// <param name="enumType">ValueType of the enumeration (needed to convert enum to int)</param>
  /// <param name="enumMask">Mask of the enumeration value (converter to int)</param>
  public EnumFlagValueViewModel(IEnumProvider parent, Type enumType, object enumMask)
  {
    Parent = parent;
    EnumType = enumType;
    EnumMask = Convert.ToInt32(enumMask);
  }

  private IEnumProvider Parent { get; init; }

  private Type EnumType { get; init; }

  private int EnumMask { get; init; }


  private int EnumIntValue
  {
    get => Convert.ToInt32(Parent.SelectedEnum);
    set => Parent.SelectedEnum = Enum.ToObject(EnumType, value);
  }

  public bool IsChecked
  {
    get => (EnumIntValue & EnumMask) != 0;
    set
    {
      var intValue = value ? EnumIntValue | EnumMask : EnumIntValue & ~EnumMask;
      if (EnumIntValue != intValue)
      {
        EnumIntValue = intValue;
        NotifyPropertyChanged(nameof(IsChecked));
      }
    }
  }

}
