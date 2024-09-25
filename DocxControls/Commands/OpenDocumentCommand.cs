using System.Windows.Input;
using Microsoft.Win32;

using Qhta.MVVM;

namespace DocxControls.Commands;

/// <summary>
/// Command to open a file.
/// </summary>
public class OpenDocumentCommand: Command, ICommand
{
  /// <summary>
  /// Displays a dialog and invokes Application OpenDocument method.
  /// </summary>
  /// <param name="parameter"></param>
  /// <exception cref="NotImplementedException"></exception>
  public override void Execute(object? parameter)
  {
      // ReSharper disable once UseObjectOrCollectionInitializer
      OpenFileDialog openFileDialog = new();
      openFileDialog.Filter = "Docx files (*.docx)|*.docx|All files (*.*)|*.*";
      openFileDialog.ShowReadOnly = true;
      if (openFileDialog.ShowDialog() == true)
      {
        string filePath = openFileDialog.FileName;
        var readOnly = openFileDialog.ReadOnlyChecked;
        Application.Instance.OpenDocument(filePath, readOnly);
      }
  }
}
