using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Windows;

using DocxControls.Views;

using Qhta.TypeUtils;

namespace DocxControls;


/// <summary>
/// Static class for executing commands.
/// </summary>
public class Application : ViewModel, DA.Application
{
  /// <summary>
  ///  Default private constructor prevents creating instances of the class by other classes.
  /// </summary>
  private Application()
  {
    ScanViewModels();
  }

  /// <summary>
  /// Singleton instance of the class.
  /// </summary>
  public static Application Instance { get; } = new();

  #region DA.Application properties implementation ------------------------------------------------------------------------------------
  DA.Document? DA.Application.ActiveDocument => ActiveWindow?.Document;

  /// <summary>
  /// Returns an active document from the <see cref="ActiveWindow"/>.
  /// May be null if no window is active.
  /// </summary>
  public VM.Document? ActiveDocument => ActiveWindow?.Document;

  DA.DocumentWindow? DA.Application.ActiveWindow => ActiveWindow;

  /// <summary>
  /// Returns a Window object that represents the active window (the window with the focus).
  /// May be null if no window is active.
  /// </summary>
  public DocumentWindow? ActiveWindow
  {
    get => _ActiveWindow;
    set
    {
      if (_ActiveWindow == value) return;
      _ActiveWindow = value;
      NotifyPropertyChanged(nameof(ActiveWindow));
    }
  }
  private DocumentWindow? _ActiveWindow;

  DA.Documents DA.Application.Documents => Documents;
  /// <summary>
  /// List of opened documents.
  /// </summary>
  public VM.Documents Documents { get; } = new();

  DA.DocumentWindows DA.Application.Windows => DocumentWindows;
  /// <summary>
  /// List of opened document Windows.
  /// </summary>
  public VM.DocumentWindows DocumentWindows { get; } = new();
  #endregion

  /// <summary>
  /// Registered ViewModel types.
  /// </summary>
  private Dictionary<Type, ViewModelTypeDescriptor> KnownViewModelTypes { get; } = new();
  /// <summary>
  /// Mapping of OpenXml types to ElementViewModel types.
  /// </summary>
  private Dictionary<Type, ViewModelTypeDescriptor> OpenXmlElementTypesMapping { get; } = new();

  /// <summary>
  /// Mapping of ModeledElement to ElementViewModel.
  /// </summary>
  private Dictionary<DX.OpenXmlElement, VM.ElementViewModel> ElementViewModels { get; } = new();

  /// <summary>
  /// Scans the assembly for ElementViewModel types and creates a mapping of ModeledElement types to ElementViewModel types.
  /// </summary>
  private void ScanViewModels()
  {
    KnownViewModelTypes.Clear();
    OpenXmlElementTypesMapping.Clear();
    foreach (var type in typeof(VM.ElementViewModel).Assembly.GetTypes().Where(t => t.IsAssignableTo(typeof(VM.ElementViewModel)) && !t.IsAbstract && t.GetCustomAttribute<NotMappedAttribute>() == null))
    {
      //ConstructorInfo? singleParamConstructorInfo = null;
      ConstructorInfo? doubleParamConstructorInfo = null;
      foreach (var constructor in type.GetConstructors())
      {
        if (constructor.GetParameters().Length == 2 && constructor.GetParameters()[0].ParameterType
              .IsAssignableTo(typeof(VM.ElementViewModel))
            & constructor.GetParameters()[1].ParameterType.IsAssignableTo(typeof(DX.OpenXmlElement)))
        {
          doubleParamConstructorInfo = constructor;
        }
        //else if (constructor.GetParameters().Length == 1 &&
        //         constructor.GetParameters()[0].ParameterType.IsAssignableTo(typeof(VM.ElementViewModel)))
        //{
        //  singleParamConstructorInfo = constructor;
        //}
      }
      if (doubleParamConstructorInfo != null)
      {
        //if (singleParamConstructorInfo == null)
        //  Debug.WriteLine($"No constructor creating new element found for {type}");
        var constructor = doubleParamConstructorInfo;
        var elementType = constructor.GetParameters()[1].ParameterType;
        Debug.WriteLine($"ElementViewModel: {elementType} -> {type}");
        var properties = new List<PropertyInfo>();
        var interfaces = type.GetInterfaces().Where
          (i => i.Namespace == typeof(DA._Element).Namespace && !i.Name.StartsWith("_")).ToArray();
        foreach (var implementedInterface in interfaces)
        {
          foreach (var property in implementedInterface.GetProperties())
          {
            Debug.WriteLine($"ScanViewModels Property: {property.Name}");
            properties.Add(property);
          }
        }
        var descriptor = new ViewModelTypeDescriptor(type, constructor, properties.ToArray());
        KnownViewModelTypes.Add(type, descriptor);
        OpenXmlElementTypesMapping.Add(elementType, descriptor);
      }
    }
  }

  /// <summary>
  /// Gets the properties of the view model to be able to display them in the properties window.
  /// </summary>
  /// <param name="type"></param>
  /// <returns></returns>
  public IEnumerable<PropertyInfo> GetViewModelProperties(Type type)
  {
    List<PropertyInfo> properties = new();
    if (type.BaseType != null && type.BaseType.IsEqualOrSubclassOf(typeof(VM.ElementViewModel)))
      properties.AddRange(GetViewModelProperties(type.BaseType));
    var thisProperties = type.GetProperties().Where(p => p.DeclaringType == type && p.CanWrite 
      && p.GetCustomAttribute<NotMappedAttribute>()==null && !ExcludedViewModelProperties.Contains(p.Name)).ToArray();
    foreach (var property in thisProperties)
    {
      Debug.WriteLine($"GetViewModelProperties Property: {property.Name}");
      properties.Add(property);
    }
    return properties;
  }

  private readonly string[] ExcludedViewModelProperties = new string[] { "Parent", "Element", "IsModified", "IsEditable", "IsSelected", "Dispatcher", "Owner", };

  /// <summary>
  /// Creates a view model for the specified element.
  /// </summary>
  /// <param name="parentViewModel"></param>
  /// <param name="element"></param>
  /// <returns></returns>
  /// <exception cref="InvalidOperationException"></exception>
  public VM.ElementViewModel CreateViewModel(ViewModel parentViewModel, DX.OpenXmlElement element)
  {
    if (OpenXmlElementTypesMapping.Count == 0)
    {
      ScanViewModels();
    }
    if (ElementViewModels.TryGetValue(element, out var result))
    {
      return result;
    }
    if (OpenXmlElementTypesMapping.TryGetValue(element.GetType(), out var viewModelDescriptor))

      result = (VM.ElementViewModel)viewModelDescriptor.constructorInfo.Invoke([parentViewModel, element]);
    else
    {
      Debug.WriteLine($"Unknown view model for element type: {element.GetType()}");
      result = new VM.UnknownElementViewModel(parentViewModel, element);
    }
    ElementViewModels[element] = result;
    return result;
  }

  /// <summary>
  /// Creates a view model for the specified elementType.
  /// </summary>
  /// <param name="parentViewModel"></param>
  /// <param name="elementType"></param>
  /// <returns></returns>
  /// <exception cref="InvalidOperationException"></exception>
  public VM.ElementViewModel CreateViewModel(ViewModel parentViewModel, Type elementType)
  {
    if (KnownViewModelTypes.Count == 0)
    {
      ScanViewModels();
    }
    VM.ElementViewModel result;
    DX.OpenXmlElement element;
    if (KnownViewModelTypes.TryGetValue(elementType, out var viewModelDescriptor))
    {
      result = (VM.ElementViewModel)viewModelDescriptor.constructorInfo.Invoke([parentViewModel, null]);
      element = result.ModeledElement!;
    }
    else
    {
      var newElement = (DX.OpenXmlElement?)Activator.CreateInstance(elementType, [parentViewModel, null]);
      element = newElement ?? throw new InvalidOperationException($"ElementViewModel for elementType type {elementType} could not be created");
      Debug.WriteLine($"Unknown view model for elementType type: {elementType}");
      result = new VM.UnknownElementViewModel(parentViewModel, element);
    }

    ElementViewModels[element] = result;
    return result;
  }

  /// <summary>
  /// Registers the view model for the specified element.
  /// </summary>
  /// <param name="element"></param>
  /// <param name="viewModel"></param>
  public void RegisterViewModel(DX.OpenXmlElement element, VM.ElementViewModel viewModel)
  {
    ElementViewModels[element] = viewModel;
  }

  /// <summary>
  /// Unregisters the view model for the specified element.
  /// </summary>
  /// <param name="element"></param>
  public void UnregisterViewModel(DX.OpenXmlElement element)
  {
    ElementViewModels.Remove(element);
  }

  /// <summary>
  /// Notifies the view model that the property has changed.
  /// </summary>
  /// <param name="element"></param>
  /// <param name="propertyName"></param>
  public void NotifyElementPropertyChanged(DX.OpenXmlElement element, string propertyName)
  {
    if (ElementViewModels.TryGetValue(element, out var viewModel))
    {
      viewModel.NotifyPropertyChanged(propertyName);
    }
  }

  /// <summary>
  /// Notifies the view model that all element properties have changed.
  /// </summary>
  /// <param name="element"></param>
  public void NotifyElementPropertiesChanged(DX.OpenXmlElement element)
  {
    if (ElementViewModels.TryGetValue(element, out var viewModel))
    {
      viewModel.NotifyAllPropertiesChanged();
    }
  }

  /// <summary>
  /// OpenDocument a document for viewing/editing.
  /// </summary>
  /// <param name="fileName">The full filename of the document.</param>
  /// <param name="readOnly">True to open the document as read-only. The default value is False.</param>
  /// <param name="visible">True to open the document in a visible window. The default value is True.</param>
  public VM.Document OpenDocument(string fileName, bool readOnly = false, bool visible = true)
  {
    Documents.Open(fileName, readOnly, visible);
    var documentViewModel = Documents.Last();
    if (documentViewModel == null)
      throw new InvalidOperationException($"Document \"{fileName}\" not opened");

    var documentWindow = new DocumentWindow { DataContext = documentViewModel };
    DocumentWindows.Add(documentWindow);
    if (System.Windows.Application.Current.MainWindow is DA.ApplicationWindow mainWindow)
    {
      mainWindow.AddDocumentWindow(documentWindow);
    }
    //documentWindow.Owner = System.Windows.Application.Current.MainWindow;
    //documentWindow.Show();
    DocumentOpened?.Invoke(this, new DA.DocumentEventArgs(documentViewModel));
    DocumentChanged?.Invoke(this, EventArgs.Empty);
    return documentViewModel;
  }

  /// <summary>
  /// OpenDocument a properties window for the sender object.
  /// </summary>
  /// <param name="sender"></param>
  public void ShowProperties(object sender)
  {
    var window = System.Windows.Application.Current.MainWindow;
    if (window == null) return;
    var propertiesWindow = new PropertiesWindow
    {
      Owner = window,
      DataContext = sender
    };
    propertiesWindow.ShowDialog();
  }

  DA.Document DA.Application.NewDocument() => NewDocument();
  /// <summary>
  /// Creates a new document.
  /// </summary>
  public VM.Document NewDocument()
  {
    Documents.Add();
    var document = Documents.Last();
    if (document == null)
      throw new InvalidOperationException($"New document not created");
    var documentWindow = new DocumentWindow { DataContext = document };
    DocumentWindows.Add(documentWindow);
    if (System.Windows.Application.Current.MainWindow is DA.ApplicationWindow mainWindow)
    {
      mainWindow.AddDocumentWindow(documentWindow);
    }
    DocumentCreated?.Invoke(this, new DA.DocumentEventArgs(document));
    DocumentChanged?.Invoke(this, EventArgs.Empty);
    return document;
  }

  /// <summary>
  /// Checks if the document can be closed.
  /// If the document is not editable or not modified, it can be closed.
  /// If the DocumentBeforeClose event is handled, the event is raised.
  /// In other cases, the user is prompted to save changes in the document.
  /// </summary>
  /// <param name="document"></param>
  /// <param name="canBreak"> Specifies whether ask to break all further actions.</param>  /// <returns></returns>
  public DA.CancelArgs CanCloseDocument(VM.Document document, bool canBreak)
  {
    if (!document.IsEditable || !document.IsModified)
    {
      return new DA.CancelArgs();
    }
    if (DocumentBeforeClose != null)
    {
      var args = new DA.DocumentCancelEventArgs(document, canBreak);
      DocumentBeforeClose(this, args);
      return args.Cancel;
    }
    var message = Strings.SaveChangesInDocument;
    var buttons = canBreak ? MessageBoxButton.YesNoCancel : MessageBoxButton.YesNo;
    var result = MessageBox.Show(message, Strings.ApplicationClosing, buttons);
    if (result == MessageBoxResult.No)
      return new DA.CancelArgs { Cancel = true };
    else if (result == MessageBoxResult.Cancel)
      return new DA.CancelArgs { Break = true };
    return new DA.CancelArgs();
  }

  /// <summary>
  /// Exit the document.
  /// </summary>
  /// <param name="document"></param>
  /// <returns></returns>
  public bool CloseDocument(VM.Document document)
  {
    var documentWindows = DocumentWindows.AsQueryable<DocumentWindow>().Where(w => w.DataContext == document).ToArray();
    foreach (var documentWindow in documentWindows)
    {
      if (!documentWindow.CloseDocument())
      {
        return false;
      }
    }
    return true;
  }

  /// <summary>
  /// Exit all opened documents.
  /// </summary>
  /// <returns></returns>
  public bool CloseAllDocuments()
  {
    foreach (var window in DocumentWindows.AsQueryable<DocumentWindow>().ToArray())
    {
      if (!window.CloseDocument())
      {
        return false;
      }
    }
    return true;
  }

  /// <summary>
  /// Checks if the document can be saved.
  /// If the document is not editable or not modified, it can not be saved.
  /// If the DocumentBeforeSave event is handled, the event is raised.
  /// In other cases, the user is prompted to save changes in the document.
  /// </summary>
  /// <param name="document"></param>
  ///  <param name="saveAsUIAs">
  /// True if the Save As dialog box is displayed, whether to save a new document.
  /// </param>
  /// <param name="canBreak"> Specifies whether ask to break all further actions.</param>
  /// <returns></returns>
  public DA.CancelArgs CanSaveDocument(VM.Document document, bool saveAsUIAs, bool canBreak)
  {
    if (!document.IsEditable || !document.IsModified)
    {
      return new DA.CancelArgs();
    }
    if (DocumentBeforeSave != null)
    {
      var args = new DA.DocumentSaveEventArgs(document, saveAsUIAs, canBreak);
      DocumentBeforeSave(this, args);
      return args.Cancel;
    }
    var message = Strings.SaveChangesInDocument;
    var buttons = canBreak ? MessageBoxButton.YesNoCancel : MessageBoxButton.YesNo;
    var result = MessageBox.Show(message, Strings.ApplicationClosing, buttons);
    if (result == MessageBoxResult.No)
      return new DA.CancelArgs { Cancel = true };
    else if (result == MessageBoxResult.Cancel)
      return new DA.CancelArgs { Break = true };
    return new DA.CancelArgs();
  }

  #region DA.Application methods implementation ------------------------------------------------------------------------------------

  void DA.Application.Activate(DA.DocumentWindow window) => Activate((DocumentWindow)window);
  /// <summary>
  /// Activates the specified window.
  /// </summary>
  /// <param name="window"></param>
  public void Activate(DocumentWindow window) => window.Activate();

  DA.DocumentWindow DA.Application.CreateNewWindow(DA.Document document) => CreateNewWindow((VM.Document)document);
  /// <summary>
  /// Creates a new window for existing document.
  /// </summary>
  public DocumentWindow CreateNewWindow(VM.Document document)
  {
    var documentWindow = new DocumentWindow { DataContext = document };
    DocumentWindows.Add(documentWindow);
    if (System.Windows.Application.Current.MainWindow is DA.ApplicationWindow mainWindow)
      mainWindow.AddDocumentWindow(documentWindow);
    DocumentChanged?.Invoke(this, EventArgs.Empty);
    return documentWindow;
  }

  /// <summary>
  /// Closes all opened documents and quits the application.
  /// </summary>
  public void Exit()
  {
    if (CloseAllDocuments())
    {
      BeforeExit?.Invoke(this, EventArgs.Empty);
    }
  }

  /// <summary>
  /// Selects all elements in the active Window.
  /// </summary>
  public void SelectAll()
  {

  }
  #endregion

  #region local methods ----------------------------------------------------------------------------------------------------------
  #endregion

  #region DA.Application events implementation ------------------------------------------------------------------------------------
  /// <summary>
  /// Occurs when the application is about to exit.
  /// </summary>
  public event EventHandler? BeforeExit;

  /// <summary>
  /// Occurs when a new document is created.
  /// </summary>
  public event DA.DocumentEventHandler? DocumentCreated;

  /// <summary>
  /// Occurs when a document is opened.
  /// </summary>
  public event DA.DocumentEventHandler? DocumentOpened;

  /// <summary>
  /// Occurs when a new document is created, when an existing document is opened,
  /// or when another document is made the active document.
  /// </summary>
  public event EventHandler? DocumentChanged;

  /// <summary>
  /// Occurs immediately before any open document is saved.
  /// </summary>
  public event DA.DocumentSaveEventHandler? DocumentBeforeSave;

  /// <summary>
  /// Occurs immediately before any open document closes.
  /// </summary>
  public event DA.DocumentCloseEventHandler? DocumentBeforeClose;

  /// <summary>
  /// Used to notify listeners that a document window has been activated.
  /// </summary>
  /// <param name="window"></param>
  public void NotifyWindowActivated(DocumentWindow window) => WindowActivated?.Invoke(this, new DA.DocumentWindowEventArgs(window));

  /// <summary>
  /// Occurs when any document window is activated.
  /// </summary>
  public event DA.DocumentWindowEventHandler? WindowActivated;

  /// <summary>
  /// Used to notify listeners that a document window has been deactivated.
  /// </summary>
  /// <param name="window"></param>
  public void NotifyWindowDeactivated(DocumentWindow window) => WindowDeactivated?.Invoke(this, new DA.DocumentWindowEventArgs(window));
  /// <summary>
  /// Occurs when any document window is deactivated.
  /// </summary>
  public event DA.DocumentWindowEventHandler? WindowDeactivated;
  #endregion


  #region Plugins implementation ---------------------------------------------------------------------------------------------------
  IEnumerable<DA.Plugin> DA.Application.Plugins => Plugins;
  /// <summary>
  /// Loaded plugins from the specified directory.
  /// </summary>
  public VM.Plugins Plugins { get; } = new();

  // public ObservableCollection<DA.PluginCommand> PluginCommands { get; }= new();

  /// <summary>
  /// Common instance of the PluginsWindow.
  /// </summary>
  public PluginsWindow? PluginsWindow { get; set; }

  /// <summary>
  /// Loads plugins from the specified directory.
  /// </summary>
  public bool LoadPlugins()
  {
    string pluginFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Plugins");
    if (Directory.Exists(pluginFolder))
    {
      foreach (var dll in Directory.GetFiles(pluginFolder, "*.dll"))
      {
        var assembly = Assembly.LoadFrom(dll);
        if (Plugins.Any(p => p.Assembly?.FullName == assembly.FullName))
          continue;
        var types = assembly.GetTypes();
        foreach (var t in types.Where(t => t.Implements(typeof(DA.Plugin))))
        {
          var plugin = (DA.Plugin?)Activator.CreateInstance(t, this);
          if (plugin != null)
          {
            Plugins.Add(plugin);
            plugin.StartUp();
          }
        };
      }
    }
    return true;
  }

  /// <summary>
  /// Opens a new Window with the list of plugins.
  /// </summary>
  public void OpenPluginsView()
  {
    if (PluginsWindow == null)
      PluginsWindow = new PluginsWindow();
    PluginsWindow.Owner = System.Windows.Application.Current.MainWindow;
    PluginsWindow.DataContext = Plugins;
    PluginsWindow.Show();
  }

  #endregion
}
