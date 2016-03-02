// Decompiled with JetBrains decompiler
// Type: Microsoft.Crm.Tools.PluginRegistration.App
// Assembly: PluginRegistration, Version=7.1.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: BDC54875-46F6-4E1E-9048-C47CF582701C
// Assembly location: C:\Distribs\CRM\sdk\SDK\Tools\PluginRegistration\PluginRegistration.exe

using Microsoft.Xrm.Tooling.CrmConnectControl;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;

namespace Microsoft.Crm.Tools.PluginRegistration
{
  public partial class App : Application
  {
    //private bool _contentLoaded;

    public static Dictionary<Guid, CrmConnectionManager> OrganizationConnections { get; set; }

    public static Guid ActiveOrganizationId { get; set; }

    protected override void OnStartup(StartupEventArgs e)
    {
      base.OnStartup(e);
    }

    /*[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
    [DebuggerNonUserCode]
    public void InitializeComponent()
    {
      if (this._contentLoaded)
        return;
      this._contentLoaded = true;
      this.StartupUri = new Uri("Views/MainView.xaml", UriKind.Relative);
      Application.LoadComponent((object) this, new Uri("/PluginRegistration;component/app.xaml", UriKind.Relative));
    }

    [DebuggerNonUserCode]
    [GeneratedCode("PresentationBuildTasks", "4.0.0.0")]*/
    [STAThread]
    public static void Main()
    {
      App app = new App();
      app.InitializeComponent();
      app.Run();
    }
  }
}
