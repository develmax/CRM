// Decompiled with JetBrains decompiler
// Type: Microsoft.Crm.Tools.PluginRegistration.ProfilerSettingsView
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
  public partial class ProfilerSettingsView : Window, IComponentConnector
  {
    /*internal Button CloseButton;
    internal Button btnrefresh;
    internal ListBox lbox;
    internal RadioButton rbexception;
    internal RadioButton rbpersistent;
    internal TextBlock lblpersistenkey;
    internal TextBox txtpersistentkey;
    internal CheckBox cbmaxnuberexec;
    internal TextBlock maxlbl;
    internal TextBox txtmax;
    private bool _contentLoaded;*/

    public ProfilerSettingsView()
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
      Application.LoadComponent((object) this, new Uri("/PluginRegistration;component/views/profilersettingsview.xaml", UriKind.Relative));
    }

    [DebuggerNonUserCode]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
    void IComponentConnector.Connect(int connectionId, object target)
    {
      switch (connectionId)
      {
        case 1:
          this.CloseButton = (Button) target;
          break;
        case 2:
          this.btnrefresh = (Button) target;
          break;
        case 3:
          this.lbox = (ListBox) target;
          break;
        case 4:
          this.rbexception = (RadioButton) target;
          break;
        case 5:
          this.rbpersistent = (RadioButton) target;
          break;
        case 6:
          this.lblpersistenkey = (TextBlock) target;
          break;
        case 7:
          this.txtpersistentkey = (TextBox) target;
          break;
        case 8:
          this.cbmaxnuberexec = (CheckBox) target;
          break;
        case 9:
          this.maxlbl = (TextBlock) target;
          break;
        case 10:
          this.txtmax = (TextBox) target;
          break;
        default:
          this._contentLoaded = true;
          break;
      }
    }*/
  }
}
