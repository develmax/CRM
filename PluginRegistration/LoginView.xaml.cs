// Decompiled with JetBrains decompiler
// Type: Microsoft.Crm.Tools.PluginRegistration.LoginView
// Assembly: PluginRegistration, Version=7.1.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: BDC54875-46F6-4E1E-9048-C47CF582701C
// Assembly location: C:\Distribs\CRM\sdk\SDK\Tools\PluginRegistration\PluginRegistration.exe

using Microsoft.Crm.Tools.Libraries;
using Microsoft.Xrm.Tooling.CrmConnectControl;
using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace Microsoft.Crm.Tools.PluginRegistration
{
  public partial class LoginView : Window, IComponentConnector
  {
    private CrmOrganization _crmOrganization;
    private bool _organisationRetreived;
    /*internal LoginView LoginWindow;
    internal Grid LayoutRoot;
    internal Button CloseButton;
    internal CrmServerLoginControl CrmLoginCtrl;
    private bool _contentLoaded;*/

    public LoginView()
    {
      this.InitializeComponent();
      this._crmOrganization = new CrmOrganization();
    }

    private void CrmLoginCtrl_ConnectionStatusEvent(object sender, ConnectStatusEventArgs e)
    {
      bool flag = false;
      Trace.WriteLine(string.Format("Status:{0}", (object) e.Status));
      LoginViewModel loginViewModel = this.DataContext as LoginViewModel;
      if (loginViewModel != null && e.ConnectSucceeded && !loginViewModel.BIsConnectedComplete)
        loginViewModel.ProcessSuccess();
      if (e.Status == "Connection to CRM Complete" && loginViewModel != null)
        loginViewModel.GetSelectedOrgDetails();
      this._organisationRetreived = e.Status == "Retrieving Organizations from CRM" && !flag;
    }

    private void CrmLoginCtrl_ConnectionCheckBegining(object sender, EventArgs e)
    {
      LoginViewModel loginViewModel = this.DataContext as LoginViewModel;
      if (loginViewModel == null)
        return;
      loginViewModel.BIsConnectedComplete = false;
    }

    private void CrmLoginCtrl_OnUserCancelClicked(object sender, EventArgs e)
    {
      this.Close();
    }

    /*[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
    [DebuggerNonUserCode]
    public void InitializeComponent()
    {
      if (this._contentLoaded)
        return;
      this._contentLoaded = true;
      Application.LoadComponent((object) this, new Uri("/PluginRegistration;component/views/loginview.xaml", UriKind.Relative));
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    [GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
    [DebuggerNonUserCode]
    void IComponentConnector.Connect(int connectionId, object target)
    {
      switch (connectionId)
      {
        case 1:
          this.LoginWindow = (LoginView) target;
          break;
        case 2:
          this.LayoutRoot = (Grid) target;
          break;
        case 3:
          this.CloseButton = (Button) target;
          break;
        case 4:
          this.CrmLoginCtrl = (CrmServerLoginControl) target;
          this.CrmLoginCtrl.UserCancelClicked += new EventHandler(this.CrmLoginCtrl_OnUserCancelClicked);
          this.CrmLoginCtrl.ConnectionCheckBegining += new EventHandler(this.CrmLoginCtrl_ConnectionCheckBegining);
          this.CrmLoginCtrl.ConnectionStatusEvent += new EventHandler<ConnectStatusEventArgs>(this.CrmLoginCtrl_ConnectionStatusEvent);
          break;
        default:
          this._contentLoaded = true;
          break;
      }
    }*/
  }
}
