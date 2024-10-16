﻿using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

using Docx.Automation;

using DocxControls;
using DocxControls.Views;

using Syncfusion.Windows.Tools.Controls;

using DA = Docx.Automation;
using DCV = DocxControls.Views;

namespace DocxEditor;
/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window, DA.ApplicationWindow
{

  public MainWindow()
  {
    InitializeComponent();
    Loaded += MainWindow_Loaded;
  }

  private void MainWindow_Loaded(object sender, RoutedEventArgs e)
  {
    AddPluginCommandsToMenu();
  }

  DA.Application DA.ApplicationWindow.Application => Application;
  /// <summary>
  /// Gets the application instance.
  /// </summary>
  public DocxControls.Application Application => DocxControls.Application.Instance;

  void DA.ApplicationWindow.AddDocumentWindow(DA.DocumentWindow window) => AddDocumentWindow((DCV.DocumentWindow)window);
  public void AddDocumentWindow(DCV.DocumentWindow window)
  {
    if (DocumentContainer.ActiveDocument is DCV.DocumentWindow activeWindow)
    {
      window.Left = activeWindow.Left + 20;
      window.Top = activeWindow.Top + 20;
    }
    DocumentContainer.Items.Add(window);
    Activate(window);
  }

  void DA.ApplicationWindow.Activate(DA.DocumentWindow window) => Activate((DCV.DocumentWindow)window);
  /// <summary>
  /// Brings the specified window to the front and sets it as the active window.
  /// </summary>
  /// <param name="window"></param>
  public void Activate(DCV.DocumentWindow window)
  {
    DocumentContainer.ActiveDocument = window;
    Application.ActiveWindow = window;
    CommandManager.InvalidateRequerySuggested();
  }

  private void AddPluginCommandsToMenu()
  {
    // Assuming you have a method to get the list of plugins
    Application.LoadPlugins();

    foreach (var plugin in Application.Plugins)
    {
      TryAddPluginMenuItem(plugin);
    }
  }

  /// <summary>
  /// Try to add a plugin to the Tools menu.
  /// </summary>
  /// <param name="plugin"></param>
  /// <returns></returns>
  private bool TryAddPluginMenuItem(DA.Plugin plugin)
  {
    var pluginMenuItem = ToolsMenu.Items.OfType<MenuItem>().FirstOrDefault(item => item.Tag?.ToString() == plugin.Name);
    if (pluginMenuItem == null)
    {
      pluginMenuItem = new MenuItem
      {
        Tag = plugin.Name
      };
      BindingOperations.SetBinding(pluginMenuItem, HeaderedItemsControl.HeaderProperty, new Binding("Caption") { Source = plugin });
      BindingOperations.SetBinding(pluginMenuItem, ToolTipProperty, new Binding("Tooltip") { Source = plugin });
      ToolsMenu.Items.Add(pluginMenuItem);
    }

    foreach (var command in plugin.Commands)
    {
      var commandMenuItem = pluginMenuItem.Items.OfType<MenuItem>().FirstOrDefault(item => item.Tag?.ToString() == command.Name);
      if (commandMenuItem == null)
      {
        commandMenuItem = new MenuItem
        {
          Tag = command.Name,
          Command = command
        };
        BindingOperations.SetBinding(commandMenuItem, HeaderedItemsControl.HeaderProperty, new Binding("Caption") { Source = command });
        BindingOperations.SetBinding(commandMenuItem, ToolTipProperty, new Binding("Tooltip") { Source = command });
        pluginMenuItem.Items.Add(commandMenuItem);
      }
    }
    return true;
  }

  private void New_Click(object sender, RoutedEventArgs e)
  {
    Application.NewDocument();
  }

  private void Open_Click(object sender, RoutedEventArgs e)
  {
    //Application.OpenFile();
  }

  private void Exit_Click(object sender, RoutedEventArgs e)
  {
    if (Application.CloseAllDocuments())
      System.Windows.Application.Current.Shutdown();
  }

  private void Cut_Click(object sender, RoutedEventArgs e)
  {
    MessageBox.Show("Cut clicked");
  }

  private void Copy_Click(object sender, RoutedEventArgs e)
  {
    MessageBox.Show("Copy clicked");
  }

  private void Paste_Click(object sender, RoutedEventArgs e)
  {
    MessageBox.Show("Paste clicked");
  }

  private void About_Click(object sender, RoutedEventArgs e)
  {
    MessageBox.Show("About clicked");
  }

  protected override void OnClosing(CancelEventArgs e)
  {
    if (!Application.CloseAllDocuments())
      e.Cancel = true;
  }


}
