using System.Reflection;
using System.Windows.Input;
using Docx.Automation;

namespace Docx.Normalization;

public class Normalization : PluginClass
{
  /// <summary>
  /// Initializing constructor.
  /// </summary>
  /// <param name="application"></param>
  public Normalization(Application application): base(application)
  {
  }

  public override void StartUp()
  {
    throw new NotImplementedException();
  }

}
