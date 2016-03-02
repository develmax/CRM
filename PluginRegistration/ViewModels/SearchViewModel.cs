// Decompiled with JetBrains decompiler
// Type: Microsoft.Crm.Tools.PluginRegistration.SearchViewModel
// Assembly: PluginRegistration, Version=7.1.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: BDC54875-46F6-4E1E-9048-C47CF582701C
// Assembly location: C:\Distribs\CRM\sdk\SDK\Tools\PluginRegistration\PluginRegistration.exe

using Microsoft.Crm.Tools.Libraries;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;

namespace Microsoft.Crm.Tools.PluginRegistration
{
  public class SearchViewModel : BaseNotifyable
  {
    private readonly OrganizationControlViewModel _organizationControlViewModel;
    private ObservableCollection<ICrmTreeNode> _assembliesTree;
    private ICrmTreeNode _crmTreeNode;
    private ObservableCollection<ICrmTreeNode> _searchList;
    private string _searchText;
    private RelayCommand _searchButtonCommand;
    private readonly SearchView _searchView;
    private bool _isExpandedValue;
    private bool _focusableValue;
    private RelayCommand _selectButtonCommand;
    private RelayCommand _cancelButtonCommand;
    private readonly CrmViewType _view;
    private bool _isSelectedOnMain;

    internal object SelectedItem { get; set; }

    public RelayCommand CancelButtonCommand
    {
      get
      {
        this._cancelButtonCommand = new RelayCommand((Action<object>) (s => this.cancel_Click()));
        return this._cancelButtonCommand;
      }
    }

    public RelayCommand SelectButtonCommand
    {
      get
      {
        this._selectButtonCommand = new RelayCommand((Action<object>) (s => this.btnSelect_Click()));
        return this._selectButtonCommand;
      }
    }

    public bool FocusableValue
    {
      get
      {
        return this._focusableValue;
      }
      set
      {
        this.SetProperty<bool>(ref this._focusableValue, value, "FocusableValue");
      }
    }

    public bool IsExpandedValue
    {
      get
      {
        return this._isExpandedValue;
      }
      set
      {
        this.SetProperty<bool>(ref this._isExpandedValue, value, "IsExpandedValue");
      }
    }

    public RelayCommand SearchButtonCommand
    {
      get
      {
        this._searchButtonCommand = new RelayCommand((Action<object>) (s => this.btnSearch_Click()));
        return this._searchButtonCommand;
      }
    }

    public string SearchText
    {
      get
      {
        return this._searchText;
      }
      set
      {
        this.SetProperty<string>(ref this._searchText, value, "SearchText");
      }
    }

    public ObservableCollection<ICrmTreeNode> SearchList
    {
      get
      {
        return this._searchList;
      }
      set
      {
        this.SetProperty<ObservableCollection<ICrmTreeNode>>(ref this._searchList, value, "SearchList");
      }
    }

    internal SearchViewModel(OrganizationControlViewModel organizationControlViewModel, ObservableCollection<ICrmTreeNode> assembliesTree, ICrmTreeNode crmTreeNode, CrmViewType view, SearchView searchView)
    {
      this._searchView = searchView;
      this._searchView.SourceInitialized += new EventHandler(MaximizeHelper.OnMaximize);
      this._organizationControlViewModel = organizationControlViewModel;
      this._assembliesTree = assembliesTree;
      this._crmTreeNode = crmTreeNode;
      this._view = view;
      this.SearchList = view != CrmViewType.Assembly ? assembliesTree : this._organizationControlViewModel.LoadNodes(this._view, true);
      this.SelectedItem = this._organizationControlViewModel.SelectedItem;
    }

    private void btnSearch_Click()
    {
      ExtensionMethods.Searchable(this._organizationControlViewModel.AssembliesTree, false);
      this.SearchList = this._organizationControlViewModel.LoadNodes(this._view, true);
      ExtensionMethods.Searchable(this.SearchList, true);
      if (Convert.ToString(this.SearchText) == null || Convert.ToString(this.SearchText) == "")
      {
        this.IsExpandedValue = true;
        this.SearchList = this._organizationControlViewModel.LoadNodes(this._view, true);
      }
      else
      {
        this.SearchAndRemove(this.SearchText);
        this.IsExpandedValue = true;
        this.FocusableValue = true;
      }
      if (this.SearchList != null && this.SearchList.Count > 0 && (this.SelectedItem == null || !this.SearchList.Contains(this.SelectedItem as ICrmTreeNode)))
      {
        this.SelectedItem = (object) this.SearchList[0];
        (this.SelectedItem as ICrmTreeNode).IsNodeSelected = true;
      }
      else
      {
        if (this.SearchList == null || this.SearchList.Count != 0)
          return;
        this.SelectedItem = (object) null;
      }
    }

    private void btnSelect_Click()
    {
      this._isSelectedOnMain = true;
      ExtensionMethods.Searchable(this._organizationControlViewModel.AssembliesTree, false);
      ExtensionMethods.DeselectNodes(this._organizationControlViewModel.AssembliesTree);
      ExtensionMethods.SelectNode(this._organizationControlViewModel.AssembliesTree, this.SelectedItem as ICrmTreeNode);
      this._searchView.Close();
    }

    public void SelectionChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
    {
      if (e.NewValue == null)
        return;
      this.SelectedItem = e.NewValue;
    }

    private void cancel_Click()
    {
      this._searchView.Close();
    }

    public void ClosingSearchWindow(object sender, CancelEventArgs e)
    {
      if (this._isSelectedOnMain)
        return;
      ExtensionMethods.Searchable(this._organizationControlViewModel.AssembliesTree, false);
    }

    private void SearchAndRemove(string text)
    {
      if (text == null)
        throw new ArgumentNullException("text");
      List<ICrmTreeNode> unmatchedNodes = new List<ICrmTreeNode>();
      foreach (ICrmTreeNode node in (Collection<ICrmTreeNode>) this.SearchList)
      {
        if (node != null)
          this.Search(node, (List<ICrmTreeNode>) null, unmatchedNodes, text);
      }
      foreach (ICrmTreeNode node in unmatchedNodes)
      {
        if (node != null)
          ExtensionMethods.RemoveNode(this.SearchList, node);
      }
    }

    private bool Search(ICrmTreeNode node, List<ICrmTreeNode> matchedNodes, List<ICrmTreeNode> unmatchedNodes, string text)
    {
      if (node == null)
        throw new ArgumentNullException("node");
      bool flag1 = false;
      if (node.NodeChildren != null)
      {
        foreach (ICrmTreeNode node1 in (Collection<ICrmTreeNode>) node.NodeChildren)
        {
          if (this.Search(node1, matchedNodes, unmatchedNodes, text))
            flag1 = true;
        }
      }
      bool flag2 = flag1;
      if (!flag2)
        flag2 = node.NodeId.ToString().IndexOf(text, StringComparison.OrdinalIgnoreCase) != -1;
      if (!flag2)
        flag2 = node.NodeText.IndexOf(text, StringComparison.CurrentCultureIgnoreCase) != -1;
      if (flag2)
      {
        if (matchedNodes != null)
          matchedNodes.Add(node);
      }
      else if (unmatchedNodes != null)
        unmatchedNodes.Add(node);
      return flag2;
    }
  }
}
