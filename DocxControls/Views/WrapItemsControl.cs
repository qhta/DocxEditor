using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace DocxControls;


/// <summary>
/// Control that can wrap text items as a wrap panel
/// </summary>
public class WrapItemsControl : ListBox
{
  static WrapItemsControl()
  {
    DefaultStyleKeyProperty.OverrideMetadata(typeof(WrapItemsControl), new FrameworkPropertyMetadata(typeof(WrapItemsControl)));
  }

  /// <summary>
  /// Dependency property for <see cref="ItemBorderBrush"/> property
  /// </summary>
  public static readonly DependencyProperty ItemBorderBrushProperty = DependencyProperty.Register(nameof(ItemBorderBrush), typeof(Brush), typeof(WrapItemsControl), new PropertyMetadata(null));

  /// <summary>
  /// Gets or sets the brush for the border of each item
  /// </summary>
  public Brush ItemBorderBrush
  {
    get => (Brush)GetValue(ItemBorderBrushProperty);
    set => SetValue(ItemBorderBrushProperty, value);
  }

  /// <summary>
  /// Dependency property for <see cref="ItemBorderThickness"/> property
  /// </summary>
  public static readonly DependencyProperty ItemBorderThicknessProperty = DependencyProperty.Register(nameof(ItemBorderThickness), typeof(double), typeof(WrapItemsControl), new PropertyMetadata(0.0));

  /// <summary>
  /// Gets or sets the brush for the border of each item
  /// </summary>
  public double ItemBorderThickness
  {
    get => (double)GetValue(ItemBorderThicknessProperty);
    set => SetValue(ItemBorderThicknessProperty, value);
  }

  /// <summary>
  /// Dependency property for <see cref="ItemMargin"/> property
  /// </summary>
  public static readonly DependencyProperty ItemMarginProperty = DependencyProperty.Register(nameof(ItemMargin), typeof(Thickness), typeof(WrapItemsControl), new PropertyMetadata(new Thickness(0)));

  /// <summary>
  /// Gets or sets the brush for the border of each item
  /// </summary>
  public Thickness ItemMargin
  {
    get => (Thickness)GetValue(ItemMarginProperty);
    set => SetValue(ItemMarginProperty, value);
  }

  /// <summary>
  /// Opens the properties window for the selected item
  /// </summary>
  /// <param name="e"></param>
  protected override void OnMouseRightButtonUp(MouseButtonEventArgs e)
  {
    Executables.ShowProperties(DataContext);
    e.Handled = true;
  }
  ///// <summary>
  ///// Handles the mouse left button down event to select the item
  ///// </summary>
  ///// <param name="e">The event data</param>
  //protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
  //{
  //  base.OnMouseLeftButtonDown(e);
  //  var clickedItem = GetItemAt(e.GetPosition(this));

  //  if (clickedItem != null)
  //  {
  //    if (SelectedItem == null)
  //    {
  //      SetCurrentValue(SelectedItemProperty, clickedItem);
  //      SetCurrentValue(SelectedIndexProperty, Items.IndexOf(clickedItem));
  //    }
  //    else
  //    {
  //      SetCurrentValue(SelectedItemProperty, null);
  //      SetCurrentValue(SelectedIndexProperty, -1);
  //    }
  //  }
  //}

  ///// <summary>
  ///// Gets the item at the specified position
  ///// </summary>
  ///// <param name="position">The position</param>
  ///// <returns>The item at the specified position, or null if no item is found</returns>
  //private object? GetItemAt(Point position)
  //{
  //  HitTestResult result = VisualTreeHelper.HitTest(this, position);
  //  if (result != null)
  //  {
  //    DependencyObject? current = result.VisualHit;
  //    while (current != null && current != this)
  //    {
  //      if (current is FrameworkElement element && Items.Contains(element.DataContext))
  //      {
  //        return element.DataContext;
  //      }
  //      current = VisualTreeHelper.GetParent(current);
  //    }
  //  }
  //  return null;
  //}

}

