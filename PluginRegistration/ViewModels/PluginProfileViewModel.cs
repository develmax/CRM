// Decompiled with JetBrains decompiler
// Type: Microsoft.Crm.Tools.PluginRegistration.PluginProfileViewModel
// Assembly: PluginRegistration, Version=7.1.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: BDC54875-46F6-4E1E-9048-C47CF582701C
// Assembly location: C:\Distribs\CRM\sdk\SDK\Tools\PluginRegistration\PluginRegistration.exe

using Microsoft.Crm.Tools.Libraries;
using Microsoft.Crm.Tools.PluginRegistration.CommonControls;
using Microsoft.Win32;
using PluginProfiler.Plugins;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace Microsoft.Crm.Tools.PluginRegistration
{
  public class PluginProfileViewModel : BaseNotifyable
  {
    private bool _saveButtonEnable;
    private bool _pageEnable;
    private bool _viewButtonEnable;
    private RelayCommand _cmdClose;
    private ProfilerPluginReport _mReport;
    private PluginProfileView _pluginProfileView;

    public bool PageEnable
    {
      get
      {
        return this._pageEnable;
      }
      set
      {
        this.SetProperty<bool>(ref this._pageEnable, value, "PageEnable");
      }
    }

    public bool ViewButtonEnable
    {
      get
      {
        return this._viewButtonEnable;
      }
      set
      {
        this.SetProperty<bool>(ref this._viewButtonEnable, value, "ViewButtonEnable");
      }
    }

    public bool SaveButtonEnable
    {
      get
      {
        return this._saveButtonEnable;
      }
      set
      {
        this.SetProperty<bool>(ref this._saveButtonEnable, value, "SaveButtonEnable");
      }
    }

    public RelayCommand CmdClose
    {
      get
      {
        if (this._cmdClose == null)
          this._cmdClose = new RelayCommand((Action<object>) (s => this.CloseButton_Click()));
        return this._cmdClose;
      }
    }

    public PluginProfileViewModel(PluginProfileView pluginProfileView)
    {
      this._pluginProfileView = pluginProfileView;
    }

    public void ViewButton_Click(object sender, RoutedEventArgs e)
    {
      if (!this.ParseReport(false, sender))
      {
        this.EnableDisableButtons(sender);
      }
      else
      {
        try
        {
          string fileName = Path.Combine(Path.GetTempPath(), Path.GetFileNameWithoutExtension(Path.GetRandomFileName())) + ".xml";
          this.SaveFormattedReport(fileName, sender);
          using (Process process = new Process())
          {
            process.StartInfo = new ProcessStartInfo()
            {
              FileName = fileName,
              UseShellExecute = true
            };
            this.PageEnable = false;
            process.Start();
            process.WaitForExit();
          }
        }
        catch (Exception ex)
        {
          ErrorMessageViewModel.ShowErrorMessageBox((Window) this._pluginProfileView, "Unable to save the Plug-in Profile.", "Profile", ex, (UserControl) null);
        }
        finally
        {
          this.PageEnable = true;
        }
      }
    }

    public void SaveButton_Click(object sender, RoutedEventArgs e)
    {
      if (!this.ParseReport(false, sender))
      {
        this.EnableDisableButtons(sender);
      }
      else
      {
        try
        {
          SaveFileDialog saveFileDialog = new SaveFileDialog();
          saveFileDialog.Filter = "XML Files (*.xml)|*.xml|All files|*.*";
          bool? nullable = saveFileDialog.ShowDialog();
          if (!nullable.HasValue || !nullable.Value)
            return;
          this.SaveFormattedReport(saveFileDialog.FileName, sender);
        }
        catch (Exception ex)
        {
          ErrorMessageViewModel.ShowErrorMessageBox((Window) this._pluginProfileView, "Unable to save the Plug-in Profile.", "Profile", ex, (UserControl) null);
        }
      }
    }

    internal void LogPathControl_PathChanged(object sender)
    {
      if (!this.EnableDisableButtons(sender))
        return;
      this.ParseReport(true, sender);
    }

    public void PluginProfileView_Loaded(object sender, RoutedEventArgs e)
    {
      this._pluginProfileView = sender as PluginProfileView;
    }

    public void CloseButton_Click()
    {
      this._pluginProfileView.Close();
    }

    private bool EnableDisableButtons(object sender)
    {
      FrameworkElement frameworkElement = (FrameworkElement) null;
      if (sender is Button)
        frameworkElement = (FrameworkElement) PluginProfileViewModel.GetParentWindow((DependencyObject) (sender as Button));
      else if (sender is TextBox)
        frameworkElement = (FrameworkElement) PluginProfileViewModel.GetParentWindow((DependencyObject) (sender as TextBox));
      if (frameworkElement != null)
      {
        this._pluginProfileView = frameworkElement as PluginProfileView;
        FileBrowserView fileBrowserView = this._pluginProfileView.fileBrowser;
        FileBrowserViewModel browserViewModel = fileBrowserView.DataContext as FileBrowserViewModel;
        if (!fileBrowserView.IsCrmDebugProfile)
        {
          if (browserViewModel != null && browserViewModel.FileExists)
          {
            this.ViewButtonEnable = true;
            this.SaveButtonEnable = true;
            return true;
          }
        }
        else if (browserViewModel != null && browserViewModel.SelectedDebugProfile != null)
        {
          this.ViewButtonEnable = true;
          this.SaveButtonEnable = true;
          return true;
        }
        this.ViewButtonEnable = false;
        this.SaveButtonEnable = false;
      }
      return false;
    }

    private void SaveFormattedReport(string fileName, object sender)
    {
      if (!this.ParseReport(false, sender))
        return;
      string contents = this._mReport.ToString(true);
      File.WriteAllText(fileName, contents);
    }

    private bool ParseReport(bool requireReportParse, object sender)
    {
      FrameworkElement frameworkElement = (FrameworkElement) null;
      if (sender is Button)
        frameworkElement = (FrameworkElement) PluginProfileViewModel.GetParentWindow((DependencyObject) (sender as Button));
      else if (sender is TextBox)
        frameworkElement = (FrameworkElement) PluginProfileViewModel.GetParentWindow((DependencyObject) (sender as TextBox));
      if (frameworkElement == null)
        return false;
      this._pluginProfileView = frameworkElement as PluginProfileView;
      return Microsoft.Crm.Tools.PluginRegistration.CommonControls.Helper.ParseReportOrShowError((Window) this._pluginProfileView, this._pluginProfileView.fileBrowser, requireReportParse, ref this._mReport);
    }

    private static Window GetParentWindow(DependencyObject container)
    {
      DependencyObject parent = LogicalTreeHelper.GetParent(container);
      return parent as Window ?? PluginProfileViewModel.GetParentWindow(parent);
    }
  }
}
