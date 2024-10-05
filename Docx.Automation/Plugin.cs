using System.Reflection;
using System.Windows.Input;

namespace Docx.Automation;

/// <summary>
/// Interface for plugins. Can be used to extend the functionality of the application.
/// Plugins can be implemented as classes that inherit from this interface in the separate assemblies
/// and later loaded by the application.
/// </summary>
public interface Plugin
{

  /// <summary>
  /// The assembly of the plugin.
  /// </summary>
  public Assembly Assembly { get; }

  /// <summary>
  /// The application instance.
  /// </summary>
  public Application? Application { get; }

  /// <summary>
  /// Language of the plugin user interface.
  /// </summary>
  public String? Lang { get; set;  }

  /// <summary>
  /// The name of the plugin.
  /// </summary>
  public string? Name { get; }

  /// <summary>
  /// Displayed name of the plugin.
  /// </summary>
  public string? Caption { get; }
  
  /// <summary>
  /// Displayed tooltip of the plugin
  /// </summary>
  public string? Tooltip { get; }

  /// <summary>
  /// The description of the plugin.
  /// </summary>
  public string? Description { get; }

  /// <summary>
  /// The version of the plugin.
  /// </summary>
  public string? Version { get; }

  /// <summary>
  /// Company (or author) that created the plugin.
  /// </summary>
  public string? Company { get; }

  /// <summary>
  /// Method that is called when the plugin is loaded.
  /// </summary>
  public void StartUp();

  /// <summary>
  /// Method that is called when the plugin is unloaded.
  /// </summary>
  public void CloseUp();

  /// <summary>
  /// The main method of the plugin.
  /// </summary>
  public void Execute();

  /// <summary>
  /// 
  /// </summary>
  public IEnumerable<PluginCommand> Commands { get; }
}