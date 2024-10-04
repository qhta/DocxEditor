using System.Windows.Input;

namespace Docx.Automation;

/// <summary>
/// Command interface for plugins.
/// </summary>
public interface PluginCommand: ICommand
{
  /// <summary>
  /// The plugin that owns the command.
  /// </summary>
  public Plugin Plugin { get; }

  /// <summary>
  /// Name of the command.
  /// </summary>
  public string? Name { get; }

  /// <summary>
  /// Displayed caption of the command. Used for menus and buttons.
  /// </summary>
  public string? Caption { get; }

  /// <summary>
  /// Short description of the command. Used for tooltips.
  /// </summary>
  public string? Tooltip { get; }

  /// <summary>
  /// Description of the command. Used for tooltips and help.
  /// </summary>
  public string? Description { get; }
}