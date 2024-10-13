namespace Docx.Automation;

/// <summary>
/// Represents a border in a document.
/// </summary>
public interface Border: IElement
{
  /// <summary>
  /// <para>Border Style</para>
  /// <para>Represents the following attribute in the schema: w:val</para>
  /// </summary>
  /// <remark>
  /// xmlns:w=http://schemas.openxmlformats.org/wordprocessingml/2006/main
  /// </remark>
  public BorderType Type { get; set; }
  /// <summary>
  /// <para>Border Color</para>
  /// <para>Represents the following attribute in the schema: w:color</para>
  /// </summary>
  /// <remark>
  /// xmlns:w=http://schemas.openxmlformats.org/wordprocessingml/2006/main
  /// </remark>
  public String? Color { get; set; }
  /// <summary>
  /// <para>Border Theme Color</para>
  /// <para>Represents the following attribute in the schema: w:themeColor</para>
  /// </summary>
  /// <remark>
  /// xmlns:w=http://schemas.openxmlformats.org/wordprocessingml/2006/main
  /// </remark>
  public ThemeColor? ThemeColor { get; set; }
  /// <summary>
  /// <para>Border Theme Color Tint</para>
  /// <para>Represents the following attribute in the schema: w:themeTint</para>
  /// </summary>
  /// <remark>
  /// xmlns:w=http://schemas.openxmlformats.org/wordprocessingml/2006/main
  /// </remark>
  public string? ThemeTint { get; set; }
  /// <summary>
  /// <para>Border Theme Color Shade</para>
  /// <para>Represents the following attribute in the schema: w:themeShade</para>
  /// </summary>
  /// <remark>
  /// xmlns:w=http://schemas.openxmlformats.org/wordprocessingml/2006/main
  /// </remark>
  public string? ThemeShade { get; set; }
  /// <summary>
  /// <para>Border Width</para>
  /// <para>Represents the following attribute in the schema: w:sz</para>
  /// </summary>
  /// <remark>
  /// xmlns:w=http://schemas.openxmlformats.org/wordprocessingml/2006/main
  /// </remark>
  public Length? Size { get; set; }
  /// <summary>
  /// <para>Border Spacing Measurement</para>
  /// <para>Represents the following attribute in the schema: w:space</para>
  /// </summary>
  /// <remark>
  /// xmlns:w=http://schemas.openxmlformats.org/wordprocessingml/2006/main
  /// </remark>
  public Length? Space { get; set; }
  /// <summary>
  /// <para>Border Shadow</para>
  /// <para>Represents the following attribute in the schema: w:shadow</para>
  /// </summary>
  /// <remark>
  /// xmlns:w=http://schemas.openxmlformats.org/wordprocessingml/2006/main
  /// </remark>
  public bool? Shadow { get; set; }
  /// <summary>
  /// <para>Create Frame Effect</para>
  /// <para>Represents the following attribute in the schema: w:frame</para>
  /// </summary>
  /// <remark>
  /// xmlns:w=http://schemas.openxmlformats.org/wordprocessingml/2006/main
  /// </remark>
  public bool? Frame { get; set; }

}