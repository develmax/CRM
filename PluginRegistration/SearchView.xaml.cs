// Decompiled with JetBrains decompiler
// Type: Microsoft.Crm.Tools.PluginRegistration.SearchView
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
  public partial class SearchView : Window, IComponentConnector
  {
    /*internal SearchView SearchWindow;
    internal Grid LayoutRoot;
    internal Button CloseButton;
    internal TextBox TextSearch;
    internal Button btnSearch;
    private bool _contentLoaded;*/

    public SearchView()
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
      Application.LoadComponent((object) this, new Uri("/PluginRegistration;component/views/searchview.xaml", UriKind.Relative));
    }

    [GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DebuggerNonUserCode]
    void IComponentConnector.Connect(int connectionId, object target)
    {
      switch (connectionId)
      {
        case 1:
          this.SearchWindow = (SearchView) target;
          break;
        case 2:
          this.LayoutRoot = (Grid) target;
          break;
        case 3:
          this.CloseButton = (Button) target;
          break;
        case 4:
          this.TextSearch = (TextBox) target;
          break;
        case 5:
          this.btnSearch = (Button) target;
          break;
        default:
          this._contentLoaded = true;
          break;
      }
    }*/
  }
}
