// Decompiled with JetBrains decompiler
// Type: Microsoft.Crm.Tools.PluginRegistration.PluginProfileView
// Assembly: PluginRegistration, Version=7.1.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: BDC54875-46F6-4E1E-9048-C47CF582701C
// Assembly location: C:\Distribs\CRM\sdk\SDK\Tools\PluginRegistration\PluginRegistration.exe

using Microsoft.Crm.Tools.PluginRegistration.CommonControls;
using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace Microsoft.Crm.Tools.PluginRegistration
{
  public partial class PluginProfileView : Window, IComponentConnector
  {
    /*internal Grid LayoutRoot;
    internal Button CloseButton;
    internal FileBrowserView fileBrowser;
    internal Button btnView;
    internal Button btnSave;
    internal Button btnClose;
    private bool _contentLoaded;*/

    public PluginProfileView()
    {
      this.InitializeComponent();
      try
      {
        if (App.OrganizationConnections != null && App.OrganizationConnections.Count > 0 && App.OrganizationConnections.ContainsKey(App.ActiveOrganizationId))
        {
          this.fileBrowser.CrmClient = App.OrganizationConnections[App.ActiveOrganizationId].CrmSvc;
          this.fileBrowser.ShowCrmBrowserOption = true;
        }
        else
          this.fileBrowser.ShowCrmBrowserOption = false;
      }
      catch
      {
        this.fileBrowser.ShowCrmBrowserOption = false;
      }
    }

    private void fileBrowser_PathChanged_1(object sender, EventArgs e)
    {
      PluginProfileViewModel profileViewModel = this.DataContext as PluginProfileViewModel;
      if (profileViewModel == null)
        return;
      profileViewModel.LogPathControl_PathChanged(sender);
    }

    private void Window_Closed(object sender, EventArgs e)
    {
      if (this.fileBrowser == null)
        return;
      this.fileBrowser = (FileBrowserView) null;
    }

    /*[DebuggerNonUserCode]
    [GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
    public void InitializeComponent()
    {
      if (this._contentLoaded)
        return;
      this._contentLoaded = true;
      Application.LoadComponent((object) this, new Uri("/PluginRegistration;component/views/pluginprofileview.xaml", UriKind.Relative));
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    [DebuggerNonUserCode]
    [GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
    void IComponentConnector.Connect(int connectionId, object target)
    {
      switch (connectionId)
      {
        case 1:
          ((Window) target).Closed += new EventHandler(this.Window_Closed);
          break;
        case 2:
          this.LayoutRoot = (Grid) target;
          break;
        case 3:
          this.CloseButton = (Button) target;
          break;
        case 4:
          this.fileBrowser = (FileBrowserView) target;
          this.fileBrowser.PathChanged += new EventHandler<EventArgs>(this.fileBrowser_PathChanged_1);
          break;
        case 5:
          this.btnView = (Button) target;
          break;
        case 6:
          this.btnSave = (Button) target;
          break;
        case 7:
          this.btnClose = (Button) target;
          break;
        default:
          this._contentLoaded = true;
          break;
      }
    }*/
  }
}
