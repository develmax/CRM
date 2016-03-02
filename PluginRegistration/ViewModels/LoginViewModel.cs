// Decompiled with JetBrains decompiler
// Type: Microsoft.Crm.Tools.PluginRegistration.LoginViewModel
// Assembly: PluginRegistration, Version=7.1.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: BDC54875-46F6-4E1E-9048-C47CF582701C
// Assembly location: C:\Distribs\CRM\sdk\SDK\Tools\PluginRegistration\PluginRegistration.exe

using Microsoft.Crm.Sdk.Messages;
using Microsoft.Crm.Tools.Libraries;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Discovery;
using Microsoft.Xrm.Tooling.CrmConnectControl;
using Microsoft.Xrm.Tooling.CrmConnectControl.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace Microsoft.Crm.Tools.PluginRegistration
{
  public class LoginViewModel : BaseNotifyable
  {
    internal bool BIsConnectedComplete;
    private CrmConnectionManager _mgr;
    private LoginView _loginWindow;
    private CrmServerLoginControl _crmLoginCtrl;
    private CrmOrgList _crmOrgsList;
    private MainViewModel _mainViewModel;
    private Guid _retrievedOrganizationID;
    public Guid _userID;

    public CrmOrgList CrmOrgsList
    {
      get
      {
        return this._crmOrgsList;
      }
      set
      {
        this._crmOrgsList = value;
      }
    }

    public LoginViewModel(MainViewModel mainViewModel)
    {
      this._mainViewModel = mainViewModel;
    }

    public void Login_Load(object sender, RoutedEventArgs e)
    {
      this._loginWindow = sender as LoginView;
      if (this._loginWindow != null)
        this._crmLoginCtrl = this._loginWindow.FindName("CrmLoginCtrl") as CrmServerLoginControl;
        Dispatcher.CurrentDispatcher.Invoke(
            DispatcherPriority.Normal,  
            new Action(() => { this._crmLoginCtrl.IsEnabled = true; }));

      this.BIsConnectedComplete = false;
      this._mgr = new CrmConnectionManager()
      {
        ParentControl = (UserControl) this._crmLoginCtrl,
        UseUserLocalDirectoryForConfigStore = true
      };
      if (this._crmLoginCtrl == null)
        return;
      this._crmLoginCtrl.SetGlobalStoreAccess(this._mgr);
      this._crmLoginCtrl.SetControlMode(ServerLoginConfigCtrlMode.FullLoginPanel);
    }

    internal void ProcessSuccess()
    {
      string s = ConfigurationManager.AppSettings["maxcrmconnectiontimeoutminuets"];
      if (!string.IsNullOrEmpty(s))
      {
        int result = -1;
        if (int.TryParse(s, out result))
          this._mgr.CrmSvc.OrganizationServiceProxy.Timeout = TimeSpan.FromMinutes((double) result);
      }
      this.BIsConnectedComplete = true;
      WhoAmIResponse whoAmIresponse = (WhoAmIResponse) this._mgr.CrmSvc.ExecuteCrmOrganizationRequest((OrganizationRequest) new WhoAmIRequest(), "Get Organization Id for Logged In user - Login");
      if (whoAmIresponse == null && this._mgr.CrmSvc.LastCrmException != null)
        throw this._mgr.CrmSvc.LastCrmException;
      this._retrievedOrganizationID = whoAmIresponse.OrganizationId;
      this._userID = this._mgr.CrmSvc.GetMyCrmUserId();
      if (App.OrganizationConnections == null)
        App.OrganizationConnections = new Dictionary<Guid, CrmConnectionManager>();
      if (!App.OrganizationConnections.ContainsKey(this._retrievedOrganizationID))
        App.OrganizationConnections.Add(this._retrievedOrganizationID, this._mgr);
      try
      {
        this._loginWindow.DialogResult = new bool?(true);
      }
      catch (InvalidOperationException)
      {
      }
      finally
      {
        this._loginWindow.Close();
      }
    }

    public void GetSelectedOrgDetails()
    {
      this._mainViewModel.CrmOrganization = new CrmOrganization()
      {
        OrganizationFriendlyName = this._mgr.ConnectedOrgFriendlyName,
        OrganizationUniqueName = this._mgr.ConnectedOrgUniqueName,
        OrganizationServiceUrl = this._mgr.CrmSvc.ConnectedOrgPublishedEndpoints[EndpointType.OrganizationService],
        WebApplicationUrl = this._mgr.CrmSvc.ConnectedOrgPublishedEndpoints[EndpointType.WebApplication],
        ServerBuild = this._mgr.CrmSvc.ConnectedOrgVersion,
        CrmServiceConnection = this._mgr.CrmSvc
      };
      WhoAmIResponse whoAmIresponse = (WhoAmIResponse) this._mgr.CrmSvc.ExecuteCrmOrganizationRequest((OrganizationRequest) new WhoAmIRequest(), "Get Organization Id for Logged in User - Selected Org");
      if (whoAmIresponse == null && this._mgr.CrmSvc.LastCrmException != null)
        throw this._mgr.CrmSvc.LastCrmException;
      this._mainViewModel.CrmOrganization.OrganizationId = whoAmIresponse.OrganizationId;
    }
  }
}
