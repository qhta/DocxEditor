using System.Reflection;

namespace Docx.Automation;

/// <summary>
/// Abstract class for plugins.
/// </summary>
public abstract class PluginClass : Plugin
{
  /// <summary>
  /// Initializing constructor.
  /// </summary>
  /// <param name="application"></param>
  protected PluginClass(Application application)
  {
    Application = application;
  }

  /// <summary>
  /// Stores the assembly of the plugin.
  /// </summary>
  public Assembly Assembly => this.GetType().Assembly;

  /// <summary>
  /// Docx application instance.
  /// </summary>
  public Application? Application { get; }

  /// <summary>
  /// Name of the plugin.
  /// </summary>
  public string? Name => this.GetType().Name;

  /// <summary>
  /// Version of the plugin.
  /// </summary>
  public virtual string? Version => Assembly.GetName().Version?.ToString();

  /// <summary>
  /// Company (or author) that created the plugin.
  /// </summary>
  public virtual string? Company => GetAttribute<AssemblyCompanyAttribute>(Assembly)?.Company;

  /// <summary>
  /// Displayed name of the plugin.
  /// </summary>
  public virtual string? Caption => GetAttribute<AssemblyTitleAttribute>(Assembly)?.Title;

  /// <summary>
  /// Displayed tooltip of the plugin.
  /// </summary>
  public virtual string? Tooltip => GetAttribute<AssemblyDescriptionAttribute>(Assembly)?.Description;

  /// <summary>
  /// Description of the plugin.
  /// </summary>
  public virtual string? Description => GetAttribute<AssemblyDescriptionAttribute>(Assembly)?.Description;

  /// <summary>
  /// Language of the plugin user interface.
  /// </summary>
  public virtual string? Lang { get; set; }

  /// <summary>
  /// Get the attribute of the specified type from the assembly.
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="assembly"></param>
  /// <returns></returns>
  protected T? GetAttribute<T>(Assembly assembly) where T : Attribute
  {
    return (T?)Attribute.GetCustomAttribute(assembly, typeof(T));
  }

  /// <summary>
  /// Method that is called when the plugin is loaded.
  /// </summary>
  public virtual void StartUp()
  {
    // Do nothing by default.
  }

  /// <summary>
  /// Method that is called when the plugin is unloaded.
  /// </summary>
  public virtual void CloseUp()
  {
    // Do nothing by default.
  }

  /// <summary>
  /// The main method of the plugin.
  /// </summary>
  public virtual void Execute()
  {
    // Implement if needed.
  }

  /// <summary>
  /// Commands provided by the plugin.
  /// </summary>
  public IEnumerable<PluginCommand> Commands => _commands;

  /// <summary>
  /// Fill this list in the <see cref="StartUp"/> method with commands provided by the plugin.
  /// </summary>
  protected static readonly List<PluginCommand> _commands = new();
}
