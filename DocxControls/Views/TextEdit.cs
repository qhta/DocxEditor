using Syncfusion.Windows.Controls.Input;

namespace DocxControls.Views;
/// <summary>
/// Extends the SfMaskedEdit control
/// </summary>
public class TextEdit: SfMaskedEdit
{
  /// <summary>
  /// End edit on Enter key
  /// </summary>
  /// <param name="e"></param>
  protected override void OnPreviewKeyDown(KeyEventArgs e)
  {
    if (e.Key == Key.Enter)
    {
      // Move focus to another control to end the edit
        MoveFocus(new TraversalRequest(FocusNavigationDirection.Down));
        e.Handled = true;
        return;
    }
    base.OnPreviewKeyDown(e);
  }

  /*

    Mask="{Binding EditMask}"
   */
}
