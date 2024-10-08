using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace DocxControls.Views;


/// <summary>
/// Control that can wrap text items as a wrap panel
/// </summary>
public partial class WrapItemsControl : ListBox
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

}

