namespace DocxControls;

/// <summary>
/// Interface for providing a property info to display in a <c>DataGrid</c>
/// </summary>
public interface IPropertyProvider
{
  /// <summary>
  /// Is the property read-only?
  /// </summary>
  public bool IsReadOnly{ get; }

  /// <summary>
  /// Value of the property
  /// </summary>
  public object? Value { get; }

  /// <summary>
  /// Is the property obsolete?
  /// </summary>
  public bool IsObsolete { get; }

  /// <summary>
  /// <para>
  ///  Mask to use with <c>Exceed MaskedTextBox</c>. Mask characters are the following:
  /// </para>
  /// <list type="table">
  ///   <item><term>0</term><description>Digit, required. This element will accept any single digit between 0 and 9.</description></item>
  ///   <item><term>9</term><description>Digit or space, optional</description></item>
  ///   <item><term>#</term><description>Digit or space, optional.  If this position is blank in the mask, it will be rendered as a space in the Text property. Plus (+) and minus (-) signs are allowed.</description></item>
  ///   <item><term>L</term><description>Letter, required. Restricts input to the ASCII letters a-z and A-Z. This mask element is equivalent to a-zA-Z in regular expressions.</description></item>
  ///   <item><term>?</term><description>Letter, optional. Restricts input to the ASCII letters a-z and A-Z.This mask element is equivalent to a-zA-Z? in regular expressions.</description></item>
  ///   <item><term>&amp;</term><description>Character, required. If the AsciiOnly property is set to true, this element behaves like the "L" element.</description></item>
  ///   <item><term>C</term><description>Character, optional. Any non-control character. If the AsciiOnly property is set to true, this element behaves like the "?" element.</description></item>
  ///   <item><term>A</term><description>Alphanumeric, required. If the AsciiOnly property is set to true, the only characters it will accept are the ASCII letters a-z and A-Z.</description></item>
  ///   <item><term>a</term><description>Alphanumeric, optional. If the AsciiOnly property is set to true, the only characters it will accept are the ASCII letters a-z and A-Z.</description></item>
  ///   <item><term>.</term><description>Decimal placeholder. The actual display character used will be the decimal symbol appropriate to the format provider, as determined by the control's FormatProvider property.</description></item>
  ///   <item><term>,</term><description>Thousands placeholder. The actual display character used will be the thousands placeholder appropriate to the format provider, as determined by the control's FormatProvider property.</description></item>
  ///   <item><term>:</term><description>Time separator. The actual display character used will be the time symbol appropriate to the format provider, as determined by the control's FormatProvider property.</description></item>
  ///   <item><term>&lt;</term><description>Shift down.Converts all characters that follow to lowercase.</description></item>
  ///   <item><term>&gt;</term><description>Shift up.Converts all characters that follow to uppercase.</description></item>
  ///   <item><term> </term><description>Disable a previous shift up of shift down.</description></item>
  ///   <item><term>\</term><description>Escape. Escapes a mask character, turning it into a literal. "\" is the escape sequence for a backslash.</description></item>
  /// </list>
  /// </summary>
  public string? EditMask { get; }

  /// <summary>
  /// Watermark to display in the control.
  /// </summary>
  public string? Watermark { get; }

  /// <summary>
  /// Determines if the property is just created.
  /// </summary>
  public bool IsNew { get; }

  /// <summary>
  /// Determines if the property is empty.
  /// </summary>
  public bool IsEmpty { get; }

}
