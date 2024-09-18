using System.Windows;
using System.Windows.Interop;

namespace DocxControls
{
  /// <summary>
  /// Interaction logic for PropertiesWindow.xaml
  /// </summary>
  public partial class PropertiesWindow : Window
  {
    /// <summary>
    /// Default constructor
    /// </summary>
    public PropertiesWindow()
    {
      InitializeComponent();
      DataContextChanged += PropertiesWindow_DataContextChanged;
    }

    private void PropertiesWindow_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
    {
      //Debug.WriteLine($"PropertiesWindow.DataContextChanged({DataContext.GetType().Name})");
      if (DataContext is ObjectViewModel objectViewModel)
      {
        //Debug.WriteLine($"objectViewModel({objectViewModel.ObjectProperties.GetType()?.Name})");
        objectViewModel.ObjectProperties.PropertyChanged += ObjectProperties_PropertyChanged;
      }
    }

    private void ObjectProperties_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
      if (e.PropertyName == nameof(PropertiesViewModel.DataGridWidth) && sender is PropertiesViewModel propertiesViewModel)
      {
        var desiredWidth = propertiesViewModel.DataGridWidth;
        //Debug.WriteLine($"PropertiesWindow.DataGridWidth={desiredWidth}");
        if (desiredWidth > 0)
        {
          var windowHandle = new WindowInteropHelper(this).Handle;
          var windowStyle = NativeMethods.GetWindowLong(windowHandle, NativeMethods.GWL_STYLE);
          var hasMenu = (windowStyle & NativeMethods.WS_SYSMENU) != 0;

          var rect = new NativeMethods.RECT();
          NativeMethods.AdjustWindowRectEx(ref rect, windowStyle, hasMenu, NativeMethods.GetWindowLong(windowHandle, NativeMethods.GWL_EXSTYLE));

          var nonClientWidth = (rect.Right - rect.Left);
          this.Width = desiredWidth+nonClientWidth;
        }
      }
    }

    internal static class NativeMethods
    {
      public const int GWL_STYLE = -16;
      public const int GWL_EXSTYLE = -20;
      public const int WS_SYSMENU = 0x00080000;

      [System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true)]
      public static extern int GetWindowLong(IntPtr hWnd, int nIndex);

      [System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true)]
      public static extern bool AdjustWindowRectEx(ref RECT lpRect, int dwStyle, bool bMenu, int dwExStyle);

      [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
      public struct RECT
      {
        public int Left;
        public int Top;
        public int Right;
        public int Bottom;
      }
    }

    //private void PropertiesView_OnSizeChanged(object sender, SizeChangedEventArgs e)
    //{
    //  Debug.WriteLine($"{sender.GetType().Name}_SizeChanged ({e.NewSize.Width},{e.NewSize.Height})");
    //}
    private void PropertiesWindow_OnSizeChanged(object sender, SizeChangedEventArgs e)
    {
      //throw new NotImplementedException();
      //if (DataContext!=null)
    }
  }
}
