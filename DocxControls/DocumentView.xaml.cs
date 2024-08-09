﻿using System.Windows.Controls;

namespace DocxControls;
/// <summary>
/// Interaction logic for DocumentView.xaml
/// </summary>
public partial class DocumentView : UserControl, IDisposable
{
  public DocumentView()
  {
    InitializeComponent();
  }

  public DocumentView(string filePath, bool isEditable)
  {
    InitializeComponent();
    // ReSharper disable once ObjectCreationAsStatement
    Open(filePath, isEditable);
  }

  public WordDocumentViewModel WordDocument { get; set; } = null!;

  public void Open(string filePath, bool isEditable)
  {
    WordDocument = new WordDocumentViewModel(filePath, isEditable);
    DataContext = WordDocument;
  }

  #region Dispose pattern
  private bool _disposed = false;

  public void Dispose()
  {
    Dispose(true);
    GC.SuppressFinalize(this);
  }

  protected virtual void Dispose(bool disposing)
  {
    if (_disposed) return;

    if (disposing)
    {
      // Dispose managed resources here
    }

    // Dispose unmanaged resources here

    _disposed = true;
  }

  ~DocumentView()
  {
    Dispose(false);
  }
  #endregion
}