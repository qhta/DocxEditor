using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace DocxControls;

/// <summary>
/// Attached properties for a <see cref="ListBoxItem"/> that allows to execute a command on double click, left mouse down or right mouse up.
/// </summary>
public static class ListBoxItemBehaviors
{
  #region DoubleClickCommand
  /// <summary>
  /// Dependency property for <c>DoubleClickCommand</c>> property
  /// </summary>
  public static readonly DependencyProperty DoubleClickCommandProperty =
    DependencyProperty.RegisterAttached(
      "DoubleClickCommand",
      typeof(ICommand),
      typeof(ListBoxItemBehaviors),
      new PropertyMetadata(null, OnDoubleClickCommandChanged));

  /// <summary>
  /// Getter for <c>DoubleClickCommand</c>> property
  /// </summary>
  /// <param name="obj"></param>
  /// <returns></returns>
  public static ICommand GetDoubleClickCommand(DependencyObject obj)
  {
    return (ICommand)obj.GetValue(DoubleClickCommandProperty);
  }

  /// <summary>
  /// Setter for <c>DoubleClickCommand</c>> property
  /// </summary>
  /// <param name="obj"></param>
  /// <param name="value"></param>
  public static void SetDoubleClickCommand(DependencyObject obj, ICommand value)
  {
    obj.SetValue(DoubleClickCommandProperty, value);
  }

  private static void OnDoubleClickCommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
  {
    if (d is ListBoxItem item)
    {
      if (e.OldValue != null)
      {
        item.MouseDoubleClick -= OnMouseDoubleClick;
      }

      if (e.NewValue != null)
      {
        item.MouseDoubleClick += OnMouseDoubleClick;
      }
    }
  }

  private static void OnMouseDoubleClick(object sender, RoutedEventArgs e)
  {
    if (sender is ListBoxItem item)
    {
      ICommand command = GetDoubleClickCommand(item);
      // ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
      if (command != null && command.CanExecute(item.DataContext))
      {
        command.Execute(item.DataContext);
        e.Handled = true;
      }
    }
  }
  #endregion DoubleClickCommand

  #region LeftMouseDownCommand
  /// <summary>
  /// Dependency property for <c>LeftMouseDownCommand</c>> property
  /// </summary>
  public static readonly DependencyProperty LeftMouseDownCommandProperty =
    DependencyProperty.RegisterAttached(
      "LeftMouseDownCommand",
      typeof(ICommand),
      typeof(ListBoxItemBehaviors),
      new PropertyMetadata(null, OnLeftMouseDownCommandChanged));

  /// <summary>
  /// Getter for <c>LeftMouseDownCommand</c>> property
  /// </summary>
  /// <param name="obj"></param>
  /// <returns></returns>
  public static ICommand GetLeftMouseDownCommand(DependencyObject obj)
  {
    return (ICommand)obj.GetValue(LeftMouseDownCommandProperty);
  }

  /// <summary>
  /// Setter for <c>LeftMouseDownCommand</c>> property
  /// </summary>
  /// <param name="obj"></param>
  /// <param name="value"></param>
  public static void SetLeftMouseDownCommand(DependencyObject obj, ICommand value)
  {
    obj.SetValue(LeftMouseDownCommandProperty, value);
  }

  private static void OnLeftMouseDownCommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
  {
    if (d is ListBoxItem item)
    {
      if (e.OldValue != null)
      {
        item.MouseLeftButtonDown -= OnMouseLeftMouseDown;
      }

      if (e.NewValue != null)
      {
        item.MouseLeftButtonDown += OnMouseLeftMouseDown;
      }
    }
  }

  private static void OnMouseLeftMouseDown(object sender, RoutedEventArgs e)
  {
    if (sender is ListBoxItem item)
    {
      ICommand command = GetLeftMouseDownCommand(item);
      // ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
      if (command != null && command.CanExecute(item.DataContext))
      {
        command.Execute(item.DataContext);
        e.Handled = true;
      }
    }
  }
  #endregion LeftMouseDownCommand

  #region RightMouseUpCommand
  /// <summary>
  /// Dependency property for <c>RightMouseUpCommand</c>> property
  /// </summary>
  public static readonly DependencyProperty RightMouseUpCommandProperty =
    DependencyProperty.RegisterAttached(
      "RightMouseUpCommand",
      typeof(ICommand),
      typeof(ListBoxItemBehaviors),
      new PropertyMetadata(null, OnRightMouseUpCommandChanged));

  /// <summary>
  /// Getter for <c>RightMouseUpCommand</c>> property
  /// </summary>
  /// <param name="obj"></param>
  /// <returns></returns>
  public static ICommand GetRightMouseUpCommand(DependencyObject obj)
  {
    return (ICommand)obj.GetValue(RightMouseUpCommandProperty);
  }

  /// <summary>
  /// Setter for <c>RightMouseUpCommand</c>> property
  /// </summary>
  /// <param name="obj"></param>
  /// <param name="value"></param>
  public static void SetRightMouseUpCommand(DependencyObject obj, ICommand value)
  {
    obj.SetValue(RightMouseUpCommandProperty, value);
  }

  private static void OnRightMouseUpCommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
  {
    if (d is ListBoxItem item)
    {
      if (e.OldValue != null)
      {
        item.MouseRightButtonUp -= OnMouseRightMouseUp;
      }

      if (e.NewValue != null)
      {
        item.MouseRightButtonUp += OnMouseRightMouseUp;
      }
    }
  }

  private static void OnMouseRightMouseUp(object sender, RoutedEventArgs e)
  {
    if (sender is ListBoxItem item)
    {
      ICommand command = GetRightMouseUpCommand(item);
      // ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
      if (command != null && command.CanExecute(item.DataContext))
      {
        command.Execute(item.DataContext);
        e.Handled = true;
      }
    }
  }
  #endregion RightMouseUpCommand
}