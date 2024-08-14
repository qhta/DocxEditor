using System.Windows.Markup;

namespace DocxControls;

/// <summary>
/// Markup extension to bind to a static property of provider type
/// </summary>
public class StaticPropertyExtension : MarkupExtension
{
  /// <summary>
  /// Provider type
  /// </summary>
  public Type? TargetType { get; set; }
  /// <summary>
  /// Provider property name
  /// </summary>
  public string? PropertyName { get; set; }

  /// <summary>
  /// Provide the value of the static property
  /// </summary>
  /// <param name="serviceProvider"></param>
  /// <returns></returns>
  /// <exception cref="InvalidOperationException"></exception>
  public override object? ProvideValue(IServiceProvider serviceProvider)
  {
    if (TargetType == null || string.IsNullOrEmpty(PropertyName))
      throw new InvalidOperationException("TargetType and PropertyName must be set.");

    var property = TargetType.GetProperty(PropertyName, System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public);
    if (property == null)
      throw new InvalidOperationException($"Property '{PropertyName}' not found on type '{TargetType}'.");

    return property.GetValue(null, null);
  }
}

