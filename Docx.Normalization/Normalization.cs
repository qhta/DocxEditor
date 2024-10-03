using System.Reflection;
using Docx.Automation;

namespace Docx.Normalization;

public class Normalization: Plugin
{
  /// <summary>
  /// Initializing constructor.
  /// </summary>
  /// <param name="application"></param>
  public Normalization(Application application)
  {
    Application = application;
  }

  /// <summary>
  /// Stores the assembly of the plugin.
  /// </summary>
  public Assembly? Assembly { get; set; }

  public Application? Application { get; }

  public string? Name => "Normalization";

  public string? GetDescription(string lang) => "Normalize the document";

  public void Execute()
  {
    throw new NotImplementedException();
  }
}
