using Qhta.MVVM;

namespace DocxControls.Commands;

/// <summary>
/// Command to select all elements in the ActiveWindow.
/// </summary>
public class SelectAll : Command
{

  //public SelectAllCommand()
  //{
  //  Application.Instance.RegisterCommand(this);
  //}

  /// <summary>
  /// Checks if ActiveWindow is not null.
  /// </summary>
  /// <param name="parameter"></param>
  /// <returns></returns>
  public override bool CanExecute(object? parameter)
  {
    var activeWindow = Application.Instance.ActiveWindow;
    var document = activeWindow?.Document;
    //Debug.WriteLine($"SelectAllCommand.CanExecute ActiveWindow={activeWindow} Document={document}");
    return document!=null;
  }

  /// <summary>
  /// Invokes ActiveWindow SelectAll method.
  /// </summary>
  /// <param name="parameter"></param>
  /// <exception cref="NotImplementedException"></exception>
  public override void Execute(object? parameter)
  {
    Application.Instance.ActiveWindow?.SelectAll();
  }
}
