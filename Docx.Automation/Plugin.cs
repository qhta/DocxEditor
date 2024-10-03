using System.Reflection;

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
  public Assembly? Assembly { get; set; }

  /// <summary>
  /// The application instance.
  /// </summary>
  public Application? Application { get; }

  /// <summary>
  /// The name of the plugin.
  /// </summary>
  public string? Name { get; }

  /// <summary>
  /// The description of the plugin.
  /// </summary>
  public virtual string? GetDescription(string lang) => null;

  /// <summary>
  /// The version of the plugin.
  /// </summary>
  public virtual string? Version => null;

  /// <summary>
  /// The author of the plugin.
  /// </summary>
  public virtual string? Author => null;

  /// <summary>
  /// The website of the plugin.
  /// </summary>
  public virtual string? Website => null;

  /// <summary>
  /// The icon of the plugin.
  /// </summary>
  public virtual string? Icon => null;

  /// <summary>
  /// The command to execute the plugin.
  /// </summary>
  public virtual bool CanExecute() => true;

  /// <summary>
  /// The command to execute the plugin.
  /// </summary>
  public abstract void Execute();
}