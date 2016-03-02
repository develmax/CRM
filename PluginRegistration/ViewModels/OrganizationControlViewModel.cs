// Decompiled with JetBrains decompiler
// Type: Microsoft.Crm.Tools.PluginRegistration.OrganizationControlViewModel
// Assembly: PluginRegistration, Version=7.1.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: BDC54875-46F6-4E1E-9048-C47CF582701C
// Assembly location: C:\Distribs\CRM\sdk\SDK\Tools\PluginRegistration\PluginRegistration.exe

using CrmLibraries;
using Microsoft.Crm.Tools.Libraries;
using Microsoft.Crm.Tools.PluginRegistration.CommonControls;
using Microsoft.Xrm.Sdk.Metadata;
using Microsoft.Xrm.Tooling.Connector;
using PluginProfiler.Library;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Forms.Layout;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace Microsoft.Crm.Tools.PluginRegistration
{
  public class OrganizationControlViewModel : BaseNotifyable, IPartImportsSatisfiedNotification
  {
    private const string SYSTEM_ERROR_MESSAGE = "The selected item is required for the Microsoft Dynamics CRM system to work correctly.";
    private const string SYSTEM_ERROR_CAPTION = "Microsoft Dynamics CRM";
    private const string REGISTER = "_Register";
    private const string NEW_ASSEMBLY = "Register New _Assembly";
    private const string NEW_STEP = "Register New S_tep";
    private const string NEW_IMAGE = "Register New _Image";
    private const string NEW_SERVICE_ENDPOINT = "Register New Service _Endpoint";
    private const string VIEW = "View";
    private const string DISPLAY_ASSEMBLY = "Display by Assembly";
    private const string DISPLAY_ENTITY = "Display by Entity";
    private const string DISPLAY_MESSAGE = "Display by Message";
    private const string INSTALL_PROFILER = "Install Pr_ofiler";
    private const string UNINSTALL_PROFILER = "Uninstall Pr_ofiler";
    private const string START_PROFILING = "Start Pro_filing";
    private const string STOP_PROFILING = "Stop Pro_filing";
    private const string DEBUG_PROFILER = "D_ebug";
    private const string PROFILE_WORKFLOW = "Pro_file Workflow";
    private const string START_PROFILE_WORKFLOW = "Start Profiling Workflow";
    private const string STOPALL_n_UNINSTALLPROFILE = "Stop All P_rofiling & Uninstall Profiler";
    private const string UPDATE = "_Update";
    private const string DISABLE = "_Disable";
    private const string ENABLE = "_Enable";
    private const string UNREGISTER = "U_nregister";
    private const string REFRESH = "Re_fresh";
    private const string SEARCH = "_Search";
    private const string ASSEMBLY_REG_GESTURE = "Ctrl+A";
    private const string STEP_REG_GESTURE = "Ctrl+T";
    private const string IMAGE_REG_GESTURE = "Ctrl+I";
    private const string ENDPOINT_REG_GESTURE = "Ctrl+E";
    private const string DISPLAY_ASSEMBLY_GESTURE = "Ctrl+Shift+A";
    private const string DISPLAY_ENTITY_GESTURE = "Ctrl+Shift+E";
    private const string DISPLAY_MESSAGE_GESTURE = "Ctrl+Shift+M";
    private const string SEARCH_GESTURE = "Ctrl+F";
    private const string REFRESH_GESTURE = "F5";
    private const string UNREGISTER_GESTURE = "Del";
    private RelayCommand _displayByAssemblyMenuShortCut;
    private RelayCommand _serviceEndPointRegistrationMenuShortCut;
    private RelayCommand _imageRegistrationMenuShortCut;
    private RelayCommand _stepRegistrationMenuShortCut;
    private RelayCommand _assemblyRegistrationMenuShortCut;
    private RelayCommand _displayByMessageMenuShortCut;
    private RelayCommand _displayByEntityMenuShortCut;
    private RelayCommand _refreshMenuShortCut;
    private RelayCommand _unregisterMenuShortCut;
    private RelayCommand _searchMenuShortCut;
    private RelayCommand _cmdMenuItems;
    private CrmMenuItems _registerMenu;
    private CrmMenuItems _newAssemblyMenu;
    private CrmMenuItems _newStepMenu;
    private CrmMenuItems _newImageMenu;
    private CrmMenuItems _newServiceMenu;
    private CrmMenuItems _viewMenu;
    private CrmMenuItems _displayAssemblyMenu;
    private CrmMenuItems _displayEntityMenu;
    private CrmMenuItems _displayMessageMenu;
    private CrmMenuItems _installProfilerMenu;
    private CrmMenuItems _unregisterMenu;
    private CrmMenuItems _debugMenu;
    private CrmMenuItems _profilingMenu;
    private CrmMenuItems _updateMenu;
    private CrmMenuItems _disableMenu;
    private CrmMenuItems _enableMenu;
    private CrmMenuItems _refreshMenu;
    private CrmMenuItems _searchMenu;
    private CrmMenuItems _profileWorkflowMenu;
    private CrmMenuItems _refresh;
    private CrmMenuItems _search;
    private CrmMenuItems _stopAllnUninstallProfile;
    private ProgressBarView _progView;
    private RelayCommand _click_OK;
    private bool _isEnableStatus;
    private Visibility _visibilityStatus;
    private string _progressStatusTxt;
    private DataTable _gridTable;
    private Dictionary<CrmTreeNodeImageType, System.Drawing.Image> _nodeImageList;
    private System.Drawing.Image _imgUninstallProfiler;
    private Dictionary<CrmImageType, System.Drawing.Image> _icons;
    private bool _isNotRefresh;
    public CrmViewType _currentView;
    private readonly CrmOrganization _org;
    private readonly MainViewModel _mainViewModel;
    private readonly OrganizationControlView _orgControlView;
    private static CrmEntitySorter _mEntitySorter;
    private Dictionary<string, CrmTreeNode> _rootNodeList;
    private Dictionary<Guid, Guid> _viewNodeList;
    private Dictionary<Guid, Guid> _stepParentList;
    private ObservableCollection<ICrmTreeNode> _assembliesTree;
    private ObservableCollection<CrmMenuItems> _crmMenuItemSource;
    private ObservableCollection<CrmMenuItems> _crmContextMenuSource;
    private ObservableCollection<CrmMenuItems> _crmDefaultContextMenuSource;
    private MenuActionType _menuActionType;
    private object _selectedItem;
    private bool _treeNodeExpanded;
    private CompositionContainer container;
    private AggregateCatalog catalog;
    private Visibility _btnVisibleStatus;
    private GridLength _detailsTabHeight;
    private AggregateCatalog debugModuleCatalog;

    public RelayCommand DisplayByAssemblyMenuShortCut
    {
      get
      {
        return this._displayByAssemblyMenuShortCut ?? (this._displayByAssemblyMenuShortCut = new RelayCommand((Action<object>) (s => this.MenuItem_Clicked((object) "Display by Assembly"))));
      }
    }

    public RelayCommand DisplayByEntityMenuShortCut
    {
      get
      {
        return this._displayByEntityMenuShortCut ?? (this._displayByEntityMenuShortCut = new RelayCommand((Action<object>) (s => this.MenuItem_Clicked((object) "Display by Entity"))));
      }
    }

    public RelayCommand DisplayByMessageMenuShortCut
    {
      get
      {
        return this._displayByMessageMenuShortCut ?? (this._displayByMessageMenuShortCut = new RelayCommand((Action<object>) (s => this.MenuItem_Clicked((object) "Display by Message"))));
      }
    }

    public RelayCommand AssemblyRegistrationMenuShortCut
    {
      get
      {
        return this._assemblyRegistrationMenuShortCut ?? (this._assemblyRegistrationMenuShortCut = new RelayCommand((Action<object>) (s => this.MenuItem_Clicked((object) "Register New _Assembly"))));
      }
    }

    public RelayCommand StepRegistrationMenuShortCut
    {
      get
      {
        return this._stepRegistrationMenuShortCut ?? (this._stepRegistrationMenuShortCut = new RelayCommand((Action<object>) (s => this.MenuItem_Clicked((object) "Register New S_tep"))));
      }
    }

    public RelayCommand ImageRegistrationMenuShortCut
    {
      get
      {
        return this._imageRegistrationMenuShortCut ?? (this._imageRegistrationMenuShortCut = new RelayCommand((Action<object>) (s => this.MenuItem_Clicked((object) "Register New _Image"))));
      }
    }

    public RelayCommand ServiceEndPointRegistrationMenuShortCut
    {
      get
      {
        return this._serviceEndPointRegistrationMenuShortCut ?? (this._serviceEndPointRegistrationMenuShortCut = new RelayCommand((Action<object>) (s => this.MenuItem_Clicked((object) "Register New Service _Endpoint"))));
      }
    }

    public RelayCommand SearchMenuShortCut
    {
      get
      {
        return this._searchMenuShortCut ?? (this._searchMenuShortCut = new RelayCommand((Action<object>) (s => this.MenuItem_Clicked((object) "_Search"))));
      }
    }

    public RelayCommand RefreshMenuShortCut
    {
      get
      {
        return this._refreshMenuShortCut ?? (this._refreshMenuShortCut = new RelayCommand((Action<object>) (s => this.MenuItem_Clicked((object) "Re_fresh"))));
      }
    }

    public RelayCommand UnregisterMenuShortCut
    {
      get
      {
        return this._unregisterMenuShortCut ?? (this._unregisterMenuShortCut = new RelayCommand((Action<object>) (s => this.MenuItem_Clicked((object) "U_nregister"))));
      }
    }

    public CrmMenuItems StopAllnUninstallProfile
    {
      get
      {
        return this._stopAllnUninstallProfile;
      }
      set
      {
        this.SetProperty<CrmMenuItems>(ref this._stopAllnUninstallProfile, value, "StopAllnUninstallProfile");
      }
    }

    public CrmMenuItems NewAssemblyMenu
    {
      get
      {
        return this._newAssemblyMenu;
      }
      set
      {
        this.SetProperty<CrmMenuItems>(ref this._newAssemblyMenu, value, "NewAssemblyMenu");
      }
    }

    public CrmMenuItems NewStepMenu
    {
      get
      {
        return this._newStepMenu;
      }
      set
      {
        this.SetProperty<CrmMenuItems>(ref this._newStepMenu, value, "NewStepMenu");
      }
    }

    public CrmMenuItems NewImageMenu
    {
      get
      {
        return this._newImageMenu;
      }
      set
      {
        this.SetProperty<CrmMenuItems>(ref this._newImageMenu, value, "NewImageMenu");
      }
    }

    public CrmMenuItems NewServiceMenu
    {
      get
      {
        return this._newServiceMenu;
      }
      set
      {
        this.SetProperty<CrmMenuItems>(ref this._newServiceMenu, value, "NewServiceMenu");
      }
    }

    public CrmMenuItems DisplayAssemblyMenu
    {
      get
      {
        return this._displayAssemblyMenu;
      }
      set
      {
        this.SetProperty<CrmMenuItems>(ref this._displayAssemblyMenu, value, "DisplayAssemblyMenu");
      }
    }

    public CrmMenuItems DisplayEntityMenu
    {
      get
      {
        return this._displayEntityMenu;
      }
      set
      {
        this.SetProperty<CrmMenuItems>(ref this._displayEntityMenu, value, "DisplayEntityMenu");
      }
    }

    public CrmMenuItems DisplayMessageMenu
    {
      get
      {
        return this._displayMessageMenu;
      }
      set
      {
        this.SetProperty<CrmMenuItems>(ref this._displayMessageMenu, value, "DisplayMessageMenu");
      }
    }

    public CrmMenuItems RegisterMenu
    {
      get
      {
        return this._registerMenu;
      }
      set
      {
        this.SetProperty<CrmMenuItems>(ref this._registerMenu, value, "RegisterMenu");
      }
    }

    public CrmMenuItems ViewMenu
    {
      get
      {
        return this._viewMenu;
      }
      set
      {
        this.SetProperty<CrmMenuItems>(ref this._viewMenu, value, "ViewMenu");
      }
    }

    public CrmMenuItems InstallProfilerMenu
    {
      get
      {
        return this._installProfilerMenu;
      }
      set
      {
        this.SetProperty<CrmMenuItems>(ref this._installProfilerMenu, value, "InstallProfilerMenu");
      }
    }

    public CrmMenuItems DebugMenu
    {
      get
      {
        return this._debugMenu;
      }
      set
      {
        this.SetProperty<CrmMenuItems>(ref this._debugMenu, value, "DebugMenu");
      }
    }

    public CrmMenuItems ProfilingMenu
    {
      get
      {
        return this._profilingMenu;
      }
      set
      {
        this.SetProperty<CrmMenuItems>(ref this._profilingMenu, value, "ProfilingMenu");
      }
    }

    public CrmMenuItems UpdateMenu
    {
      get
      {
        return this._updateMenu;
      }
      set
      {
        this.SetProperty<CrmMenuItems>(ref this._updateMenu, value, "UpdateMenu");
      }
    }

    public CrmMenuItems DisableMenu
    {
      get
      {
        return this._disableMenu;
      }
      set
      {
        this.SetProperty<CrmMenuItems>(ref this._disableMenu, value, "DisableMenu");
      }
    }

    public CrmMenuItems EnableMenu
    {
      get
      {
        return this._enableMenu;
      }
      set
      {
        this.SetProperty<CrmMenuItems>(ref this._enableMenu, value, "EnableMenu");
      }
    }

    public CrmMenuItems UnregisterMenu
    {
      get
      {
        return this._unregisterMenu;
      }
      set
      {
        this.SetProperty<CrmMenuItems>(ref this._unregisterMenu, value, "UnregisterMenu");
      }
    }

    public CrmMenuItems RefreshMenu
    {
      get
      {
        return this._refreshMenu;
      }
      set
      {
        this.SetProperty<CrmMenuItems>(ref this._refreshMenu, value, "RefreshMenu");
      }
    }

    public CrmMenuItems SearchMenu
    {
      get
      {
        return this._searchMenu;
      }
      set
      {
        this.SetProperty<CrmMenuItems>(ref this._searchMenu, value, "SearchMenu");
      }
    }

    public CrmMenuItems ProfileWorkflowMenu
    {
      get
      {
        return this._profileWorkflowMenu;
      }
      set
      {
        this.SetProperty<CrmMenuItems>(ref this._profileWorkflowMenu, value, "ProfileWorkflowMenu");
      }
    }

    public GridLength DetailsTabHeight
    {
      get
      {
        return this._detailsTabHeight;
      }
      set
      {
        this.SetProperty<GridLength>(ref this._detailsTabHeight, value, "DetailsTabHeight");
      }
    }

    public Visibility BtnVisibleStatus
    {
      get
      {
        return this._btnVisibleStatus;
      }
      set
      {
        this.SetProperty<Visibility>(ref this._btnVisibleStatus, value, "BtnVisibleStatus");
      }
    }

    public bool IsEnableStatus
    {
      get
      {
        return this._isEnableStatus;
      }
      set
      {
        this.SetProperty<bool>(ref this._isEnableStatus, value, "IsEnableStatus");
      }
    }

    public Visibility VisibilityStatus
    {
      get
      {
        return this._visibilityStatus;
      }
      set
      {
        this.SetProperty<Visibility>(ref this._visibilityStatus, value, "VisibilityStatus");
      }
    }

    public string ProgressStatusTxt
    {
      get
      {
        return this._progressStatusTxt;
      }
      set
      {
        this.SetProperty<string>(ref this._progressStatusTxt, value, "ProgressStatusTxt");
      }
    }

    public RelayCommand Click_OK
    {
      get
      {
        return this._click_OK ?? (this._click_OK = new RelayCommand((Action<object>) (s => this.ProgOK_Clicked())));
      }
    }

    public CrmOrganization Organization
    {
      get
      {
        return this._org;
      }
    }

    public DataTable GridTable
    {
      get
      {
        return this._gridTable;
      }
      set
      {
        this.SetProperty<DataTable>(ref this._gridTable, value, "GridTable");
      }
    }

    public RelayCommand CmdMenuItems
    {
      get
      {
        return this._cmdMenuItems ?? (this._cmdMenuItems = new RelayCommand(new Action<object>(this.MenuItem_Clicked)));
      }
    }

    public ObservableCollection<ICrmTreeNode> AssembliesTree
    {
      get
      {
        return this._assembliesTree;
      }
      set
      {
        this.SetProperty<ObservableCollection<ICrmTreeNode>>(ref this._assembliesTree, value, "AssembliesTree");
      }
    }

    public ObservableCollection<CrmMenuItems> CrmMenuItemSource
    {
      get
      {
        return this._crmMenuItemSource;
      }
      set
      {
        this.SetProperty<ObservableCollection<CrmMenuItems>>(ref this._crmMenuItemSource, value, "CrmMenuItemSource");
      }
    }

    public ObservableCollection<CrmMenuItems> CrmContextMenuSource
    {
      get
      {
        return this._crmContextMenuSource;
      }
      set
      {
        this.SetProperty<ObservableCollection<CrmMenuItems>>(ref this._crmContextMenuSource, value, "CrmContextMenuSource");
      }
    }

    public ObservableCollection<CrmMenuItems> CrmDefaultContextMenuSource
    {
      get
      {
        return this._crmDefaultContextMenuSource;
      }
      set
      {
        this.SetProperty<ObservableCollection<CrmMenuItems>>(ref this._crmDefaultContextMenuSource, value, "CrmDefaultContextMenuSource");
      }
    }

    public object SelectedItem
    {
      get
      {
        return this._selectedItem;
      }
      set
      {
        this.SetProperty<object>(ref this._selectedItem, value, "SelectedItem");
      }
    }

    public bool TreeNodeExpanded
    {
      get
      {
        return this._treeNodeExpanded;
      }
      set
      {
        this.SetProperty<bool>(ref this._treeNodeExpanded, value, "TreeNodeExpanded");
      }
    }

    [Import(typeof (IPluginRegistrationView), AllowDefault = true, AllowRecomposition = false)]
    public IPluginRegistrationView PluginView { get; set; }

    [Import(typeof (IDebuggerView), AllowDefault = true, AllowRecomposition = false)]
    public IDebuggerView DebugView { get; set; }

    [ImportMany(typeof (IPluginRegistrationView), AllowRecomposition = true)]
    public IEnumerable<Lazy<IPluginRegistrationView, IPluginMetadata>> Plugins { get; set; }

    public OrganizationControlViewModel(string tempViewstatus, CrmOrganization crmOrganization, MainViewModel mainViewModel, OrganizationControlView orgControlView, double detailsTabHeight = 0.0)
    {
      this._mainViewModel = mainViewModel;
      this._orgControlView = orgControlView;
      this.PropertyGridLoad();
      this.DetailsTabHeight = detailsTabHeight != 0.0 ? new GridLength(detailsTabHeight, GridUnitType.Pixel) : new GridLength(300.0, GridUnitType.Pixel);
      if (crmOrganization != null)
        this._org = crmOrganization;
      this._menuActionType = MenuActionType.Initial;
      if (OrganizationControlViewModel._mEntitySorter == null)
        OrganizationControlViewModel._mEntitySorter = new CrmEntitySorter();
      this._currentView = CrmViewType.Assembly;
      if (this._mainViewModel._isReload)
      {
        switch (tempViewstatus)
        {
          case "Assembly":
            this._currentView = CrmViewType.Assembly;
            break;
          case "Message":
            this._currentView = CrmViewType.Message;
            break;
          case "Entity":
            this._currentView = CrmViewType.Entity;
            break;
        }
      }
      else if (this._mainViewModel._viewTypeStatus.ContainsKey(this._org.OrganizationId) && this._mainViewModel.OrganizationsTab.SelectedOrganization != null)
      {
        TabItem selectedOrganization = this._mainViewModel.OrganizationsTab.SelectedOrganization;
        if (selectedOrganization != null)
        {
          string str;
          this._mainViewModel._viewTypeStatus.TryGetValue((selectedOrganization.Tag as CrmOrganization).OrganizationId, out str);
          switch (str)
          {
            case "Assembly":
              this._currentView = CrmViewType.Assembly;
              break;
            case "Message":
              this._currentView = CrmViewType.Message;
              break;
            case "Entity":
              this._currentView = CrmViewType.Entity;
              break;
          }
        }
      }
      this.LoadImages();
      this.LoadMenuItems();
      this.LoadContextMenuItems();
      this.LoadNodes();
      this.LoadUserControlContextMenu();
    }

    private void LoadUserControlContextMenu()
    {
      this.CrmDefaultContextMenuSource = new ObservableCollection<CrmMenuItems>();
      this.CrmDefaultContextMenuSource.Insert(0, this.NewAssemblyMenu);
      this.CrmDefaultContextMenuSource.Insert(1, this.NewStepMenu);
      this.CrmDefaultContextMenuSource.Insert(2, this.NewImageMenu);
      this.CrmDefaultContextMenuSource.Insert(3, this.NewServiceMenu);
      this.CrmDefaultContextMenuSource.Insert(4, (CrmMenuItems) null);
      if (this._org.ProfilerPlugin == null)
        this.StopAllnUninstallProfile = new CrmMenuItems()
        {
          Name = "Install Pr_ofiler",
          CrmMenuSubItems = (ObservableCollection<CrmMenuItems>) null,
          ImagePath = this._icons[CrmImageType.InstallProfiler]
        };
      else
        this.StopAllnUninstallProfile = new CrmMenuItems()
        {
          Name = "Stop All P_rofiling & Uninstall Profiler",
          CrmMenuSubItems = (ObservableCollection<CrmMenuItems>) null,
          ImagePath = this._imgUninstallProfiler
        };
      this.CrmDefaultContextMenuSource.Insert(5, this.StopAllnUninstallProfile);
      this.CrmDefaultContextMenuSource.Insert(6, (CrmMenuItems) null);
      this.CrmDefaultContextMenuSource.Insert(7, this._refresh);
      this.CrmDefaultContextMenuSource.Insert(8, this._search);
    }

    private void PropertyGridLoad()
    {
      foreach (System.Windows.Forms.Control control in (ArrangedElementCollection) this._orgControlView.propGridEntity.Controls)
      {
        ToolStrip toolStrip = control as ToolStrip;
        ToolStripButton toolStripButton = new ToolStripButton();
        toolStripButton.Click += new EventHandler(this.PropertySave_Click);
        toolStripButton.ToolTipText = "Save";
        if (toolStrip != null)
        {
          ToolStripItemCollection items = toolStrip.Items;
          items.Add((ToolStripItem) toolStripButton);
          IEnumerator enumerator = items.GetEnumerator();
          try
          {
            while (enumerator.MoveNext())
            {
              ToolStripItem toolStripItem = (ToolStripItem) enumerator.Current;
              switch (toolStripItem.ToolTipText)
              {
                case "Categorized":
                  toolStripItem.Image = CrmResources.LoadImage(CrmImageType.Categorize);
                  continue;
                case "Alphabetical":
                  toolStripItem.Image = CrmResources.LoadImage(CrmImageType.Alphabetical);
                  continue;
                case "Property Pages":
                  toolStripItem.Image = CrmResources.LoadImage(CrmImageType.PropertiesPage);
                  continue;
                case "Save":
                  toolStripItem.Image = CrmResources.LoadImage(CrmImageType.Save);
                  continue;
                default:
                  continue;
              }
            }
            break;
          }
          finally
          {
            IDisposable disposable = enumerator as IDisposable;
            if (disposable != null)
              disposable.Dispose();
          }
        }
      }
    }

    private void LoadMenuItems()
    {
      if (this.CrmMenuItemSource == null)
        this.CrmMenuItemSource = new ObservableCollection<CrmMenuItems>();
      else
        this.CrmMenuItemSource.Clear();
      this.LoadRegisterMenu(false);
      this.LoadViewMenu();
      this.LoadProfilerSettings(false);
      this.LoadNodeActions(false);
      this.LoadTreeActions(false);
      this.ChangeSelection(this._currentView);
    }

    private void LoadContextMenuItems()
    {
      if (this.CrmContextMenuSource == null)
        this.CrmContextMenuSource = new ObservableCollection<CrmMenuItems>();
      else
        this.CrmContextMenuSource.Clear();
      this.LoadRegisterMenu(true);
      this.LoadTreeActions(true);
      this.LoadProfilerSettings(true);
      this.LoadNodeActions(true);
    }

    private void LoadRegisterMenu(bool isContextMenu = false)
    {
      if (this.RegisterMenu == null)
        this.RegisterMenu = new CrmMenuItems();
      if (this.NewAssemblyMenu == null)
        this.NewAssemblyMenu = new CrmMenuItems();
      if (this.NewStepMenu == null)
        this.NewStepMenu = new CrmMenuItems();
      if (this.NewImageMenu == null)
        this.NewImageMenu = new CrmMenuItems();
      if (this.NewServiceMenu == null)
        this.NewServiceMenu = new CrmMenuItems();
      this.NewAssemblyMenu.Name = "Register New _Assembly";
      this.NewAssemblyMenu.CrmMenuSubItems = (ObservableCollection<CrmMenuItems>) null;
      this.NewAssemblyMenu.ImagePath = this._icons[CrmImageType.RegisterNewAssembly];
      this.NewAssemblyMenu.InputGestureText = "Ctrl+A";
      this.NewStepMenu.Name = "Register New S_tep";
      this.NewStepMenu.CrmMenuSubItems = (ObservableCollection<CrmMenuItems>) null;
      this.NewStepMenu.ImagePath = this._icons[CrmImageType.RegisterNewStep];
      this.NewStepMenu.InputGestureText = "Ctrl+T";
      this.NewImageMenu.Name = "Register New _Image";
      this.NewImageMenu.CrmMenuSubItems = (ObservableCollection<CrmMenuItems>) null;
      this.NewImageMenu.ImagePath = this._icons[CrmImageType.RegisterNewImage];
      this.NewImageMenu.InputGestureText = "Ctrl+I";
      this.NewServiceMenu.Name = "Register New Service _Endpoint";
      this.NewServiceMenu.CrmMenuSubItems = (ObservableCollection<CrmMenuItems>) null;
      this.NewServiceMenu.ImagePath = this._icons[CrmImageType.RegisterNewServiceEndpoint];
      this.NewServiceMenu.InputGestureText = "Ctrl+E";
      if (!isContextMenu)
      {
        this.RegisterMenu.Name = "_Register";
        this.RegisterMenu.ImagePath = this._icons[CrmImageType.Register];
        this.RegisterMenu.CrmMenuSubItems = new ObservableCollection<CrmMenuItems>();
        this.RegisterMenu.CrmMenuSubItems.Insert(0, this.NewAssemblyMenu);
        this.RegisterMenu.CrmMenuSubItems.Insert(1, this.NewStepMenu);
        this.RegisterMenu.CrmMenuSubItems.Insert(2, this.NewImageMenu);
        this.RegisterMenu.CrmMenuSubItems.Insert(3, this.NewServiceMenu);
        this.CrmMenuItemSource.Insert(0, this.RegisterMenu);
      }
      else
      {
        this.CrmContextMenuSource.Insert(0, this.NewAssemblyMenu);
        this.CrmContextMenuSource.Insert(1, this.NewStepMenu);
        this.CrmContextMenuSource.Insert(2, this.NewImageMenu);
        this.CrmContextMenuSource.Insert(3, this.NewServiceMenu);
        this.CrmContextMenuSource.Insert(4, (CrmMenuItems) null);
      }
    }

    private void LoadViewMenu()
    {
      if (this.ViewMenu == null)
        this.ViewMenu = new CrmMenuItems();
      this.ViewMenu.Name = "View";
      this.ViewMenu.ImagePath = this._icons[CrmImageType.View];
      this.ViewMenu.CrmMenuSubItems = new ObservableCollection<CrmMenuItems>();
      if (this.DisplayAssemblyMenu == null)
        this.DisplayAssemblyMenu = new CrmMenuItems();
      if (this.DisplayEntityMenu == null)
        this.DisplayEntityMenu = new CrmMenuItems();
      if (this.DisplayMessageMenu == null)
        this.DisplayMessageMenu = new CrmMenuItems();
      this.DisplayAssemblyMenu.Name = "Display by Assembly";
      this.DisplayAssemblyMenu.CrmMenuSubItems = (ObservableCollection<CrmMenuItems>) null;
      this.DisplayAssemblyMenu.IsCheckable = true;
      this.DisplayAssemblyMenu.ImagePath = this._nodeImageList[CrmTreeNodeImageType.Assembly];
      this.DisplayAssemblyMenu.InputGestureText = "Ctrl+Shift+A";
      this.DisplayEntityMenu.Name = "Display by Entity";
      this.DisplayEntityMenu.CrmMenuSubItems = (ObservableCollection<CrmMenuItems>) null;
      this.DisplayEntityMenu.IsCheckable = true;
      this.DisplayEntityMenu.ImagePath = this._nodeImageList[CrmTreeNodeImageType.MessageEntity];
      this.DisplayEntityMenu.InputGestureText = "Ctrl+Shift+E";
      this.DisplayMessageMenu.Name = "Display by Message";
      this.DisplayMessageMenu.CrmMenuSubItems = (ObservableCollection<CrmMenuItems>) null;
      this.DisplayMessageMenu.IsCheckable = true;
      this.DisplayMessageMenu.ImagePath = this._nodeImageList[CrmTreeNodeImageType.Message];
      this.DisplayMessageMenu.InputGestureText = "Ctrl+Shift+M";
      this.ViewMenu.CrmMenuSubItems.Insert(0, this.DisplayAssemblyMenu);
      this.ViewMenu.CrmMenuSubItems.Insert(1, this.DisplayEntityMenu);
      this.ViewMenu.CrmMenuSubItems.Insert(2, this.DisplayMessageMenu);
      this.CrmMenuItemSource.Insert(1, this.ViewMenu);
    }

    private void LoadProfilerSettings(bool isContextMenu = false)
    {
      if (OrganizationHelper.IsProfilerSupported)
      {
        if (this.InstallProfilerMenu == null)
          this.InstallProfilerMenu = new CrmMenuItems();
        if (this._org.ProfilerPlugin != null)
        {
          this.InstallProfilerMenu.Name = "Uninstall Pr_ofiler";
          this.InstallProfilerMenu.ImagePath = this._imgUninstallProfiler;
          this.InstallProfilerMenu.CrmMenuSubItems = (ObservableCollection<CrmMenuItems>) null;
        }
        else
        {
          this.InstallProfilerMenu.Name = "Install Pr_ofiler";
          this.InstallProfilerMenu.ImagePath = this._icons[CrmImageType.InstallProfiler];
          this.InstallProfilerMenu.CrmMenuSubItems = (ObservableCollection<CrmMenuItems>) null;
        }
      }
      if (this.DebugMenu == null)
        this.DebugMenu = new CrmMenuItems();
      this.DebugMenu.Name = "D_ebug";
      this.DebugMenu.ImagePath = this._icons[CrmImageType.Debug];
      this.DebugMenu.CrmMenuSubItems = (ObservableCollection<CrmMenuItems>) null;
      switch (this._menuActionType)
      {
        case MenuActionType.Initial:
          if (isContextMenu)
            break;
          this.CrmMenuItemSource.Insert(2, this.InstallProfilerMenu);
          this.CrmMenuItemSource.Insert(3, this.DebugMenu);
          break;
        case MenuActionType.SelectionChanged:
          if (this.SelectedItem is CrmPlugin)
          {
            if (OrganizationHelper.IsProfilerSupported && (this.SelectedItem as CrmPlugin).IsProfilerPlugin)
            {
              if (this.ProfilingMenu == null)
                this.ProfilingMenu = new CrmMenuItems();
              this.ProfilingMenu.Name = "Start Pro_filing";
              this.ProfilingMenu.ImagePath = this._icons[CrmImageType.StartProfiling];
              this.ProfilingMenu.CrmMenuSubItems = (ObservableCollection<CrmMenuItems>) null;
              this.ProfileWorkflowMenu = new CrmMenuItems();
              this.ProfileWorkflowMenu.Name = "Pro_file Workflow";
              this.ProfileWorkflowMenu.ImagePath = this._icons[CrmImageType.Process];
              this.ProfileWorkflowMenu.CrmMenuSubItems = (ObservableCollection<CrmMenuItems>) null;
            }
          }
          else if (this.SelectedItem is CrmPluginStep)
          {
            CrmPluginStep crmPluginStep = this.SelectedItem as CrmPluginStep;
            if (OrganizationHelper.IsProfilerSupported && crmPluginStep.Organization != null && (crmPluginStep.Organization.ProfilerPlugin != null && !crmPluginStep.Organization.Plugins[crmPluginStep.PluginId].IsProfilerPlugin) && (crmPluginStep.IsProfiled || crmPluginStep.Enabled))
            {
              if (this.ProfilingMenu == null)
                this.ProfilingMenu = new CrmMenuItems();
              if (crmPluginStep.IsProfiled)
              {
                this.ProfilingMenu.Name = "Stop Pro_filing";
                this.ProfilingMenu.ImagePath = this._icons[CrmImageType.StopProfiling];
                this.ProfilingMenu.CrmMenuSubItems = (ObservableCollection<CrmMenuItems>) null;
              }
              else if (crmPluginStep.Enabled)
              {
                this.ProfilingMenu.Name = "Start Pro_filing";
                this.ProfilingMenu.ImagePath = this._icons[CrmImageType.StartProfiling];
                this.ProfilingMenu.CrmMenuSubItems = (ObservableCollection<CrmMenuItems>) null;
              }
            }
          }
          if (this.SelectedItem == null)
            break;
          switch ((this.SelectedItem as ICrmTreeNode).NodeType)
          {
            case CrmTreeNodeType.MessageEntity:
            case CrmTreeNodeType.ServiceEndpoint:
            case CrmTreeNodeType.Image:
            case CrmTreeNodeType.Message:
            case CrmTreeNodeType.Assembly:
              if (isContextMenu)
                return;
              this.CrmMenuItemSource.Insert(2, this.InstallProfilerMenu);
              this.CrmMenuItemSource.Insert(3, this.DebugMenu);
              return;
            case CrmTreeNodeType.WorkflowActivity:
              return;
            case CrmTreeNodeType.Step:
              CrmPluginStep crmPluginStep1 = this.SelectedItem as CrmPluginStep;
              if (!isContextMenu)
              {
                this.CrmMenuItemSource.Insert(2, this.InstallProfilerMenu);
                this.CrmMenuItemSource.Insert(3, this.DebugMenu);
                if (!OrganizationHelper.IsProfilerSupported || crmPluginStep1.Organization == null || (crmPluginStep1.Organization.ProfilerPlugin == null || crmPluginStep1.Organization.Plugins[crmPluginStep1.PluginId].IsProfilerPlugin) || !crmPluginStep1.IsProfiled && !crmPluginStep1.Enabled)
                  return;
                this.CrmMenuItemSource.Insert(4, this.ProfilingMenu);
                return;
              }
              if (!OrganizationHelper.IsProfilerSupported || crmPluginStep1.Organization == null || (crmPluginStep1.Organization.ProfilerPlugin == null || crmPluginStep1.Organization.Plugins[crmPluginStep1.PluginId].IsProfilerPlugin) || !crmPluginStep1.IsProfiled && !crmPluginStep1.Enabled)
                return;
              this.CrmContextMenuSource.Add(this.ProfilingMenu);
              this.CrmContextMenuSource.Add((CrmMenuItems) null);
              return;
            case CrmTreeNodeType.Plugin:
              if (!isContextMenu)
              {
                this.CrmMenuItemSource.Insert(2, this.InstallProfilerMenu);
                this.CrmMenuItemSource.Insert(3, this.DebugMenu);
                if (!OrganizationHelper.IsProfilerSupported || this.SelectedItem == null || !(this.SelectedItem as CrmPlugin).IsProfilerPlugin)
                  return;
                this.CrmMenuItemSource.Insert(4, this.ProfileWorkflowMenu);
                return;
              }
              if (!OrganizationHelper.IsProfilerSupported || this.SelectedItem == null || !(this.SelectedItem as CrmPlugin).IsProfilerPlugin)
                return;
              this.ProfileWorkflowMenu.Name = "Start Profiling Workflow";
              this.CrmContextMenuSource.Add(this.ProfileWorkflowMenu);
              this.CrmContextMenuSource.Add((CrmMenuItems) null);
              return;
            default:
              return;
          }
      }
    }

    private void LoadNodeActions(bool isContextMenu = false)
    {
      if (this.UpdateMenu == null)
        this.UpdateMenu = new CrmMenuItems();
      if (this.DisableMenu == null)
        this.DisableMenu = new CrmMenuItems();
      this.UnregisterMenu = new CrmMenuItems();
      if (this.EnableMenu == null)
        this.EnableMenu = new CrmMenuItems();
      this.UpdateMenu.Name = "_Update";
      this.UpdateMenu.ImagePath = this._icons[CrmImageType.Update];
      this.UpdateMenu.CrmMenuSubItems = (ObservableCollection<CrmMenuItems>) null;
      this.DisableMenu.Name = "_Disable";
      this.DisableMenu.ImagePath = this._nodeImageList[CrmTreeNodeImageType.StepDisabled];
      this.DisableMenu.CrmMenuSubItems = (ObservableCollection<CrmMenuItems>) null;
      this.EnableMenu.Name = "_Enable";
      this.EnableMenu.ImagePath = this._nodeImageList[CrmTreeNodeImageType.StepEnabled];
      this.EnableMenu.CrmMenuSubItems = (ObservableCollection<CrmMenuItems>) null;
      this.UnregisterMenu.Name = "U_nregister";
      this.UnregisterMenu.ImagePath = this._icons[CrmImageType.Unregister];
      this.UnregisterMenu.CrmMenuSubItems = (ObservableCollection<CrmMenuItems>) null;
      this.UnregisterMenu.IsEnabled = this.SelectedItem == null || !this.IsNodeSystemItem(this.SelectedItem as ICrmTreeNode);
      if (isContextMenu)
        this.UnregisterMenu.InputGestureText = "Del";
      switch (this._menuActionType)
      {
        case MenuActionType.Initial:
          if (!isContextMenu)
          {
            this.CrmMenuItemSource.Add(this.UnregisterMenu);
            break;
          }
          this.CrmContextMenuSource.Add(this.UnregisterMenu);
          break;
        case MenuActionType.SelectionChanged:
          if (this.SelectedItem == null)
            break;
          switch ((this.SelectedItem as ICrmTreeNode).NodeType)
          {
            case CrmTreeNodeType.MessageEntity:
            case CrmTreeNodeType.Message:
              if (!isContextMenu)
              {
                this.CrmMenuItemSource.Add(this.UnregisterMenu);
                return;
              }
              this.CrmContextMenuSource.Add(this.UnregisterMenu);
              return;
            case CrmTreeNodeType.ServiceEndpoint:
            case CrmTreeNodeType.Image:
            case CrmTreeNodeType.Assembly:
              if (!isContextMenu)
              {
                this.CrmMenuItemSource.Add(this.UpdateMenu);
                this.CrmMenuItemSource.Add(this.UnregisterMenu);
                return;
              }
              this.CrmContextMenuSource.Add(this.UpdateMenu);
              this.CrmContextMenuSource.Add(this.UnregisterMenu);
              return;
            case CrmTreeNodeType.WorkflowActivity:
              if (!isContextMenu)
              {
                this.CrmMenuItemSource.Add(this.UnregisterMenu);
                return;
              }
              this.CrmContextMenuSource.Add(this.UnregisterMenu);
              return;
            case CrmTreeNodeType.Step:
              CrmPluginStep crmPluginStep = this.SelectedItem as CrmPluginStep;
              if (!isContextMenu)
              {
                this.CrmMenuItemSource.Add(this.UpdateMenu);
                if (!crmPluginStep.IsProfiled)
                {
                  if (!crmPluginStep.Enabled)
                    this.CrmMenuItemSource.Add(this.EnableMenu);
                  else
                    this.CrmMenuItemSource.Add(this.DisableMenu);
                }
                this.CrmMenuItemSource.Add(this.UnregisterMenu);
                return;
              }
              this.CrmContextMenuSource.Add(this.UpdateMenu);
              if (!crmPluginStep.IsProfiled)
              {
                if (!crmPluginStep.Enabled)
                  this.CrmContextMenuSource.Add(this.EnableMenu);
                else
                  this.CrmContextMenuSource.Add(this.DisableMenu);
              }
              this.CrmContextMenuSource.Add(this.UnregisterMenu);
              return;
            case CrmTreeNodeType.Plugin:
              if (!isContextMenu)
              {
                this.CrmMenuItemSource.Add(this.UnregisterMenu);
                return;
              }
              this.CrmContextMenuSource.Add(this.UnregisterMenu);
              return;
            default:
              return;
          }
        default:
          if (!isContextMenu)
          {
            this.CrmMenuItemSource.Add(this.UpdateMenu);
            this.CrmMenuItemSource.Add(this.DisableMenu);
            this.CrmMenuItemSource.Add(this.UnregisterMenu);
            break;
          }
          this.CrmContextMenuSource.Add(this.UpdateMenu);
          this.CrmContextMenuSource.Add(this.DisableMenu);
          this.CrmContextMenuSource.Add(this.UnregisterMenu);
          break;
      }
    }

    private void LoadTreeActions(bool isContextMenu = false)
    {
      this._refresh = new CrmMenuItems()
      {
        Name = "Re_fresh",
        ImagePath = this._icons[CrmImageType.Refresh],
        CrmMenuSubItems = (ObservableCollection<CrmMenuItems>) null
      };
      this._search = new CrmMenuItems()
      {
        Name = "_Search",
        ImagePath = this._icons[CrmImageType.Search],
        CrmMenuSubItems = (ObservableCollection<CrmMenuItems>) null
      };
      if (!isContextMenu)
      {
        this.CrmMenuItemSource.Add(this._refresh);
        this.CrmMenuItemSource.Add(this._search);
      }
      else
      {
        this._refresh.InputGestureText = "F5";
        this._search.InputGestureText = "Ctrl+F";
        this.CrmContextMenuSource.Add(this._refresh);
        this.CrmContextMenuSource.Add(this._search);
        this.CrmContextMenuSource.Add((CrmMenuItems) null);
      }
    }

    public void AddStep(CrmPluginStep step)
    {
      if (step == null)
        throw new ArgumentNullException("step");
      switch (this._currentView)
      {
        case CrmViewType.Assembly:
          Guid guid = step.ServiceBusConfigurationId == Guid.Empty ? step.PluginId : step.ServiceBusConfigurationId;
          break;
        case CrmViewType.Message:
        case CrmViewType.Entity:
          Guid nodeId = this.CreateCrmTreeNodes(this._currentView, step.MessageId, step.MessageEntityId, true).NodeId;
          if (this._stepParentList.ContainsKey(step.StepId))
            break;
          this._stepParentList.Add(step.StepId, nodeId);
          break;
        default:
          throw new NotImplementedException("View = " + this._currentView.ToString());
      }
    }

    public void RefreshStep(CrmPluginStep step)
    {
      if (step == null)
        throw new ArgumentNullException("step");
    }

    private CrmTreeNode CreateCrmTreeNodes(CrmViewType view, Guid messageId, Guid messageEntityId, bool addToTree)
    {
      if (Guid.Empty == messageId)
        throw new ArgumentException("Invalid Guid", "messageId");
      CrmTreeNode crmTreeNode;
      CrmTreeNode node;
      switch (view)
      {
        case CrmViewType.Message:
          crmTreeNode = new CrmTreeNode(this._org.Messages[messageId]);
          node = !(Guid.Empty == messageEntityId) ? new CrmTreeNode(this._org.MessageEntities[messageEntityId]) : new CrmTreeNode(new CrmMessageEntity(this.Organization, messageId, Guid.Empty, "none", "none", CrmPluginStepDeployment.Both, new DateTime?(), new DateTime?()));
          break;
        case CrmViewType.Entity:
          crmTreeNode = !(Guid.Empty == messageEntityId) ? new CrmTreeNode(this._org.MessageEntities[messageEntityId]) : new CrmTreeNode(new CrmMessageEntity(this.Organization, messageId, Guid.Empty, "none", "none", CrmPluginStepDeployment.Both, new DateTime?(), new DateTime?()));
          node = new CrmTreeNode(this._org.Messages[messageId]);
          break;
        default:
          throw new NotImplementedException("View = " + view.ToString());
      }
      Guid nodeId1 = crmTreeNode.NodeId;
      Guid nodeId2 = node.NodeId;
      if (this._rootNodeList.ContainsKey(crmTreeNode.NodeText))
      {
        crmTreeNode = this._rootNodeList[crmTreeNode.NodeText];
      }
      else
      {
        crmTreeNode.NodeId = Guid.NewGuid();
        this._rootNodeList.Add(crmTreeNode.NodeText, crmTreeNode);
      }
      if (crmTreeNode.HasChild(node.NodeText))
      {
        node = crmTreeNode[node.NodeText];
      }
      else
      {
        node.NodeId = Guid.NewGuid();
        crmTreeNode.AddChild(node);
      }
      if (!this._viewNodeList.ContainsKey(nodeId1))
        this._viewNodeList.Add(nodeId1, crmTreeNode.NodeId);
      if (!this._viewNodeList.ContainsKey(nodeId2))
        this._viewNodeList.Add(nodeId2, node.NodeId);
      return node;
    }

    internal void LoadNodes()
    {
      this.LoadNodes(this._currentView, false);
    }

    private void LoadNodes(ObservableCollection<ICrmTreeNode> nodes)
    {
      if (this.AssembliesTree == null)
        this.AssembliesTree = new ObservableCollection<ICrmTreeNode>();
      if (nodes == null || nodes.Count < 1)
        return;
      if (this._isNotRefresh)
      {
        List<ICrmTreeNode> nodes1 = Enumerable.ToList<ICrmTreeNode>((IEnumerable<ICrmTreeNode>) this.AssembliesTree);
        ExtensionMethods.RetainNodeInfo(nodes, nodes1);
      }
      this.AssembliesTree.Clear();
      this.AssembliesTree = nodes;
      this._isNotRefresh = false;
    }

    internal ObservableCollection<ICrmTreeNode> LoadNodes(CrmViewType view, bool isSearch = false)
    {
      ObservableCollection<ICrmTreeNode> nodes = new ObservableCollection<ICrmTreeNode>();
      try
      {
        switch (view)
        {
          case CrmViewType.Assembly:
            this._rootNodeList = (Dictionary<string, CrmTreeNode>) null;
            this._stepParentList = (Dictionary<Guid, Guid>) null;
            this._viewNodeList = (Dictionary<Guid, Guid>) null;
            foreach (CrmPluginAssembly crmPluginAssembly in this.Organization.Assemblies)
            {
              if (("Microsoft.Crm.ServiceBus" != crmPluginAssembly.Name || crmPluginAssembly.CustomizationLevel != 0) && (!crmPluginAssembly.IsProfilerAssembly && Guid.Empty == crmPluginAssembly.ProfiledWorkflowId))
                nodes.Add((ICrmTreeNode) crmPluginAssembly);
            }
            foreach (CrmServiceEndpoint crmServiceEndpoint in (Collection<CrmServiceEndpoint>) this.Organization.ServiceEndpoints.ToCollection())
              nodes.Add((ICrmTreeNode) crmServiceEndpoint);
            if (this.Organization.ProfilerPlugin != null)
            {
              nodes.Add((ICrmTreeNode) this.Organization.ProfilerPlugin);
              break;
            }
            break;
          case CrmViewType.Message:
          case CrmViewType.Entity:
            if (this._rootNodeList == null)
              this._rootNodeList = new Dictionary<string, CrmTreeNode>();
            else
              this._rootNodeList.Clear();
            if (this._stepParentList == null)
              this._stepParentList = new Dictionary<Guid, Guid>();
            else
              this._stepParentList.Clear();
            if (this._viewNodeList == null)
              this._viewNodeList = new Dictionary<Guid, Guid>();
            else
              this._viewNodeList.Clear();
            foreach (CrmPluginStep node in this.Organization.Steps)
            {
              if (node.MessageId != Guid.Empty)
              {
                if (this._org.MessageEntities.ContainsKey(node.MessageEntityId) || node.MessageEntityId == Guid.Empty)
                {
                  CrmTreeNode crmTreeNodes = this.CreateCrmTreeNodes(view, node.MessageId, node.MessageEntityId, false);
                  crmTreeNodes.AddChild(node);
                  this._stepParentList.Add(node.StepId, crmTreeNodes.NodeId);
                }
                else
                  Trace.TraceError(string.Format("Missing ID:{0} under Node: {1}", (object) node.MessageEntityId, (object) node.NodeText));
              }
            }
            using (Dictionary<string, CrmTreeNode>.ValueCollection.Enumerator enumerator = this._rootNodeList.Values.GetEnumerator())
            {
              while (enumerator.MoveNext())
              {
                CrmTreeNode current = enumerator.Current;
                nodes.Add((ICrmTreeNode) current);
              }
              break;
            }
          default:
            throw new NotImplementedException("View = " + view.ToString());
        }
        if (isSearch)
          return nodes;
        this.LoadNodes(nodes);
        this._currentView = view;
        if (this._mainViewModel._viewTypeStatus.ContainsKey(this._org.OrganizationId))
        {
          if (!this._mainViewModel._isReload)
            this._mainViewModel._viewTypeStatus[this._org.OrganizationId] = Convert.ToString((object) this._currentView);
        }
        else
          this._mainViewModel._viewTypeStatus.Add(this._org.OrganizationId, Convert.ToString((object) this._currentView));
      }
      catch (Exception ex)
      {
        ErrorMessageViewModel.ShowErrorMessageBox((Window) null, "Unable to change the view", "View Error", ex, (System.Windows.Controls.UserControl) null);
      }
      return nodes;
    }

    private void LoadImages()
    {
      this._nodeImageList = CrmResources.LoadImage(CrmTreeNodeImageType.Assembly, CrmTreeNodeImageType.Image, CrmTreeNodeImageType.Message, CrmTreeNodeImageType.MessageEntity, CrmTreeNodeImageType.StepDisabled, CrmTreeNodeImageType.StepEnabled, CrmTreeNodeImageType.ServiceEndpoint, CrmTreeNodeImageType.Plugin);
      this._imgUninstallProfiler = CrmResources.LoadImage("UninstallProfiler");
      this._icons = CrmResources.LoadImage(CrmImageType.Register, CrmImageType.RegisterNewAssembly, CrmImageType.RegisterNewImage, CrmImageType.RegisterNewServiceEndpoint, CrmImageType.RegisterNewStep, CrmImageType.View, CrmImageType.InstallProfiler, CrmImageType.Debug, CrmImageType.Update, CrmImageType.Unregister, CrmImageType.Refresh, CrmImageType.Search, CrmImageType.StopProfiling, CrmImageType.StartProfiling, CrmImageType.Process);
    }

    public void SelectionChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
    {
      this.SelectedItem = e.NewValue;
      if (this.SelectedItem == null)
        return;
      this._menuActionType = MenuActionType.SelectionChanged;
      this.LoadMenuItems();
      this.LoadContextMenuItems();
      ICrmTreeNode node = this.SelectedItem as ICrmTreeNode;
      bool flag = this.IsNodeSystemItem(node);
      switch (node.NodeType)
      {
        case CrmTreeNodeType.MessageEntity:
        case CrmTreeNodeType.Message:
          CrmTreeNode crmTreeNode1 = (CrmTreeNode) this.SelectedItem;
          switch (crmTreeNode1.ChildNodeType)
          {
            case CrmTreeNodeType.Step:
              this.GridTable = OrganizationHelper.CreateDataTable<CrmPluginStep>(CrmPluginStep.Columns, (IEnumerable<CrmPluginStep>) crmTreeNode1.ToEntityArray(CrmTreeNodeType.Step));
              break;
            case CrmTreeNodeType.Message:
              this.GridTable = OrganizationHelper.CreateDataTable<CrmMessage>(CrmMessage.Columns, (IEnumerable<CrmMessage>) crmTreeNode1.ToEntityArray(CrmTreeNodeType.Message));
              break;
            case CrmTreeNodeType.MessageEntity:
              this.GridTable = OrganizationHelper.CreateDataTable<CrmMessageEntity>(CrmMessageEntity.Columns, (IEnumerable<CrmMessageEntity>) crmTreeNode1.ToEntityArray(CrmTreeNodeType.MessageEntity));
              break;
            default:
              this.GridTable = (DataTable) null;
              break;
          }
              break;
          case CrmTreeNodeType.ServiceEndpoint:
          this.GridTable = OrganizationHelper.CreateDataTable<CrmPluginStep>(CrmPluginStep.Columns, (IEnumerable<CrmPluginStep>) ((CrmServiceEndpoint) node).Steps);
          break;
        case CrmTreeNodeType.Image:
          this.GridTable = (DataTable) null;
          break;
        case CrmTreeNodeType.WorkflowActivity:
        case CrmTreeNodeType.Plugin:
          this.GridTable = OrganizationHelper.CreateDataTable<CrmPluginStep>(CrmPluginStep.Columns, (IEnumerable<CrmPluginStep>) ((CrmPlugin) node).Steps);
          break;
        case CrmTreeNodeType.Step:
          this.GridTable = OrganizationHelper.CreateDataTable<CrmPluginImage>(CrmPluginImage.Columns, (IEnumerable<CrmPluginImage>) ((CrmPluginStep) node).Images);
          break;
        case CrmTreeNodeType.Assembly:
          if (!flag)
          {
            this.GridTable = OrganizationHelper.CreateDataTable<CrmPlugin>(CrmPlugin.Columns, (IEnumerable<CrmPlugin>) ((CrmPluginAssembly) node).Plugins);
            break;
          }
          break;
      }
      CrmTreeNode crmTreeNode2 = node as CrmTreeNode;
      this._orgControlView.propGridEntity.SelectedObject = crmTreeNode2 != null ? (object) crmTreeNode2.Entity : (object) node;
      this._menuActionType = MenuActionType.Initial;
    }

    private void MenuItem_Clicked(object sender)
    {
      bool flag = false;
      if (!(sender is string))
        return;
      switch (sender as string)
      {
        case "Register New _Assembly":
          this.NewAssembly_Clicked();
          break;
        case "Register New _Image":
          this.NewImage_Clicked();
          break;
        case "Register New S_tep":
          this.NewStep_Clicked();
          break;
        case "Register New Service _Endpoint":
          this.NewServiceEnpoint_Clicked();
          break;
        case "Display by Assembly":
          this.LoadNodes(CrmViewType.Assembly, false);
          ExtensionMethods.CollapseNodes(this.AssembliesTree);
          break;
        case "Display by Entity":
          this.LoadNodes(CrmViewType.Entity, false);
          break;
        case "Display by Message":
          this.LoadNodes(CrmViewType.Message, false);
          break;
        case "Install Pr_ofiler":
        case "Uninstall Pr_ofiler":
        case "Stop All P_rofiling & Uninstall Profiler":
          this.InstallProfiler_Clicked();
          break;
        case "D_ebug":
          this.Debug_Clicked();
          break;
        case "Pro_file Workflow":
        case "Start Profiling Workflow":
        case "Start Pro_filing":
        case "Stop Pro_filing":
          this.StartProfiler_Clicked();
          break;
        case "_Update":
          this.Update_Clicked();
          break;
        case "_Disable":
        case "_Enable":
          this.Disable_Click();
          break;
        case "U_nregister":
          this.Unregister_Clicked();
          break;
        case "Re_fresh":
          this.Refresh_Clicked();
          break;
        case "_Search":
          this.Search_Clicked();
          this._menuActionType = MenuActionType.SelectionChanged;
          flag = true;
          break;
      }
      this.LoadMenuItems();
      this.LoadContextMenuItems();
      if (!Enumerable.Contains<string>((IEnumerable<string>) new string[3]
      {
        "Display by Assembly",
        "Display by Entity",
        "Display by Message"
      }, sender as string))
        this._isNotRefresh = true;
      if (!flag)
        this.LoadNodes();
      else
        this._menuActionType = MenuActionType.Initial;
    }

    public void MouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
      DependencyObject reference = (DependencyObject) e.OriginalSource;
      while (!(reference is System.Windows.Controls.Control))
        reference = VisualTreeHelper.GetParent(reference);
      if (reference == null || !(reference.GetType().Name == "ContentControl"))
        return;
      this.MenuItem_Clicked((object) "_Update");
    }

    private void NewServiceEnpoint_Clicked()
    {
      this.LoadServiceBusConfigUserControl((CrmServiceEndpoint) null);
    }

    private void NewStep_Clicked()
    {
      CrmPlugin assmbly = (CrmPlugin) null;
      CrmServiceEndpoint serviceEndPoint = (CrmServiceEndpoint) null;
      if (this.SelectedItem != null)
      {
        Guid index1 = Guid.Empty;
        ICrmTreeNode crmTreeNode = this.SelectedItem as ICrmTreeNode;
        Guid index2;
        switch (crmTreeNode.NodeType)
        {
          case CrmTreeNodeType.MessageEntity:
          case CrmTreeNodeType.Message:
            index2 = Guid.Empty;
            break;
          case CrmTreeNodeType.ServiceEndpoint:
            index2 = ((CrmServiceEndpoint) this.SelectedItem).PluginId;
            index1 = ((CrmServiceEndpoint) this.SelectedItem).NodeId;
            break;
          case CrmTreeNodeType.Process:
            index2 = ((CrmProfiledProcess) this.SelectedItem).PluginId;
            break;
          case CrmTreeNodeType.Image:
            index2 = ((CrmPluginImage) this.SelectedItem).PluginId;
            break;
          case CrmTreeNodeType.WorkflowActivity:
          case CrmTreeNodeType.Plugin:
            index2 = crmTreeNode.NodeId;
            break;
          case CrmTreeNodeType.Step:
            index2 = ((CrmPluginStep) this.SelectedItem).PluginId;
            break;
          case CrmTreeNodeType.Assembly:
            index2 = ((CrmPluginAssembly) this.SelectedItem).Plugins == null || ((CrmPluginAssembly) this.SelectedItem).Plugins.Count <= 0 ? Guid.Empty : (Enumerable.First<CrmPlugin>((IEnumerable<CrmPlugin>) ((CrmPluginAssembly) this.SelectedItem).Plugins) != null ? Enumerable.First<CrmPlugin>((IEnumerable<CrmPlugin>) ((CrmPluginAssembly) this.SelectedItem).Plugins).PluginId : Guid.Empty);
            break;
          default:
            throw new NotImplementedException("NodeType = " + crmTreeNode.NodeType.ToString());
        }
        if (Guid.Empty != index2)
          assmbly = this._org.Plugins[index2];
        if (Guid.Empty != index1)
          serviceEndPoint = this._org.ServiceEndpoints[index1];
      }
      this.LoadStepRegistrationUserControl(this.Organization, assmbly, (CrmPluginStep) null, serviceEndPoint);
    }

    private void NewAssembly_Clicked()
    {
      this.LoadAssemblyUserControl((CrmPluginAssembly) null);
    }

    private void NewImage_Clicked()
    {
      this.LoadImageRegistrationUserControl((CrmPluginImage) null, this.SelectedItem != null ? (this.SelectedItem as ICrmTreeNode).NodeId : Guid.Empty);
    }

    private bool Update_Clicked()
    {
      bool flag = false;
      if (this.SelectedItem == null)
        return flag;
      if (this.IsNodeSystemItem(this.SelectedItem as ICrmTreeNode))
      {
        this.ShowSystemItemError("The assembly cannot be updated.");
        return true;
      }
      switch ((this.SelectedItem as ICrmTreeNode).NodeType)
      {
        case CrmTreeNodeType.ServiceEndpoint:
          flag = true;
          this.LoadServiceBusConfigUserControl((CrmServiceEndpoint) this.SelectedItem);
          goto case CrmTreeNodeType.WorkflowActivity;
        case CrmTreeNodeType.Process:
          flag = true;
          ProfilerSettingsView profilerSettingsView = new ProfilerSettingsView();
          profilerSettingsView.DataContext = (object) new ProfilerSettingsViewModel(this._org, PluginProfiler.OperationType.WorkflowActivity, Guid.Empty, profilerSettingsView);
          profilerSettingsView.Owner = System.Windows.Application.Current.MainWindow;
          profilerSettingsView.ShowDialog();
          goto case CrmTreeNodeType.WorkflowActivity;
        case CrmTreeNodeType.Step:
          CrmPluginStep step = (CrmPluginStep) this.SelectedItem;
          CrmPlugin assmbly = this._org[step.AssemblyId][step.PluginId];
          CrmServiceEndpoint serviceEndPoint = (CrmServiceEndpoint) null;
          if (step.ServiceBusConfigurationId != Guid.Empty)
            serviceEndPoint = this._org.ServiceEndpoints[step.ServiceBusConfigurationId];
          flag = true;
          this.LoadStepRegistrationUserControl(this.Organization, assmbly, step, serviceEndPoint);
          goto case CrmTreeNodeType.WorkflowActivity;
        case CrmTreeNodeType.Image:
          flag = true;
          this.LoadImageRegistrationUserControl((CrmPluginImage) this.SelectedItem, (this.SelectedItem as ICrmTreeNode).NodeId);
          goto case CrmTreeNodeType.WorkflowActivity;
        case CrmTreeNodeType.Assembly:
          flag = true;
          this.LoadAssemblyUserControl((CrmPluginAssembly) this.SelectedItem);
          goto case CrmTreeNodeType.WorkflowActivity;
        case CrmTreeNodeType.Plugin:
          CrmPlugin crmPlugin = (CrmPlugin) this.SelectedItem;
          if (!crmPlugin.IsProfilerPlugin || crmPlugin.IsManaged)
            return true;
          flag = true;
          this.LoadAssemblyUserControl(crmPlugin.Organization[crmPlugin.AssemblyId]);
          goto case CrmTreeNodeType.WorkflowActivity;
        case CrmTreeNodeType.WorkflowActivity:
          object selectedItem = this.SelectedItem;
          return flag;
        default:
          throw new NotImplementedException("NodeType = " + (this.SelectedItem as ICrmTreeNode).NodeType.ToString());
      }
    }

    private void Refresh_Clicked()
    {
      try
      {
        this._orgControlView.propGridEntity = new PropertyGrid();
        Helper.RefreshConnection(this._org, this._mainViewModel.LoadMessages(this._org), this._mainViewModel);
      }
      catch (Exception ex)
      {
        ErrorMessageViewModel.ShowErrorMessageBox((Window) null, "Unable to the refresh the organization. Connection must close.", "Connection Error", ex, (System.Windows.Controls.UserControl) null);
      }
    }

    private void Search_Clicked()
    {
      SearchView searchView = new SearchView();
      searchView.DataContext = (object) new SearchViewModel(this, this.AssembliesTree, this.SelectedItem as ICrmTreeNode, this._currentView, searchView);
      searchView.Owner = System.Windows.Application.Current.MainWindow;
      searchView.ShowDialog();
      this.SelectedItem = (searchView.DataContext as SearchViewModel).SelectedItem;
    }

    private void Disable_Click()
    {
      if (((ICrmTreeNode) this.SelectedItem).NodeType != CrmTreeNodeType.Step)
        return;
      if (this.IsNodeSystemItem(this.SelectedItem as ICrmTreeNode))
      {
        this.ShowSystemItemError("The step cannot be enabled or disabled.");
      }
      else
      {
        CrmPluginStep crmPluginStep = (CrmPluginStep) this.SelectedItem;
        string str1;
        string str2;
        if (crmPluginStep.Enabled)
        {
          str1 = "Disable";
          str2 = "disable";
        }
        else
        {
          str1 = "Enable";
          str2 = "enable";
        }
        if (System.Windows.MessageBox.Show(string.Format("Are you sure you want to {0} this step?", (object) str2), string.Format("{0} Step", (object) str1), MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.Yes) == MessageBoxResult.No)
          return;
        try
        {
          RegistrationHelper.UpdateStepStatus(this._org, crmPluginStep.StepId, !crmPluginStep.Enabled);
          crmPluginStep.Enabled = !crmPluginStep.Enabled;
          this.RefreshStep((CrmPluginStep) this.SelectedItem);
          int num = (int) System.Windows.MessageBox.Show(string.Format("Step {0}d successfully.", (object) str2), string.Format("{0} Step", (object) str1), MessageBoxButton.OK, MessageBoxImage.Asterisk);
        }
        catch (Exception ex)
        {
          ErrorMessageViewModel.ShowErrorMessageBox((Window) null, string.Format("Unable to {0} this item at ths time. An error occurred.", (object) str2), string.Format("{0} Step", (object) str1), ex, (System.Windows.Controls.UserControl) null);
        }
      }
    }

    private void Unregister_Clicked()
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      //var cDisplayClass14_1 = new OrganizationControlViewModel.\u003C\u003Ec__DisplayClass14();
      // ISSUE: reference to a compiler-generated field
      //cDisplayClass14_1.\u003C\u003E4__this = this;
      if (this.SelectedItem == null)
        return;
      if (this.IsNodeSystemItem(this.SelectedItem as ICrmTreeNode))
      {
        this.ShowSystemItemError("It cannot be unregistered.");
      }
      else
      {
        // ISSUE: reference to a compiler-generated field
        //cDisplayClass14_1.mainThread = System.Windows.Application.Current.Dispatcher.Thread;
        var mainThread = System.Windows.Application.Current.Dispatcher.Thread;
        // ISSUE: reference to a compiler-generated field
        //cDisplayClass14_1.builder = new StringBuilder();
        var builder = new StringBuilder();
        CrmPlugin crmPlugin = this.SelectedItem as CrmPlugin;
        BackgroundWorker backgroundWorker = new BackgroundWorker();
        if (crmPlugin != null && crmPlugin.IsProfilerPlugin)
        {
          this.InstallProfiler_Clicked();
        }
        else
        {
          if (System.Windows.MessageBox.Show("Are you sure you want to unregister this item?", "Unregister", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No) != MessageBoxResult.Yes)
            return;
          this.BtnVisibleStatus = Visibility.Hidden;
          this.VisibilityStatus = Visibility.Visible;
          this.ProgressStatusTxt = string.Format("Unregistering {0}...", (object) (this.SelectedItem as ICrmTreeNode).NodeType.ToString());
          ProgressBarView progressBarView = new ProgressBarView();
          progressBarView.Owner = System.Windows.Application.Current.MainWindow;
          progressBarView.DataContext = (object) this;
          this._progView = progressBarView;
          this._progView.Show();
          this._mainViewModel.IsMainViewEnabled = false;
          bool isUnregisterSuccess = false;
          backgroundWorker.DoWork += (DoWorkEventHandler) ((o, e) =>
          {
            try
            {
              ICrmEntity crmEntity = (ICrmEntity) this.SelectedItem;
              CrmOrganization org = this._org;
              ProgressIndicator progressIndicator = this._mainViewModel.ProgressIndicator;
              ICrmEntity[] crmEntityArray = new ICrmEntity[1]
              {
                crmEntity
              };
              foreach (KeyValuePair<string, int> keyValuePair in RegistrationHelper.Unregister(org, progressIndicator, crmEntityArray))
                builder.AppendLine(string.Format("{0} {1} Unregistered Successfully", (object) keyValuePair.Value, (object) keyValuePair.Key));
              crmEntity.Remove();
              this.GridTable = (DataTable) null;
              isUnregisterSuccess = true;
            }
            catch (Exception ex1)
            {
              // ISSUE: variable of a compiler-generated type
              //OrganizationControlViewModel.\u003C\u003Ec__DisplayClass14 cDisplayClass14 = cDisplayClass14_1;
              Exception ex = ex1;
              Dispatcher.FromThread(mainThread).Invoke((Action) (() =>
              {
                // ISSUE: reference to a compiler-generated field
                //cDisplayClass14.\u003C\u003E4__this.ProgOK_Clicked();
                this.ProgOK_Clicked();
                ErrorMessageViewModel.ShowErrorMessageBox((Window) null, "Unable to unregister this item an error occurred.", "Unregister Error", ex, (System.Windows.Controls.UserControl) null);
              }));
            }
            finally
            {
              this._mainViewModel.IsMainViewEnabled = true;
            }
          });
          backgroundWorker.RunWorkerCompleted += (RunWorkerCompletedEventHandler) ((o, e) =>
          {
            this.LoadNodes();
            Dispatcher.FromThread(mainThread).Invoke((Action) (() => this.ProgOK_Clicked()));
            if (isUnregisterSuccess)
            {
              int num = (int) System.Windows.MessageBox.Show(builder.ToString(), "Unregister", MessageBoxButton.OK, MessageBoxImage.Asterisk);
            }
            this._mainViewModel.IsMainViewEnabled = true;
          });
          backgroundWorker.RunWorkerAsync();
        }
      }
    }

    private void InstallProfiler_Clicked()
    {
      ProgressBarView progressBarView = new ProgressBarView();
      progressBarView.Owner = System.Windows.Application.Current.MainWindow;
      progressBarView.DataContext = (object) this;
      this._progView = progressBarView;
      Thread mainThread = Thread.CurrentThread;
      BackgroundWorker backgroundWorker = new BackgroundWorker();
      if (!OrganizationHelper.IsProfilerSupported)
        return;
      if (this._org.ProfilerPlugin != null)
      {
        if (MessageBoxResult.Yes != System.Windows.MessageBox.Show("This will delete all previously collected profiling sessions. Continue?", "Profiler Installation", MessageBoxButton.YesNo, MessageBoxImage.Asterisk, MessageBoxResult.No))
          return;
        this.ProgressStatusTxt = string.Empty;
        this._progView.Show();
        this._mainViewModel.IsMainViewEnabled = false;
        this.IsEnableStatus = false;
        this.VisibilityStatus = Visibility.Visible;
        this.ProgressStatusTxt = "Uninstalling Profiler...";
        backgroundWorker.DoWork += (DoWorkEventHandler) ((o, e) =>
        {
          try
          {
            OrganizationHelper.UninstallProfiler(this._org);
            this.ProgressStatusTxt = "Profiler Uninstalled Successfully";
            if (!ExtensionMethods.HasNode(this.AssembliesTree, this.SelectedItem as ICrmTreeNode))
              return;
            Dispatcher.FromThread(mainThread).Invoke((Action) (() => ExtensionMethods.RemoveNode(this.AssembliesTree, this.SelectedItem as ICrmTreeNode)));
          }
          catch (Exception ex)
          {
            Dispatcher.FromThread(mainThread).Invoke((Action) (() => ErrorMessageViewModel.ShowErrorMessageBox((Window) this._progView, "Unable to Uninstall the Profiler", "Profiler Installation Error", ex, (System.Windows.Controls.UserControl) null)));
            this.ProgressStatusTxt = "Profiler Uninstall Failed";
          }
        });
        backgroundWorker.RunWorkerCompleted += (RunWorkerCompletedEventHandler) ((o, e) =>
        {
          this.IsEnableStatus = true;
          this.VisibilityStatus = Visibility.Collapsed;
          this.BtnVisibleStatus = Visibility.Visible;
          this.LoadMenuItems();
          this.LoadContextMenuItems();
          this.LoadNodes();
          this.LoadUserControlContextMenu();
        });
        backgroundWorker.WorkerReportsProgress = true;
        backgroundWorker.RunWorkerAsync();
      }
      else
      {
        this._mainViewModel.IsMainViewEnabled = false;
        this.ProgressStatusTxt = string.Empty;
        this._progView.Show();
        this.IsEnableStatus = false;
        this.VisibilityStatus = Visibility.Visible;
        this.ProgressStatusTxt = "Installing Profiler...";
        backgroundWorker.DoWork += (DoWorkEventHandler) ((o, e) =>
        {
          try
          {
            CrmPlugin installedPlugin = OrganizationHelper.InstallProfiler(this._org);
            this.ProgressStatusTxt = "Profiler Installed Successfully";
            Dispatcher.FromThread(mainThread).Invoke((Action) (() => this.AssembliesTree.Add((ICrmTreeNode) installedPlugin)));
          }
          catch (Exception ex)
          {
            Dispatcher.FromThread(mainThread).Invoke((Action) (() => ErrorMessageViewModel.ShowErrorMessageBox((Window) this._progView, "Unable to Install the Profiler", "Profiler Installation Error", ex, (System.Windows.Controls.UserControl) null)));
            this.ProgressStatusTxt = "Profiler Install Failed";
          }
        });
        backgroundWorker.RunWorkerCompleted += (RunWorkerCompletedEventHandler) ((o, e) =>
        {
          this.VisibilityStatus = Visibility.Collapsed;
          this.IsEnableStatus = true;
          this.BtnVisibleStatus = Visibility.Visible;
          this._menuActionType = this.SelectedItem != null ? MenuActionType.SelectionChanged : MenuActionType.Initial;
          this.LoadMenuItems();
          this.LoadContextMenuItems();
          this.LoadNodes();
          this.LoadUserControlContextMenu();
        });
        backgroundWorker.WorkerReportsProgress = true;
        backgroundWorker.RunWorkerAsync();
      }
    }

    private void StartProfiler_Clicked()
    {
      if (!OrganizationHelper.IsProfilerSupported)
        return;
      CrmPluginStep step = this.SelectedItem as CrmPluginStep;
      if (step == null)
      {
        CrmPlugin crmPlugin = this.SelectedItem as CrmPlugin;
        if (crmPlugin == null || !crmPlugin.IsProfilerPlugin)
          return;
        ProfilerSettingsView profilerSettingsView = new ProfilerSettingsView();
        ProfilerSettingsViewModel settingsViewModel = new ProfilerSettingsViewModel(this._org, PluginProfiler.OperationType.WorkflowActivity, Guid.Empty, profilerSettingsView);
        profilerSettingsView.DataContext = (object) settingsViewModel;
        profilerSettingsView.Owner = System.Windows.Application.Current.MainWindow;
        bool? nullable = profilerSettingsView.ShowDialog();
        if (nullable.HasValue)
        {
          if (!nullable.Value)
            return;
        }
        try
        {
          foreach (Guid assemblyId in (IEnumerable<Guid>) ProfilerManagementUtility.RetrieveProfilerWorkflowAssemblies(this.Organization.CrmServiceConnection, settingsViewModel.ProfiledOperationId))
            OrganizationHelper.RefreshAssembly(this.Organization, assemblyId, true);
          OrganizationHelper.RefreshProfiledProcess(this.Organization, settingsViewModel.ProfilerOperationId);
        }
        catch (Exception ex)
        {
          ErrorMessageViewModel.ShowErrorMessageBox((Window) null, "Unable to enable profiling on this workflow due to an error. Profiling has succeeded, but you will need to click refresh.", "Profiling Error", ex, (System.Windows.Controls.UserControl) null);
        }
      }
      else if (step.IsProfiled)
      {
        try
        {
          OrganizationHelper.DisableProfiler(step);
        }
        catch (Exception ex)
        {
          ErrorMessageViewModel.ShowErrorMessageBox((Window) null, "Unable to disable profiling due to an error.", "Profiling Error", ex, (System.Windows.Controls.UserControl) null);
          return;
        }
        OrganizationHelper.RefreshStep(step.Organization, step);
        if (step.Organization.ProfilerPlugin != null)
          OrganizationHelper.RefreshPlugin(step.Organization, step.Organization.ProfilerPlugin);
        step.ProfilerStepId = new Guid?();
      }
      else
      {
        ProfilerSettingsView profilerSettingsView = new ProfilerSettingsView();
        ProfilerSettingsViewModel settingsViewModel = new ProfilerSettingsViewModel(this._org, PluginProfiler.OperationType.Plugin, step.StepId, profilerSettingsView);
        profilerSettingsView.DataContext = (object) settingsViewModel;
        profilerSettingsView.Owner = System.Windows.Application.Current.MainWindow;
        bool? nullable = profilerSettingsView.ShowDialog();
        if (nullable.HasValue && !nullable.Value)
          return;
        Guid profilerOperationId = settingsViewModel.ProfilerOperationId;
        if (step.Organization.ProfilerPlugin == null)
          return;
        OrganizationHelper.RefreshPlugin(step.Organization, step.Organization.ProfilerPlugin);
        OrganizationHelper.RefreshStep(step.Organization, step);
        step.ProfilerStepId = new Guid?(profilerOperationId);
      }
    }

    private void Debug_Clicked()
    {
      try
      {
        this.LoadDebugPluginUserControl();
      }
      catch (Exception ex)
      {
        ErrorMessageViewModel.ShowErrorMessageBox((Window) null, "Unable to debug due to an error.", "Debugging Error", ex, (System.Windows.Controls.UserControl) null);
      }
    }

    public void ProgOK_Clicked()
    {
      this._progView.Close();
      this._mainViewModel.IsMainViewEnabled = true;
    }

    private void PropertySave_Click(object sender, EventArgs e)
    {
      try
      {
        if (this.SelectedItem != null)
        {
          switch ((this.SelectedItem as ICrmTreeNode).NodeType)
          {
            case CrmTreeNodeType.Assembly:
              CrmPluginAssembly crmPluginAssembly = (CrmPluginAssembly) this.SelectedItem;
              RegistrationHelper.UpdateAssembly(this._org, crmPluginAssembly.Description, crmPluginAssembly.AssemblyId);
              int num1 = (int) System.Windows.MessageBox.Show("Assembly has been updated successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Asterisk);
              break;
            case CrmTreeNodeType.Plugin:
            case CrmTreeNodeType.WorkflowActivity:
              RegistrationHelper.UpdatePlugin(this._org, (CrmPlugin) this.SelectedItem);
              int num2 = (int) System.Windows.MessageBox.Show("Plug-in has been updated successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Asterisk);
              break;
            default:
              int num3 = (int) System.Windows.MessageBox.Show("A valid object has not been selected.", "Error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
              break;
          }
          this._isNotRefresh = true;
          this.LoadNodes();
        }
        else
        {
          int num = (int) System.Windows.MessageBox.Show("A valid object has not been selected.", "Error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
        }
      }
      catch (Exception ex)
      {
        ErrorMessageViewModel.ShowErrorMessageBox((Window) null, "Unable to Update the Assembly /Plugin due to an error.", "Update", ex, (System.Windows.Controls.UserControl) null);
      }
    }

    private bool IsNodeSystemItem(ICrmTreeNode node)
    {
      if (node is CrmTreeNode)
        return true;
      ICrmEntity crmEntity = node as ICrmEntity;
      if (crmEntity == null)
        return false;
      return crmEntity.IsSystemCrmEntity;
    }

    private void ShowSystemItemError(string text)
    {
      if (text == null)
      {
        int num1 = (int) System.Windows.MessageBox.Show("The selected item is required for the Microsoft Dynamics CRM system to work correctly.", "Microsoft Dynamics CRM", MessageBoxButton.OK, MessageBoxImage.Hand);
      }
      else
      {
        int num2 = (int) System.Windows.MessageBox.Show(string.Format("{0}\n{1}", (object) "The selected item is required for the Microsoft Dynamics CRM system to work correctly.", (object) text), "Microsoft Dynamics CRM", MessageBoxButton.OK, MessageBoxImage.Hand);
      }
    }

    internal void AddServiceEndpoint(CrmServiceEndpoint sbc)
    {
    }

    internal void RefreshServiceEndpoint(CrmServiceEndpoint m_currentServiceEndpoint)
    {
    }

    private void ChangeSelection(CrmViewType viewType)
    {
      switch (viewType)
      {
        case CrmViewType.Message:
          this.DisplayAssemblyMenu.IsSelected = false;
          this.DisplayEntityMenu.IsSelected = false;
          this.DisplayMessageMenu.IsSelected = true;
          break;
        case CrmViewType.Entity:
          this.DisplayMessageMenu.IsSelected = false;
          this.DisplayAssemblyMenu.IsSelected = false;
          this.DisplayEntityMenu.IsSelected = true;
          break;
        default:
          this.DisplayEntityMenu.IsSelected = false;
          this.DisplayMessageMenu.IsSelected = false;
          this.DisplayAssemblyMenu.IsSelected = true;
          break;
      }
    }

    internal void UpdateAutoExpand(bool newValue)
    {
    }

    private void LoadAssemblyUserControl(CrmPluginAssembly assmbly)
    {
      this.catalog = new AggregateCatalog();
      this.catalog.Catalogs.Add((ComposablePartCatalog) new DirectoryCatalog(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "AssemblyRegistration.dll"));
      this.container = new CompositionContainer((ComposablePartCatalog) this.catalog, new ExportProvider[0]);
      AttributedModelServices.ComposeExportedValue<CrmOrganization>(this.container, "CrmOrganization", this._org);
      AttributedModelServices.ComposeExportedValue<CrmPluginAssembly>(this.container, "CrmPluginAssembly", assmbly);
      this.ComposeParts();
      if (this.PluginView == null)
        return;
      (this.PluginView as Window).Owner = System.Windows.Application.Current.MainWindow;
      (this.PluginView as Window).ShowDialog();
      CrmPluginAssembly crmPluginAssembly = this.PluginView.RegisteredComponent() as CrmPluginAssembly;
      if (crmPluginAssembly == null)
        return;
      this._isNotRefresh = true;
      this.LoadNodes();
      ExtensionMethods.SelectNode(this.AssembliesTree, (ICrmTreeNode) crmPluginAssembly);
    }

    private void LoadStepRegistrationUserControl(CrmOrganization org, CrmPlugin assmbly, CrmPluginStep step, CrmServiceEndpoint serviceEndPoint)
    {
      List<Project> exportedValue1 = (List<Project>) null;
      CrmServiceClient exportedValue2 = (CrmServiceClient) null;
      IEnumerable<EntityMetadata> exportedValue3 = (IEnumerable<EntityMetadata>) null;
      EntityMetadata exportedValue4 = (EntityMetadata) null;
      this.catalog = new AggregateCatalog();
      this.catalog.Catalogs.Add((ComposablePartCatalog) new DirectoryCatalog(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "StepRegistration*.dll"));
      this.container = new CompositionContainer((ComposablePartCatalog) this.catalog, new ExportProvider[0]);
      AttributedModelServices.ComposeExportedValue<CrmOrganization>(this.container, "CrmOrganization", this._org);
      AttributedModelServices.ComposeExportedValue<CrmPlugin>(this.container, "CrmPlugin", assmbly);
      AttributedModelServices.ComposeExportedValue<CrmPluginStep>(this.container, "CrmPluginStep", step);
      AttributedModelServices.ComposeExportedValue<CrmServiceEndpoint>(this.container, "CrmServiceEndpoint", serviceEndPoint);
      AttributedModelServices.ComposeExportedValue<CrmServiceClient>(this.container, "serviceClient", exportedValue2);
      AttributedModelServices.ComposeExportedValue<List<Project>>(this.container, "projects", exportedValue1);
      AttributedModelServices.ComposeExportedValue<IEnumerable<EntityMetadata>>(this.container, "entities", exportedValue3);
      AttributedModelServices.ComposeExportedValue<EntityMetadata>(this.container, "primaryEntity", exportedValue4);
      AttributedModelServices.ComposeExportedValue<string>(this.container, "className", (string) null);
      AttributedModelServices.ComposeExportedValue<string>(this.container, "processType", (string) null);
      AttributedModelServices.ComposeExportedValue<string>(this.container, "actionName", (string) null);
      this.ComposeParts();
      if (this.PluginView != null)
      {
        (this.PluginView as Window).Owner = System.Windows.Application.Current.MainWindow;
        (this.PluginView as Window).ShowDialog();
        CrmPluginStep crmPluginStep = this.PluginView.RegisteredComponent() as CrmPluginStep;
        if (crmPluginStep != null)
        {
          this._isNotRefresh = true;
          ExtensionMethods.SelectNode(this.AssembliesTree, (ICrmTreeNode) crmPluginStep);
        }
      }
      this.container = (CompositionContainer) null;
      this.catalog = (AggregateCatalog) null;
    }

    private void LoadServiceBusConfigUserControl(CrmServiceEndpoint serviceEndpoint)
    {
      this.catalog = new AggregateCatalog();
      this.catalog.Catalogs.Add((ComposablePartCatalog) new DirectoryCatalog(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "ServiceEndpointRegistration*.dll"));
      this.container = new CompositionContainer((ComposablePartCatalog) this.catalog, new ExportProvider[0]);
      AttributedModelServices.ComposeExportedValue<CrmOrganization>(this.container, "CrmOrganization", this._org);
      AttributedModelServices.ComposeExportedValue<CrmServiceEndpoint>(this.container, "CrmServiceEndpoint", serviceEndpoint);
      this.ComposeParts();
      if (this.PluginView != null)
      {
        (this.PluginView as Window).Owner = System.Windows.Application.Current.MainWindow;
        (this.PluginView as Window).ShowDialog();
        CrmServiceEndpoint crmServiceEndpoint = this.PluginView.RegisteredComponent() as CrmServiceEndpoint;
        if (crmServiceEndpoint != null)
        {
          this._isNotRefresh = true;
          this.LoadNodes();
          ExtensionMethods.SelectNode(this.AssembliesTree, (ICrmTreeNode) crmServiceEndpoint);
        }
      }
      this.container = (CompositionContainer) null;
      this.catalog = (AggregateCatalog) null;
    }

    private void LoadImageRegistrationUserControl(CrmPluginImage crmPluginImage, Guid nodeId)
    {
      this.catalog = new AggregateCatalog();
      this.catalog.Catalogs.Add((ComposablePartCatalog) new DirectoryCatalog(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "ImageRegistration.dll"));
      this.container = new CompositionContainer((ComposablePartCatalog) this.catalog, new ExportProvider[0]);
      AttributedModelServices.ComposeExportedValue<CrmOrganization>(this.container, "CrmOrganization", this._org);
      AttributedModelServices.ComposeExportedValue<ObservableCollection<ICrmTreeNode>>(this.container, "RootNodes", this.AssembliesTree);
      AttributedModelServices.ComposeExportedValue<CrmPluginImage>(this.container, "CrmPluginImage", crmPluginImage);
      AttributedModelServices.ComposeExportedValue<Guid>(this.container, "SelectedNode", nodeId);
      this.ComposeParts();
      if (this.PluginView != null)
      {
        (this.PluginView as Window).Owner = System.Windows.Application.Current.MainWindow;
        (this.PluginView as Window).ShowDialog();
        CrmPluginImage crmPluginImage1 = this.PluginView.RegisteredComponent() as CrmPluginImage;
        if (crmPluginImage1 != null)
        {
          this._isNotRefresh = true;
          ExtensionMethods.SelectNode(this.AssembliesTree, (ICrmTreeNode) crmPluginImage1);
        }
      }
      this.container = (CompositionContainer) null;
      this.catalog = (AggregateCatalog) null;
    }

    private void LoadDebugPluginUserControl()
    {
      CrmServiceClient serviceConnection = this._org.CrmServiceConnection;
      if (this.debugModuleCatalog == null)
      {
        this.debugModuleCatalog = new AggregateCatalog();
        this.debugModuleCatalog.Catalogs.Add((ComposablePartCatalog) new DirectoryCatalog(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "DebugPlugin*.dll"));
      }
      this.container = new CompositionContainer((ComposablePartCatalog) this.debugModuleCatalog, new ExportProvider[0]);
      AttributedModelServices.ComposeExportedValue<CrmServiceClient>(this.container, "serviceClient", serviceConnection);
      AttributedModelServices.ComposeExportedValue<string>(this.container, "assemblyPath", string.Empty);
      AttributedModelServices.ComposeExportedValue<string>(this.container, "sdkBinPath", string.Empty);
      AttributedModelServices.ComposeExportedValue<bool>(this.container, "isDebugExternalyHosted", false);
      this.ComposeParts();
      if (this.DebugView == null)
        return;
      (this.DebugView as Window).Owner = System.Windows.Application.Current.MainWindow;
      (this.DebugView as Window).ShowDialog();
    }

    private void ComposeParts()
    {
      try
      {
        if (this.container == null)
          return;
        AttributedModelServices.ComposeParts(this.container, (object) this);
      }
      catch (ReflectionTypeLoadException ex)
      {
        if (ex.LoaderExceptions == null || ex.LoaderExceptions.Length <= 0)
          return;
        StringBuilder stringBuilder = new StringBuilder();
        foreach (Exception exception in ex.LoaderExceptions)
          stringBuilder.AppendLine(exception.Message);
        ErrorMessageViewModel.ShowErrorMessageBox((Window) null, " ReflectionTypeLoadException : " + ex.Message, "Error", string.Format((IFormatProvider) CultureInfo.InvariantCulture, "ImportCustomizationLoader -  Exception List:\n{0}", new object[1]
        {
          (object) stringBuilder.ToString()
        }), (System.Windows.Controls.UserControl) null);
      }
      catch (Exception ex)
      {
        ErrorMessageViewModel.ShowErrorMessageBox((Window) null, "An error occurred while trying to compose MEF component ", "Error", ex, (System.Windows.Controls.UserControl) null);
      }
    }

    public void OnImportsSatisfied()
    {
      IEnumerable<Lazy<IPluginRegistrationView, IPluginMetadata>> plugins = this.Plugins;
    }
  }
}
