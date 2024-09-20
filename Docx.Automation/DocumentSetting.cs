namespace Docx.Automation;

/// <summary>
/// Represents  a document setting.
/// </summary>
public interface DocumentSetting: DocumentProperty
{
  /// <summary>
  /// Setting category.
  /// </summary>
  public SettingCategory Category { get; }
}
