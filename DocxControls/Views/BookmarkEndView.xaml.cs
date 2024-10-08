﻿using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using DocxControls.Helpers;

namespace DocxControls.Views;
/// <summary>
/// Interaction logic for BookmarkEndView.xaml
/// </summary>
public partial class BookmarkEndView : UserControl
{
  /// <summary>
  /// Default constructor
  /// </summary>
  public BookmarkEndView()
  {
    InitializeComponent();
    DataContextChanged += BookmarkView_DataContextChanged;
  }

  private void BookmarkView_DataContextChanged(object sender, System.Windows.DependencyPropertyChangedEventArgs e)
  {
    if (DataContext is VM.BookmarkEnd bookmarkViewModel)
    {
      var id = bookmarkViewModel.Id;
      Foreground = new SolidColorBrush(BaseColors.GetDarkColor(id));
    }
  }

  private void OnToolTipOpening(object sender, ToolTipEventArgs e)
  {
    if (DataContext is VM.ElementViewModel viewModel)
    {
      viewModel.IsHighlighted = true;
    }
  }

  private void OnToolTipClosing(object sender, ToolTipEventArgs e)
  {
    if (DataContext is VM.ElementViewModel viewModel)
    {
      viewModel.IsHighlighted = false;
    }
  }

}
