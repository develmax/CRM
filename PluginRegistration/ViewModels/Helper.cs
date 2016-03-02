// Decompiled with JetBrains decompiler
// Type: Microsoft.Crm.Tools.PluginRegistration.Helper
// Assembly: PluginRegistration, Version=7.1.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: BDC54875-46F6-4E1E-9048-C47CF582701C
// Assembly location: C:\Distribs\CRM\sdk\SDK\Tools\PluginRegistration\PluginRegistration.exe

using Microsoft.Crm.Tools.Libraries;
using Microsoft.Crm.Tools.PluginRegistration.CommonControls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace Microsoft.Crm.Tools.PluginRegistration
{
  public static class Helper
  {
    public static void RefreshConnection(CrmOrganization org, CrmEntityDictionary<CrmMessage> messages)
    {
      Helper.RefreshConnection(org, messages, (MainViewModel) null);
    }

    public static void RefreshConnection(CrmOrganization org, CrmEntityDictionary<CrmMessage> messages, MainViewModel mainViewModel)
    {
      if (org == null)
        throw new ArgumentNullException("org");
      org.Connected = false;
      Helper.OpenConnection(org, messages, mainViewModel);
    }

    public static void OpenConnection(CrmOrganization org, CrmEntityDictionary<CrmMessage> messages)
    {
      Helper.OpenConnection(org, messages, (MainViewModel) null);
    }

    public static void OpenConnection(CrmOrganization org, CrmEntityDictionary<CrmMessage> messages, MainViewModel mainViewModel)
    {
      if (org == null)
        throw new ArgumentNullException("org");
      if (org.Connected)
        return;
      Thread mainThread = Application.Current.Dispatcher.Thread;
      bool loadedCompletely = false;
      bool isSuccessfullyLoaded = true;
      mainViewModel.IsMainViewEnabled = false;
      ProgressBarView progressBarView = new ProgressBarView();
      progressBarView.Owner = Application.Current.MainWindow;
      progressBarView.DataContext = (object) mainViewModel;
      ProgressBarView _progBarView = progressBarView;
      _progBarView.Show();
      BackgroundWorker worker = new BackgroundWorker();
      worker.DoWork += (DoWorkEventHandler) ((o, e) =>
      {
        try
        {
          worker.ReportProgress(1, (object) "Loading Services...");
          OrganizationHelper.LoadUsers(org);
          worker.ReportProgress(2, (object) "Loading Users...");
          org.LoggedOnUser = OrganizationHelper.GetLoggedOnUser(org);
          worker.ReportProgress(3, (object) "Get Logged In User...");
          OrganizationHelper.LoadMessageEntities(org, messages);
          worker.ReportProgress(4, (object) "Loading Messages...");
          OrganizationHelper.LoadAssemblies(org);
          worker.ReportProgress(5, (object) "Loading Assemblies...");
          Dictionary<Guid, CrmPlugin> typeList;
          OrganizationHelper.LoadPlugins(org, (IEnumerable<Guid>) null, out typeList);
          worker.ReportProgress(6, (object) "Loading Plugins...");
          OrganizationHelper.LoadServiceEndpoints(org);
          worker.ReportProgress(7, (object) "Loading Service EndPoints...");
          OrganizationHelper.LoadProfiledProcesses(org);
          worker.ReportProgress(8, (object) "Loading Profiled Processes...");
          Dictionary<Guid, CrmPluginStep> crmStepList;
          OrganizationHelper.LoadSteps(org, typeList, out crmStepList);
          worker.ReportProgress(9, (object) "Loading Steps...");
          OrganizationHelper.LoadImages(org, crmStepList);
          worker.ReportProgress(10, (object) "Loading Images...");
          org.ClearAllEntityAttributes();
          loadedCompletely = true;
        }
        catch (Exception ex)
        {
          Dispatcher.FromThread(mainThread).Invoke((Action) (() =>
          {
            _progBarView.Close();
            ErrorMessageViewModel.ShowErrorMessageBox((Window) null, "Unable to load the CRM Organization", "Organization Error", ex, (UserControl) null);
            mainViewModel.IsMainViewEnabled = true;
            isSuccessfullyLoaded = false;
          }));
        }
      });
      worker.ProgressChanged += (ProgressChangedEventHandler) ((o, e) =>
      {
        mainViewModel.ProgressStatusVal = (double) e.ProgressPercentage;
        mainViewModel.ProgressStatusTxt = Convert.ToString(e.UserState);
      });
      worker.RunWorkerCompleted += (RunWorkerCompletedEventHandler) ((o, e) =>
      {
        if (isSuccessfullyLoaded)
        {
          string tempViewstatus = Convert.ToString((object) CrmViewType.Assembly);
          _progBarView.Close();
          mainViewModel.IsMainViewEnabled = true;
          mainViewModel.CreateTab(tempViewstatus, org, (TabItem) null, false);
          Application.Current.MainWindow.Focus();
        }
        org.Connected = loadedCompletely;
      });
      worker.WorkerReportsProgress = true;
      worker.RunWorkerAsync();
    }

    public static void OpenConnectionForReload(MainViewModel mainViewModel, TabItem currentOrganization, bool isReload)
    {
      bool loadedCompletely = false;
      bool isSuccessfullyLoaded = true;
      ObservableCollection<CrmOrganization> _crmOrgCollection = new ObservableCollection<CrmOrganization>();
      Thread mainThread = Application.Current.Dispatcher.Thread;
      mainViewModel.IsMainViewEnabled = false;
      mainViewModel.ProgressStatusTxt = string.Empty;
      CrmOrganization crmOrganization = new CrmOrganization();
      ProgressBarView progressBarView = new ProgressBarView();
      progressBarView.Owner = Application.Current.MainWindow;
      progressBarView.DataContext = (object) mainViewModel;
      ProgressBarView _progBarView = progressBarView;
      BackgroundWorker worker = new BackgroundWorker();
      _progBarView.Show();
      CrmEntityDictionary<CrmMessage> messages;
      worker.DoWork += (DoWorkEventHandler) ((o, e) =>
      {
        try
        {
          foreach (TabItem tabItem in (Collection<TabItem>) mainViewModel.OrganizationsTab.ListTabOrganizations)
          {
            TabItem temp = tabItem;
            Dispatcher.FromThread(mainThread).Invoke((Action) (() => crmOrganization = temp.Tag as CrmOrganization));
            if (crmOrganization != null)
            {
              messages = mainViewModel.LoadMessages(crmOrganization);
              _crmOrgCollection.Add(crmOrganization);
              mainViewModel.OrganizationsTab.CrmOrganization = crmOrganization;
              mainViewModel.OrganizationsTab.MainViewModel = mainViewModel;
              worker.ReportProgress(1, (object) (crmOrganization.OrganizationFriendlyName + "- Loading Services..."));
              OrganizationHelper.LoadUsers(crmOrganization);
              worker.ReportProgress(2, (object) (crmOrganization.OrganizationFriendlyName + "- Loading Users..."));
              crmOrganization.LoggedOnUser = OrganizationHelper.GetLoggedOnUser(crmOrganization);
              worker.ReportProgress(3, (object) (crmOrganization.OrganizationFriendlyName + "- Get Logged In User..."));
              OrganizationHelper.LoadMessageEntities(crmOrganization, messages);
              worker.ReportProgress(4, (object) (crmOrganization.OrganizationFriendlyName + "- Loading Messages..."));
              OrganizationHelper.LoadAssemblies(crmOrganization);
              worker.ReportProgress(5, (object) (crmOrganization.OrganizationFriendlyName + "- Loading Assemblies..."));
              Dictionary<Guid, CrmPlugin> typeList;
              OrganizationHelper.LoadPlugins(crmOrganization, (IEnumerable<Guid>) null, out typeList);
              worker.ReportProgress(6, (object) (crmOrganization.OrganizationFriendlyName + "- Loading Plugins..."));
              OrganizationHelper.LoadServiceEndpoints(crmOrganization);
              worker.ReportProgress(7, (object) (crmOrganization.OrganizationFriendlyName + "- Loading Service EndPoints..."));
              OrganizationHelper.LoadProfiledProcesses(crmOrganization);
              worker.ReportProgress(8, (object) (crmOrganization.OrganizationFriendlyName + "- Loading Profiled Processes..."));
              Dictionary<Guid, CrmPluginStep> crmStepList;
              OrganizationHelper.LoadSteps(crmOrganization, typeList, out crmStepList);
              worker.ReportProgress(9, (object) (crmOrganization.OrganizationFriendlyName + "- Loading Steps..."));
              OrganizationHelper.LoadImages(crmOrganization, crmStepList);
              worker.ReportProgress(10, (object) (crmOrganization.OrganizationFriendlyName + "- Loading Images..."));
              crmOrganization.ClearAllEntityAttributes();
              loadedCompletely = true;
            }
          }
        }
        catch (Exception ex)
        {
          Dispatcher.FromThread(mainThread).Invoke((Action) (() =>
          {
            _progBarView.Close();
            ErrorMessageViewModel.ShowErrorMessageBox((Window) null, "Unable to load the CRM Organization", "Organization Error", ex, (UserControl) null);
            mainViewModel.IsMainViewEnabled = true;
            isSuccessfullyLoaded = false;
          }));
        }
      });
      worker.ProgressChanged += (ProgressChangedEventHandler) ((o, e) =>
      {
        mainViewModel.ProgressStatusVal = (double) e.ProgressPercentage;
        mainViewModel.ProgressStatusTxt = Convert.ToString(e.UserState);
      });
      worker.RunWorkerCompleted += (RunWorkerCompletedEventHandler) ((o, e) =>
      {
        if (isSuccessfullyLoaded)
        {
          _progBarView.Close();
          mainViewModel.IsMainViewEnabled = true;
          foreach (CrmOrganization org in (Collection<CrmOrganization>) _crmOrgCollection)
          {
            string tempViewstatus;
            mainViewModel._viewTypeStatus.TryGetValue(org.OrganizationId, out tempViewstatus);
            mainViewModel.CreateTab(tempViewstatus, org, currentOrganization, isReload);
            org.Connected = loadedCompletely;
          }
          Application.Current.MainWindow.Focus();
        }
        mainViewModel._isReload = false;
      });
      worker.WorkerReportsProgress = true;
      worker.RunWorkerAsync();
    }
  }
}
