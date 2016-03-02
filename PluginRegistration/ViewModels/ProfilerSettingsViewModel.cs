// Decompiled with JetBrains decompiler
// Type: Microsoft.Crm.Tools.PluginRegistration.ProfilerSettingsViewModel
// Assembly: PluginRegistration, Version=7.1.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: BDC54875-46F6-4E1E-9048-C47CF582701C
// Assembly location: C:\Distribs\CRM\sdk\SDK\Tools\PluginRegistration\PluginRegistration.exe

using CrmSdk;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Crm.Tools.Libraries;
using Microsoft.Crm.Tools.PluginRegistration.CommonControls;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using PluginProfiler.Library;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.ServiceModel;
using System.Windows;
using System.Windows.Controls;

namespace Microsoft.Crm.Tools.PluginRegistration
{
  public class ProfilerSettingsViewModel : BaseNotifyable
  {
    private readonly PluginProfiler.OperationType _operationType;
    private MessageBoxResult _messageBoxResult;
    private readonly CrmOrganization _org;
    private bool _okButtonEnable;
    private static ProfilerSettingsView _profilerview;
    private bool _maxNumberExecutionsEnabledBoxChecked;
    private bool _activateWorkflowBoxChecked;
    private bool _persistenceKeyBox;
    private bool _maxExecutionsBoxEnable;
    private bool _includeSecureInfoBoxChecked;
    private bool _entityPersistenceRadioChecked;
    private bool _persistencelbl;
    private bool _lblMax;
    private string _maxExecutionsBox;
    private string _persistenceKeyBoxText;
    private string _step0Header;
    private string _step1Header;
    private string _step2Header;
    private double _winMinHeight;
    private Visibility _step1Visible;
    private Visibility _step2Visible;
    private Visibility _step0Visible;
    private ObservableCollection<ExistingWorkflow> _workflowsList;
    private ExistingWorkflow _selectedWorkFlow;
    private ObservableCollection<ExistingWorkflowStep> _workFlowSteps;
    private ExistingWorkflowStep _selectedWorkFlowStep;
    private System.Drawing.Image _imageResource;
    private RelayCommand _cmdOk;
    private RelayCommand _cmdClose;
    private RelayCommand _cmdPersistentChecked;
    private RelayCommand _cmdrefreshWorkflows;

    internal Guid ProfiledOperationId { get; private set; }

    internal Guid ProfilerOperationId { get; private set; }

    public bool LblMax
    {
      get
      {
        return this._lblMax;
      }
      set
      {
        this.SetProperty<bool>(ref this._lblMax, value, "lblMax");
      }
    }

    public bool Persistencelbl
    {
      get
      {
        return this._persistencelbl;
      }
      set
      {
        this.SetProperty<bool>(ref this._persistencelbl, value, "Persistencelbl");
      }
    }

    internal MessageBoxResult MessageBoxResult
    {
      get
      {
        return this._messageBoxResult;
      }
      set
      {
        this.SetProperty<MessageBoxResult>(ref this._messageBoxResult, value, "MessageBoxResult");
      }
    }

    public bool OkButtonEnable
    {
      get
      {
        return this._okButtonEnable;
      }
      set
      {
        this.SetProperty<bool>(ref this._okButtonEnable, value, "OkButtonEnable");
      }
    }

    public bool MaxNumberExecutionsEnabledBoxChecked
    {
      get
      {
        return this._maxNumberExecutionsEnabledBoxChecked;
      }
      set
      {
        this.SetProperty<bool>(ref this._maxNumberExecutionsEnabledBoxChecked, value, "MaxNumberExecutionsEnabledBoxChecked");
        if (this.MaxNumberExecutionsEnabledBoxChecked)
        {
          this.MaxExecutionsBoxEnable = this.MaxNumberExecutionsEnabledBoxChecked;
          this.LblMax = this.MaxNumberExecutionsEnabledBoxChecked;
        }
        else
        {
          this.MaxExecutionsBoxEnable = this.MaxNumberExecutionsEnabledBoxChecked;
          this.LblMax = this.MaxNumberExecutionsEnabledBoxChecked;
        }
      }
    }

    public bool EntityPersistenceRadioChecked
    {
      get
      {
        return this._entityPersistenceRadioChecked;
      }
      set
      {
        this.SetProperty<bool>(ref this._entityPersistenceRadioChecked, value, "EntityPersistenceRadioChecked");
        if (this.EntityPersistenceRadioChecked)
        {
          this.PersistenceKeyBox = this.EntityPersistenceRadioChecked;
          this.Persistencelbl = this.EntityPersistenceRadioChecked;
        }
        else
        {
          this.PersistenceKeyBox = this.EntityPersistenceRadioChecked;
          this.Persistencelbl = this.EntityPersistenceRadioChecked;
        }
      }
    }

    public bool IncludeSecureInfoBoxChecked
    {
      get
      {
        return this._includeSecureInfoBoxChecked;
      }
      set
      {
        this.SetProperty<bool>(ref this._includeSecureInfoBoxChecked, value, "IncludeSecureInfoBoxChecked");
      }
    }

    public bool ActivateWorkflowBoxChecked
    {
      get
      {
        return this._activateWorkflowBoxChecked;
      }
      set
      {
        this.SetProperty<bool>(ref this._activateWorkflowBoxChecked, value, "ActivateWorkflowBoxChecked");
      }
    }

    public bool PersistenceKeyBox
    {
      get
      {
        return this._persistenceKeyBox;
      }
      set
      {
        this.SetProperty<bool>(ref this._persistenceKeyBox, value, "PersistenceKeyBox");
      }
    }

    public bool MaxExecutionsBoxEnable
    {
      get
      {
        return this._maxExecutionsBoxEnable;
      }
      set
      {
        this.SetProperty<bool>(ref this._maxExecutionsBoxEnable, value, "MaxExecutionsBoxEnable");
      }
    }

    public string PersistenceKeyBoxText
    {
      get
      {
        return this._persistenceKeyBoxText;
      }
      set
      {
        this.SetProperty<string>(ref this._persistenceKeyBoxText, value, "PersistenceKeyBoxText");
      }
    }

    public string MaxExecutionsBox
    {
      get
      {
        return this._maxExecutionsBox;
      }
      set
      {
        this.SetProperty<string>(ref this._maxExecutionsBox, value, "MaxExecutionsBox");
        if (-1 == this.ValidateMaxExecutions())
        {
          int num = (int) MessageBox.Show("Number must be a valid, positive number.", "Error", MessageBoxButton.OK, MessageBoxImage.Hand);
        }
        else
          this.RefreshOkButton();
      }
    }

    public string Step0Header
    {
      get
      {
        return this._step0Header;
      }
      set
      {
        this.SetProperty<string>(ref this._step0Header, value, "Step0Header");
      }
    }

    public string Step1Header
    {
      get
      {
        return this._step1Header;
      }
      set
      {
        this.SetProperty<string>(ref this._step1Header, value, "Step1Header");
      }
    }

    public string Step2Header
    {
      get
      {
        return this._step2Header;
      }
      set
      {
        this.SetProperty<string>(ref this._step2Header, value, "Step2Header");
      }
    }

    public Visibility Step0Visible
    {
      get
      {
        return this._step0Visible;
      }
      set
      {
        this.SetProperty<Visibility>(ref this._step0Visible, value, "Step0Visible");
      }
    }

    public Visibility Step1Visible
    {
      get
      {
        return this._step1Visible;
      }
      set
      {
        this.SetProperty<Visibility>(ref this._step1Visible, value, "Step1Visible");
      }
    }

    public Visibility Step2Visible
    {
      get
      {
        return this._step2Visible;
      }
      set
      {
        this.SetProperty<Visibility>(ref this._step2Visible, value, "Step2Visible");
      }
    }

    public ObservableCollection<ExistingWorkflow> WorkflowsList
    {
      get
      {
        return this._workflowsList;
      }
      set
      {
        this.SetProperty<ObservableCollection<ExistingWorkflow>>(ref this._workflowsList, value, "WorkflowsList");
      }
    }

    public ExistingWorkflow SelectedWorkFlow
    {
      get
      {
        return this._selectedWorkFlow;
      }
      set
      {
        this.SetProperty<ExistingWorkflow>(ref this._selectedWorkFlow, value, "SelectedWorkFlow");
        if (this.WorkFlowSteps != null && this.WorkFlowSteps.Count > 0)
        {
          foreach (ExistingWorkflowStep existingWorkflowStep in (Collection<ExistingWorkflowStep>) this.WorkFlowSteps)
          {
            if (existingWorkflowStep.IsSelected && MessageBox.Show("Are you sure you want to change the selected Workflow?", "Change Workflow", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No) != MessageBoxResult.Yes)
              return;
          }
        }
        ObservableCollection<ExistingWorkflowStep> observableCollection = new ObservableCollection<ExistingWorkflowStep>();
        if (value != null && value.Steps != null)
        {
          foreach (ExistingWorkflowStep existingWorkflowStep in value.Steps)
            observableCollection.Add(existingWorkflowStep);
        }
        this.WorkFlowSteps = observableCollection;
        this.RefreshOkButton();
      }
    }

    public ObservableCollection<ExistingWorkflowStep> WorkFlowSteps
    {
      get
      {
        return this._workFlowSteps;
      }
      set
      {
        this.SetProperty<ObservableCollection<ExistingWorkflowStep>>(ref this._workFlowSteps, value, "WorkFlowSteps");
      }
    }

    public ExistingWorkflowStep SelectedWorkFlowStep
    {
      get
      {
        return this._selectedWorkFlowStep;
      }
      set
      {
        this.SetProperty<ExistingWorkflowStep>(ref this._selectedWorkFlowStep, value, "SelectedWorkFlowStep");
      }
    }

    public System.Drawing.Image ImageResource
    {
      get
      {
        return this._imageResource;
      }
      set
      {
        this.SetProperty<System.Drawing.Image>(ref this._imageResource, value, "ImageResource");
      }
    }

    public RelayCommand CmdPersistentChecked
    {
      get
      {
        return this._cmdPersistentChecked ?? (this._cmdPersistentChecked = new RelayCommand((Action<object>) (s => this.PersistentChecked())));
      }
    }

    public RelayCommand CmdOk
    {
      get
      {
        return this._cmdOk ?? (this._cmdOk = new RelayCommand((Action<object>) (s => this.btnOk_Click())));
      }
    }

    public RelayCommand Close
    {
      get
      {
        return this._cmdClose ?? (this._cmdClose = new RelayCommand((Action<object>) (s => this.btnClose_Click())));
      }
    }

    public RelayCommand CmdRefreshWorkflows
    {
      get
      {
        return this._cmdrefreshWorkflows ?? (this._cmdrefreshWorkflows = new RelayCommand((Action<object>) (s => this.RefreshWorkflowsButton_Click())));
      }
    }

    public double WinMinHeight
    {
      get
      {
        return this._winMinHeight;
      }
      set
      {
        this.SetProperty<double>(ref this._winMinHeight, value, "WinMinHeight");
      }
    }

    public ProfilerSettingsViewModel(CrmOrganization crmOrg, PluginProfiler.OperationType type, Guid operationId, ProfilerSettingsView profilerSettingsView)
    {
      ProfilerSettingsViewModel._profilerview = profilerSettingsView;
      ProfilerSettingsViewModel._profilerview.SourceInitialized += new EventHandler(MaximizeHelper.OnMaximize);
      if (crmOrg == null)
        throw new ArgumentNullException("crmOrganization");
      Dictionary<string, System.Drawing.Image> dictionary = (Dictionary<string, System.Drawing.Image>) null;
      try
      {
        this.ImageResource = CrmResources.LoadImage("Refresh");
      }
      catch (Exception)
      {
        foreach (System.Drawing.Image image in dictionary.Values)
          image.Dispose();
        throw;
      }
      this._operationType = type;
      this.ProfiledOperationId = operationId;
      this._org = crmOrg;
      switch (type)
      {
        case PluginProfiler.OperationType.Plugin:
          this.WinMinHeight = 515.0;
          this.Step0Visible = Visibility.Collapsed;
          this.Step1Visible = Visibility.Visible;
          this.Step2Visible = Visibility.Visible;
          this.Step1Header = "Step 1: Specify profile storage";
          this.Step2Header = "Step 2: Set profiler settings";
          profilerSettingsView.rbexception.IsEnabled = true;
          profilerSettingsView.rbexception.IsChecked = new bool?(true);
          profilerSettingsView.rbpersistent.IsEnabled = true;
          profilerSettingsView.cbmaxnuberexec.IsEnabled = true;
          this.IncludeSecureInfoBoxChecked = true;
          break;
        case PluginProfiler.OperationType.WorkflowActivity:
          this.WinMinHeight = 730.0;
          this.Step0Header = "Step 1: Select workflow steps";
          this.Step1Header = "Step 2: Specify profile storage";
          this.Step2Header = "Step 3: Set profiler settings";
          this.Step0Visible = Visibility.Visible;
          this.Step1Visible = Visibility.Visible;
          this.Step2Visible = Visibility.Visible;
          profilerSettingsView.rbexception.IsChecked = new bool?(true);
          profilerSettingsView.rbexception.IsEnabled = true;
          profilerSettingsView.rbpersistent.IsEnabled = true;
          profilerSettingsView.cbmaxnuberexec.Visibility = Visibility.Collapsed;
          this.IncludeSecureInfoBoxChecked = true;
          profilerSettingsView.maxlbl.Visibility = Visibility.Collapsed;
          profilerSettingsView.txtmax.Visibility = Visibility.Collapsed;
          this.ActivateWorkflowBoxChecked = true;
          this.RefreshWorkflows();
          break;
        default:
          throw new NotImplementedException("OperationType = " + (object) type);
      }
      this.PersistenceKeyBoxText = Guid.NewGuid().ToString("N");
      this.MaxExecutionsBox = 1.ToString();
      this.RefreshOkButton();
    }

    private void btnClose_Click()
    {
      ProfilerSettingsViewModel._profilerview.DialogResult = new bool?(false);
      ProfilerSettingsViewModel._profilerview.Close();
    }

    private void btnOk_Click()
    {
      try
      {
        switch (this._operationType)
        {
          case PluginProfiler.OperationType.Plugin:
            this.ProfilerOperationId = ProfilerManagementUtility.EnablePlugin(this._org.CrmServiceConnection, this.ProfiledOperationId, this.EntityPersistenceRadioChecked, this.PersistenceKeyBoxText, new int?(this.ValidateMaxExecutions()), this.IncludeSecureInfoBoxChecked);
            break;
          case PluginProfiler.OperationType.WorkflowActivity:
            List<CustomActivityStep> list = new List<CustomActivityStep>();
            foreach (ExistingWorkflowStep existingWorkflowStep in (Collection<ExistingWorkflowStep>) this.WorkFlowSteps)
            {
              if (existingWorkflowStep.IsSelected)
                list.Add(existingWorkflowStep.Step);
            }
            this.ProfilerOperationId = ProfilerManagementUtility.EnableWorkflow(this._org.CrmServiceConnection, (string) null, this.SelectedWorkFlow.Id, this.EntityPersistenceRadioChecked, this.PersistenceKeyBoxText, this.IncludeSecureInfoBoxChecked, false, list.ToArray());
            this.ProfiledOperationId = this.SelectedWorkFlow.Id;
            if (this.ActivateWorkflowBoxChecked)
            {
              try
              {
                if ((SetStateResponse) this._org.CrmServiceConnection.ExecuteCrmOrganizationRequest(this._org.AttachSolutionToRequest((OrganizationRequest) new SetStateRequest()
                {
                  EntityMoniker = new EntityReference("workflow", this.ProfilerOperationId),
                  State = new OptionSetValue(1),
                  Status = new OptionSetValue(-1)
                }), "Publish Request") == null)
                {
                  if (this._org.CrmServiceConnection.LastCrmException != null)
                    throw this._org.CrmServiceConnection.LastCrmException;
                  break;
                }
                break;
              }
              catch (FaultException<OrganizationServiceFault> ex)
              {
                ErrorMessageViewModel.ShowErrorMessageBox((Window) null, "An error occurred while trying to activate the profiler workflow. In order to use this profiled workflow, you must activate it manually.", "Error", (Exception) ex, (UserControl) null);
                break;
              }
            }
            else
              break;
        }
        this.MessageBoxResult = MessageBoxResult.OK;
        ProfilerSettingsViewModel._profilerview.DialogResult = new bool?(true);
        ProfilerSettingsViewModel._profilerview.Close();
      }
      catch (Exception ex)
      {
        ErrorMessageViewModel.ShowErrorMessageBox((Window) null, "Unable to enable or update profiling configuration due to an error.", "Profiling", ex, (UserControl) null);
      }
    }

    private void RefreshWorkflowsButton_Click()
    {
      if (this.SelectedWorkFlow == null)
      {
        if (Queryable.Count<ExistingWorkflowStep>(Queryable.AsQueryable<ExistingWorkflowStep>((IEnumerable<ExistingWorkflowStep>) this.WorkFlowSteps), (System.Linq.Expressions.Expression<Func<ExistingWorkflowStep, bool>>) (s => s.IsSelected == true)) <= 0)
          goto label_3;
      }
      if (MessageBox.Show("Are you sure you want to change the refresh the Workflows?", "Refresh", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No) != MessageBoxResult.Yes)
        return;
label_3:
      this.RefreshWorkflows();
    }

    private void PersistentChecked()
    {
    }

    private void RefreshWorkflows()
    {
      QueryExpression queryExpression = new QueryExpression("workflow")
      {
        ColumnSet = new ColumnSet(new string[2]
        {
          "name",
          "xaml"
        })
      };
      queryExpression.Criteria.AddCondition("ownerid", ConditionOperator.EqualUserId);
      queryExpression.Criteria.AddCondition("type", ConditionOperator.Equal, (object) 1);
      queryExpression.Criteria.AddCondition("name", ConditionOperator.DoesNotEndWith, (object) " (Profiled)");
      queryExpression.Criteria.AddCondition("description", ConditionOperator.DoesNotBeginWith, (object) "|P|");
      EntityCollection entityCollection = CrmServiceConnectionExtensions.RetrieveMultipleAllPages(this._org.CrmServiceConnection, (QueryBase) queryExpression, "Not Reported");
      ObservableCollection<ExistingWorkflow> observableCollection = new ObservableCollection<ExistingWorkflow>();
      foreach (Workflow workflow in (Collection<Entity>) entityCollection.Entities)
        observableCollection.Add(new ExistingWorkflow(this._org.CrmServiceConnection, workflow, new Activity(this.RefreshOkButton)));
      this.WorkflowsList = observableCollection;
      if (this.WorkflowsList.Count > 0)
        this.SelectedWorkFlow = this.WorkflowsList[0];
      else
        ProfilerSettingsViewModel._profilerview.btnrefresh.IsEnabled = false;
      this.RefreshOkButton();
    }

    private void RefreshOkButton()
    {
      if (PluginProfiler.OperationType.WorkflowActivity == this._operationType && this.WorkflowsList.Count == 0)
        this.OkButtonEnable = false;
      else if (this.EntityPersistenceRadioChecked && this._persistenceKeyBoxText.Length == 0)
        this.OkButtonEnable = false;
      else if (-1 == this.ValidateMaxExecutions())
        this.OkButtonEnable = false;
      else
        this.OkButtonEnable = true;
    }

    private int ValidateMaxExecutions()
    {
      int result;
      if (string.IsNullOrWhiteSpace(this.MaxExecutionsBox) || !int.TryParse(this.MaxExecutionsBox, NumberStyles.Integer, (IFormatProvider) CultureInfo.InvariantCulture, out result) || result < 0)
        return -1;
      return result;
    }
  }
}
