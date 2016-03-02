// Decompiled with JetBrains decompiler
// Type: Microsoft.Crm.Tools.PluginRegistration.OrganizationsViewModel
// Assembly: PluginRegistration, Version=7.1.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: BDC54875-46F6-4E1E-9048-C47CF582701C
// Assembly location: C:\Distribs\CRM\sdk\SDK\Tools\PluginRegistration\PluginRegistration.exe

using Microsoft.Crm.Tools.Libraries;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace Microsoft.Crm.Tools.PluginRegistration
{
  public class OrganizationsViewModel : BaseNotifyable
  {
    private Dictionary<Guid, Dictionary<Guid, OrganizationControlView>> _orgList;
    private ObservableCollection<TabItem> _listTabOrganizations;
    private TabItem _selectedOrganization;
    private RelayCommand _cmdCloseOrganization;
    private bool _isInitialLoad;

    public CrmOrganization CrmOrganization { get; set; }

    public MainViewModel MainViewModel { get; set; }

    public TabItem SelectedOrganization
    {
      get
      {
        return this._selectedOrganization;
      }
      set
      {
        this.SetProperty<TabItem>(ref this._selectedOrganization, value, "SelectedOrganization");
        this.tabOrganizations_SelectedIndexChanged();
      }
    }

    public ObservableCollection<TabItem> ListTabOrganizations
    {
      get
      {
        return this._listTabOrganizations;
      }
      set
      {
        this.SetProperty<ObservableCollection<TabItem>>(ref this._listTabOrganizations, value, "ListTabOrganizations");
      }
    }

    public RelayCommand CmdCloseOrganization
    {
      get
      {
        if (this._cmdCloseOrganization == null)
          this._cmdCloseOrganization = new RelayCommand((Action<object>) (s => this.lblClose_Click(s)));
        return this._cmdCloseOrganization;
      }
    }

    public OrganizationsViewModel(CrmOrganization crmOrganization, MainViewModel mainViewModel, bool isInitialLoad = true)
    {
      this._orgList = new Dictionary<Guid, Dictionary<Guid, OrganizationControlView>>();
      this.MainViewModel = mainViewModel;
      this.CrmOrganization = crmOrganization;
      this._isInitialLoad = isInitialLoad;
    }

    private void lblClose_Click(object s)
    {
      if (this.SelectedOrganization == null)
        return;
      this.CloseOrganizationTab(((CrmOrganization) this.SelectedOrganization.Tag).OrganizationId);
    }

    private void tabOrganizations_SelectedIndexChanged()
    {
      if (this.SelectedOrganization != null)
      {
        this.MainViewModel.SelectedOrganization = (OrganizationControlViewModel) (this.SelectedOrganization.Content as OrganizationControlView).DataContext;
        this.MainViewModel.UpdateCurrentOrganization((CrmOrganization) this.SelectedOrganization.Tag);
        App.ActiveOrganizationId = ((CrmOrganization) this.SelectedOrganization.Tag).OrganizationId;
      }
      if (this.MainViewModel.OrganizationsTab != null && this.MainViewModel.OrganizationsTab.ListTabOrganizations.Count != 0)
        return;
      this.MainViewModel.SelectedOrganization = (OrganizationControlViewModel) null;
      App.ActiveOrganizationId = Guid.Empty;
    }

    private void CloseOrganizationTab(Guid organizationId)
    {
      this.ListTabOrganizations.Remove(this.GetTab(organizationId));
      this.MainViewModel.OrgTabList.Remove(organizationId);
      this.MainViewModel._viewTypeStatus.Remove(organizationId);
      if (this.ListTabOrganizations.Count == 0)
      {
        this.SelectedOrganization = (TabItem) null;
        this.MainViewModel.OrganizationStatus = string.Empty;
      }
      else
      {
        this.SelectedOrganization = this.ListTabOrganizations[0];
        this.MainViewModel.UpdateCurrentOrganization((CrmOrganization) this.SelectedOrganization.Tag);
      }
    }

    public TabItem GetTab(Guid organizationId)
    {
      TabItem tabItem1 = this.MainViewModel.OrgTabList[organizationId];
      foreach (TabItem tabItem2 in (Collection<TabItem>) this.ListTabOrganizations)
      {
        if ((tabItem2.Tag as CrmOrganization).OrganizationId == (tabItem1.Tag as CrmOrganization).OrganizationId)
          return tabItem2;
      }
      return (TabItem) null;
    }
  }
}
