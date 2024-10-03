using Qhta.MVVM;

namespace DocxControls.Commands;

/// <summary>
/// Command to open a file.
/// </summary>
public class OpenPluginsView : Command
{
  /// <summary>
  /// Displays a dialog and invokes Application OpenDocument method.
  /// </summary>
  /// <param name="parameter"></param>
  public override void Execute(object? parameter)
  {
    if (Application.Instance.LoadPlugins())
      Application.Instance.OpenPluginsView();
  }
}
