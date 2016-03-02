// Decompiled with JetBrains decompiler
// Type: Microsoft.Crm.Tools.PluginRegistration.OrganizationsView
// Assembly: PluginRegistration, Version=7.1.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: BDC54875-46F6-4E1E-9048-C47CF582701C
// Assembly location: C:\Distribs\CRM\sdk\SDK\Tools\PluginRegistration\PluginRegistration.exe

using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace Microsoft.Crm.Tools.PluginRegistration
{
  public partial class OrganizationsView : UserControl, IComponentConnector
  {
    /*internal Grid LayoutRoot;
    private bool _contentLoaded;*/

    public OrganizationsView()
    {
      this.InitializeComponent();
    }

    /*[DebuggerNonUserCode]
    [GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
    public void InitializeComponent()
    {
      if (this._contentLoaded)
        return;
      this._contentLoaded = true;
      Application.LoadComponent((object) this, new Uri("/PluginRegistration;component/views/organizationsview.xaml", UriKind.Relative));
    }

    [DebuggerNonUserCode]
    [GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
    [EditorBrowsable(EditorBrowsableState.Never)]
    void IComponentConnector.Connect(int connectionId, object target)
    {
      if (connectionId == 1)
        this.LayoutRoot = (Grid) target;
      else
        this._contentLoaded = true;
    }*/
  }
}
