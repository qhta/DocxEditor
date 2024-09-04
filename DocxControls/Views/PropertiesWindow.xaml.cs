using System.Windows;

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
      if (DataContext is ObjectViewModel vm)
      {
        if (vm.ModeledObject != null)
        {
          Title = vm.ModeledObject.GetType().Name;
        }
      }
    }
  }
}
