// Decompiled with JetBrains decompiler
// Type: Microsoft.Crm.Tools.PluginRegistration.MainViewModel
// Assembly: PluginRegistration, Version=7.1.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: BDC54875-46F6-4E1E-9048-C47CF582701C
// Assembly location: C:\Distribs\CRM\sdk\SDK\Tools\PluginRegistration\PluginRegistration.exe

using Microsoft.Crm.Tools.Libraries;
using Microsoft.Crm.Tools.PluginRegistration.CommonControls;
using Microsoft.Xrm.Tooling.Connector;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace Microsoft.Crm.Tools.PluginRegistration
{
  public class MainViewModel : BaseNotifyable
  {
    private static volatile List<CrmMessage> _messageList = new List<CrmMessage>();
    private bool _isMainViewEnabled = true;
    private Visibility _btnVisibleStatus = Visibility.Collapsed;
    private System.Drawing.Image _imgCreateConnection = CrmResources.LoadImage(CrmImageType.CreateNewConnection);
    private System.Drawing.Image _imgReloadOrganizations = CrmResources.LoadImage(CrmImageType.ReloadOrganizations);
    private System.Drawing.Image _imgReplayPlugin = CrmResources.LoadImage(CrmImageType.ReplayPluginExecution);
    private System.Drawing.Image _imgViewPlugin = CrmResources.LoadImage(CrmImageType.ViewPluginProfile);
    private MainView _mainView;
    public bool _isReload;
    public Dictionary<Guid, string> _viewTypeStatus;
    private Dictionary<Guid, TabItem> _orgTabList;
    private bool? _isOk;
    private double _progressStatusVal;
    private string _progressStatusTxt;
    private string _statusLabel;
    private bool _isToolbarEnabled;
    private string _organizationStatus;
    private string _title;
    private ProgressIndicator _progIndicator;
    private Visibility _organizationStatusVisible;
    private RelayCommand _cmdConnectionNew;
    private RelayCommand _cmdProfilerReplay;
    private RelayCommand _cmdConnectionRefresh;
    private RelayCommand _cmdPluginProfile;
    private RelayCommand _cmdHelp;
    private RelayCommand _cmdSettings;
    private OrganizationsViewModel _organizationsTab;
    private ObservableCollection<TabItem> _organizationsList;
    private bool _isEnableStatus;
    private Visibility _visibilityStatus;
    private RelayCommand _clickOk;
    private AggregateCatalog debugModuleCatalog;
    private OrganizationControlViewModel _selectedOrganization;

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

    public RelayCommand Click_OK
    {
      get
      {
        return this._clickOk ?? (this._clickOk = new RelayCommand((Action<object>) (s => this.ProgOK_Clicked())));
      }
    }

    public Dictionary<Guid, TabItem> OrgTabList
    {
      get
      {
        return this._orgTabList;
      }
      set
      {
        this._orgTabList = value;
      }
    }

    public bool IsMainViewEnabled
    {
      get
      {
        return this._isMainViewEnabled;
      }
      set
      {
        this.SetProperty<bool>(ref this._isMainViewEnabled, value, "IsMainViewEnabled");
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

    public double ProgressStatusVal
    {
      get
      {
        return this._progressStatusVal;
      }
      set
      {
        this.SetProperty<double>(ref this._progressStatusVal, value, "ProgressStatusVal");
      }
    }

    public string Title
    {
      get
      {
        return this._title;
      }
      set
      {
        this.SetProperty<string>(ref this._title, value, "Title");
      }
    }

    public ProgressIndicator ProgressIndicator
    {
      get
      {
        return this._progIndicator;
      }
      set
      {
        this.SetProperty<ProgressIndicator>(ref this._progIndicator, value, "ProgressIndicator");
      }
    }

    public Visibility OrganizationStatusVisible
    {
      get
      {
        return this._organizationStatusVisible;
      }
      set
      {
        this.SetProperty<Visibility>(ref this._organizationStatusVisible, value, "OrganizationStatusVisible");
      }
    }

    public string OrganizationStatus
    {
      get
      {
        return this._organizationStatus;
      }
      set
      {
        this.SetProperty<string>(ref this._organizationStatus, value, "OrganizationStatus");
      }
    }

    public bool IsToolbarEnabled
    {
      get
      {
        return this._isToolbarEnabled;
      }
      set
      {
        this.SetProperty<bool>(ref this._isToolbarEnabled, value, "IsToolbarEnabled");
      }
    }

    public string StatusLabel
    {
      get
      {
        return this._statusLabel;
      }
      private set
      {
        this.SetProperty<string>(ref this._statusLabel, value, "StatusLabel");
      }
    }

    public ObservableCollection<TabItem> OrganizationsList
    {
      get
      {
        return this._organizationsList;
      }
      set
      {
        this.SetProperty<ObservableCollection<TabItem>>(ref this._organizationsList, value, "OrganizationsList");
      }
    }

    public CrmOrganization CrmOrganization { get; set; }

    public OrganizationsViewModel OrganizationsTab
    {
      get
      {
        return this._organizationsTab;
      }
      set
      {
        this.SetProperty<OrganizationsViewModel>(ref this._organizationsTab, value, "OrganizationsTab");
      }
    }

    public RelayCommand CmdConnectionNew
    {
      get
      {
        return this._cmdConnectionNew ?? (this._cmdConnectionNew = new RelayCommand((Action<object>) (s => this.toolConnectionNew_Click())));
      }
    }

    public RelayCommand CmdProfilerReplay
    {
      get
      {
        return this._cmdProfilerReplay ?? (this._cmdProfilerReplay = new RelayCommand((Action<object>) (s => this.toolProfilerReplay_Click())));
      }
    }

    public RelayCommand CmdPluginProfile
    {
      get
      {
        return this._cmdPluginProfile ?? (this._cmdPluginProfile = new RelayCommand((Action<object>) (s => this.toolPluginProfile_Click())));
      }
    }

    public RelayCommand CmdConnectionRefresh
    {
      get
      {
        return this._cmdConnectionRefresh ?? (this._cmdConnectionRefresh = new RelayCommand((Action<object>) (s => this.toolReloadOrganizations_Click())));
      }
    }

    public RelayCommand CmdHelp
    {
      get
      {
        return this._cmdHelp ?? (this._cmdHelp = new RelayCommand((Action<object>) (s => this.Help_Clicked())));
      }
    }

    public RelayCommand CmdSettings
    {
      get
      {
        return this._cmdSettings ?? (this._cmdSettings = new RelayCommand((Action<object>) (s => this.Settings_Clicked())));
      }
    }

    public System.Drawing.Image ImgCreateConnection
    {
      get
      {
        return this._imgCreateConnection;
      }
    }

    public System.Drawing.Image ImgReloadOrganizations
    {
      get
      {
        return this._imgReloadOrganizations;
      }
    }

    public System.Drawing.Image ImgReplayPlugin
    {
      get
      {
        return this._imgReplayPlugin;
      }
    }

    public System.Drawing.Image ImgViewPlugin
    {
      get
      {
        return this._imgViewPlugin;
      }
    }

    public System.Drawing.Image ImgHelp
    {
      get
      {
        return CrmResources.LoadImage(CrmImageType.MainHelp);
      }
    }

    public System.Drawing.Image ImgSettings
    {
      get
      {
        return CrmResources.LoadImage(CrmImageType.MainSettings);
      }
    }

    public System.Drawing.Image ImgCRMLogo
    {
      get
      {
        return CrmResources.LoadImage(CrmImageType.CRMLogo);
      }
    }

    public OrganizationControlViewModel SelectedOrganization
    {
      get
      {
        return this._selectedOrganization;
      }
      set
      {
        this.SetProperty<OrganizationControlViewModel>(ref this._selectedOrganization, value, "SelectedOrganization");
      }
    }

    [Import(typeof (IDebuggerView), AllowDefault = true, AllowRecomposition = false)]
    public IDebuggerView DebugView { get; set; }

    public MainViewModel(MainView mainView)
    {
      this.Title = "Plugin Registration Tool";
      this.IsToolbarEnabled = true;
      if (this._viewTypeStatus == null)
        this._viewTypeStatus = new Dictionary<Guid, string>();
      mainView.SourceInitialized += new EventHandler(MaximizeHelper.OnMaximize);
      this._mainView = mainView;
    }

    private void ProgOK_Clicked()
    {
    }

    internal CrmEntityDictionary<CrmMessage> LoadMessages(CrmOrganization org)
    {
      lock (MainViewModel._messageList)
      {
        if (MainViewModel._messageList.Count == 0)
          MainViewModel._messageList = OrganizationHelper.LoadMessages(org, this.ProgressIndicator);
        return new CrmEntityDictionary<CrmMessage>(Enumerable.ToDictionary<CrmMessage, Guid>(Enumerable.Select<CrmMessage, CrmMessage>((IEnumerable<CrmMessage>) MainViewModel._messageList, (Func<CrmMessage, CrmMessage>) (msg => new CrmMessage((CrmOrganization) null, msg.MessageId, msg.Name, msg.SupportsFilteredAttributes, msg.CustomizationLevel, msg.CreatedOn, msg.ModifiedOn, msg.ImageMessagePropertyNames))), (Func<CrmMessage, Guid>) (newMessage => newMessage.MessageId)));
      }
    }

    internal void EnableToolBar(bool enabled)
    {
      this.IsToolbarEnabled = enabled;
    }

    internal void UpdateCurrentOrganization(CrmOrganization org)
    {
      if (org == null)
      {
        this.OrganizationStatusVisible = Visibility.Hidden;
      }
      else
      {
        this.OrganizationStatusVisible = Visibility.Visible;
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.AppendFormat((IFormatProvider) CultureInfo.InvariantCulture, "Organization: {0}", new object[1]
        {
          (object) org.OrganizationFriendlyName
        });
        if (org.LoggedOnUser != null)
          stringBuilder.AppendFormat((IFormatProvider) CultureInfo.InvariantCulture, " / User: {0} ({1})", new object[2]
          {
            (object) org.LoggedOnUser.Name,
            (object) org.LoggedOnUser.DomainName
          });
        if ((Version) null != org.ServerBuild)
          stringBuilder.AppendFormat((IFormatProvider) CultureInfo.InvariantCulture, " / Build: {0}", new object[1]
          {
            (object) org.ServerBuild
          });
        this.OrganizationStatus = stringBuilder.ToString().Replace("&", "&&");
      }
    }

    private void CreateConnections()
    {
      try
      {
        LoginView loginView1 = new LoginView();
        loginView1.DataContext = (object) new LoginViewModel(this);
        loginView1.Owner = Application.Current.MainWindow;
        LoginView loginView2 = loginView1;
        this._isOk = loginView2.ShowDialog();
        if (!this._isOk.HasValue || !this._isOk.Value)
          return;
        if (this.OrganizationsTab == null)
        {
          this.OrganizationsTab = new OrganizationsViewModel(this.CrmOrganization, this, true);
          this.OrgTabList = new Dictionary<Guid, TabItem>();
        }
        else
        {
          this.OrganizationsTab.CrmOrganization = this.CrmOrganization;
          this.OrganizationsTab.MainViewModel = this;
        }
        if (this.CrmOrganization == null)
          return;
        TabItem tabItem = new TabItem();
        if (this.OrgTabList.TryGetValue(this.CrmOrganization.OrganizationId, out tabItem))
        {
          if ((tabItem.Tag as CrmOrganization).LoggedOnUser.UserId != (loginView2.DataContext as LoginViewModel)._userID)
          {
            int num = (int) MessageBox.Show(string.Format("{0} is already using this {1}", (object) (tabItem.Tag as CrmOrganization).LoggedOnUser, (object) this.CrmOrganization.OrganizationFriendlyName));
          }
          else
            this.OrganizationsTab.SelectedOrganization = this.OrganizationsTab.GetTab(this.CrmOrganization.OrganizationId);
        }
        else
          Helper.OpenConnection(this.CrmOrganization, this.LoadMessages(this.CrmOrganization), this);
      }
      catch (Exception ex)
      {
        ErrorMessageViewModel.ShowErrorMessageBox((Window) null, "Unable to connect to the CRM Organization", "Connection Error", ex, (UserControl) null);
      }
    }

    internal void CreateTab(string tempViewstatus, CrmOrganization org = null, TabItem currentOrganization = null, bool isReload = false)
    {
      bool flag = false;
      double detailsTabHeight = 0.0;
      if (this.OrganizationsTab.ListTabOrganizations == null)
        this.OrganizationsTab.ListTabOrganizations = new ObservableCollection<TabItem>();
      else if (!this._isReload)
      {
        IEnumerable<TabItem> source = Enumerable.Where<TabItem>((IEnumerable<TabItem>) this.OrganizationsTab.ListTabOrganizations, (Func<TabItem, bool>) (s => s.Tag == org));
        if (source != null && Enumerable.Count<TabItem>(source) > 0)
          detailsTabHeight = ((Enumerable.First<TabItem>(source).Content as OrganizationControlView).DataContext as OrganizationControlViewModel).DetailsTabHeight.Value;
      }
      CrmOrganization crmOrganization = org ?? this.CrmOrganization;
      OrganizationControlView orgControlView = new OrganizationControlView();
      orgControlView.DataContext = (object) new OrganizationControlViewModel(tempViewstatus, crmOrganization, this, orgControlView, detailsTabHeight);
      TabItem tabItem1 = new TabItem();
      tabItem1.Header = (object) crmOrganization.OrganizationFriendlyName;
      tabItem1.IsSelected = true;
      tabItem1.Content = (object) orgControlView;
      tabItem1.Tag = (object) crmOrganization;
      TabItem tabItem = tabItem1;
      foreach (TabItem tabItem2 in (Collection<TabItem>) this.OrganizationsTab.ListTabOrganizations)
      {
        if (tabItem.Header.ToString().Trim() == tabItem2.Header.ToString().Trim())
          flag = true;
      }
      if (flag)
      {
        int index = this.OrganizationsTab.ListTabOrganizations.IndexOf(Enumerable.Single<TabItem>((IEnumerable<TabItem>) this.OrganizationsTab.ListTabOrganizations, (Func<TabItem, bool>) (tempTabItem => tabItem.Header.ToString().Trim() == tempTabItem.Header.ToString().Trim())));
        this.OrganizationsTab.ListTabOrganizations.Insert(index, tabItem);
        this.OrganizationsTab.ListTabOrganizations.RemoveAt(index + 1);
      }
      else
      {
        try
        {
          this.OrgTabList.Add(org.OrganizationId, tabItem);
          this.OrganizationsTab.ListTabOrganizations.Add(tabItem);
          this.OrganizationsTab.SelectedOrganization = tabItem;
        }
        catch (ArgumentException ex)
        {
          ErrorMessageViewModel.ShowErrorMessageBox((Window) null, ex.Message, "Organization already exists", (Exception) ex, (UserControl) null);
        }
      }
      if (!isReload)
        return;
      foreach (TabItem tabItem2 in (Collection<TabItem>) this.OrganizationsTab.ListTabOrganizations)
      {
        if (tabItem2 == currentOrganization)
        {
          this.OrganizationsTab.SelectedOrganization = tabItem2;
          break;
        }
      }
      Enumerable.ToList<TabItem>((IEnumerable<TabItem>) this.OrganizationsTab.ListTabOrganizations).Find((Predicate<TabItem>) (x => (x.Tag as CrmOrganization).OrganizationId == (currentOrganization.Tag as CrmOrganization).OrganizationId)).IsSelected = true;
    }

    private void ReloadConnection()
    {
      this._isReload = true;
      if (this.OrganizationsTab == null)
      {
        int num1 = (int) MessageBox.Show("No Organizations loaded to reload\nClick on Create Connection to load Organization");
      }
      else if (this.OrganizationsTab.ListTabOrganizations.Count == 0)
      {
        int num2 = (int) MessageBox.Show("No Organizations loaded to reload\nClick on Create Connection to load Organization");
      }
      else
        Helper.OpenConnectionForReload(this, this.OrganizationsTab.SelectedOrganization, true);
    }

    private void toolConnectionNew_Click()
    {
      this.CreateConnections();
    }

    private void toolReloadOrganizations_Click()
    {
      this.ReloadConnection();
    }

    private void toolProfilerReplay_Click()
    {
      this.LoadDebugPluginUserControl();
    }

    private void toolPluginProfile_Click()
    {
      PluginProfileView pluginProfileView = new PluginProfileView();
      pluginProfileView.DataContext = (object) new PluginProfileViewModel(pluginProfileView);
      pluginProfileView.Owner = Application.Current.MainWindow;
      pluginProfileView.ShowDialog();
    }

    private void Settings_Clicked()
    {
    }

    private void Help_Clicked()
    {
      AboutView aboutView = new AboutView();
      aboutView.DataContext = (object) new AboutViewModel();
      aboutView.Owner = Application.Current.MainWindow;
      aboutView.ShowDialog();
    }

    private void LoadDebugPluginUserControl()
    {
      if (this.debugModuleCatalog == null)
      {
        this.debugModuleCatalog = new AggregateCatalog();
        this.debugModuleCatalog.Catalogs.Add((ComposablePartCatalog) new DirectoryCatalog(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "DebugPlugin*.dll"));
      }
      CompositionContainer container = new CompositionContainer((ComposablePartCatalog) this.debugModuleCatalog, new ExportProvider[0]);
      if (App.OrganizationConnections != null && App.OrganizationConnections.Count > 0)
      {
        if (App.OrganizationConnections.ContainsKey(App.ActiveOrganizationId))
          AttributedModelServices.ComposeExportedValue<CrmServiceClient>(container, "serviceClient", App.OrganizationConnections[App.ActiveOrganizationId].CrmSvc);
        else
          AttributedModelServices.ComposeExportedValue<CrmServiceClient>(container, "serviceClient", (CrmServiceClient) null);
      }
      else
        AttributedModelServices.ComposeExportedValue<CrmServiceClient>(container, "serviceClient", (CrmServiceClient) null);
      AttributedModelServices.ComposeExportedValue<string>(container, "assemblyPath", string.Empty);
      AttributedModelServices.ComposeExportedValue<string>(container, "sdkBinPath", string.Empty);
      AttributedModelServices.ComposeExportedValue<bool>(container, "isDebugExternalyHosted", false);
      this.ComposeParts(container);
      if (this.DebugView == null)
        return;
      (this.DebugView as Window).Owner = Application.Current.MainWindow;
      (this.DebugView as Window).ShowDialog();
    }

    private void ComposeParts(CompositionContainer container)
    {
      try
      {
        if (container == null)
          return;
        AttributedModelServices.ComposeParts(container, (object) this);
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
        }), (UserControl) null);
      }
      catch (Exception ex)
      {
        ErrorMessageViewModel.ShowErrorMessageBox((Window) null, "An error occurred while trying to compose MEF component ", "Error", ex, (UserControl) null);
      }
    }
  }
}
