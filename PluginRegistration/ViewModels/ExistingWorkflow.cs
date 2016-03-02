// Decompiled with JetBrains decompiler
// Type: Microsoft.Crm.Tools.PluginRegistration.ExistingWorkflow
// Assembly: PluginRegistration, Version=7.1.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: BDC54875-46F6-4E1E-9048-C47CF582701C
// Assembly location: C:\Distribs\CRM\sdk\SDK\Tools\PluginRegistration\PluginRegistration.exe

using CrmSdk;
using Microsoft.Crm.Tools.Libraries;
using Microsoft.Xrm.Tooling.Connector;
using PluginProfiler.Library;
using System;
using System.Collections.Generic;

namespace Microsoft.Crm.Tools.PluginRegistration
{
  public sealed class ExistingWorkflow : BaseNotifyable
  {
    private Activity _refreshOkButton;
    private readonly CrmServiceClient _service;
    private readonly string Xaml;
    private ExistingWorkflowStep[] _steps;

    public Guid Id { get; private set; }

    public string Name { get; private set; }

    public ExistingWorkflowStep[] Steps
    {
      get
      {
        if (this._steps == null)
          this.Refresh();
        return this._steps;
      }
    }

    public ExistingWorkflow()
    {
    }

    public ExistingWorkflow(CrmServiceClient service, Workflow workflow, Activity refreshOkButton)
    {
      this._service = service;
      this.Id = workflow.Id;
      this.Name = workflow.Name;
      this.Xaml = workflow.Xaml;
      this._refreshOkButton = refreshOkButton;
    }

    public override string ToString()
    {
      return this.Name;
    }

    public void Refresh()
    {
      List<ExistingWorkflowStep> list = new List<ExistingWorkflowStep>();
      foreach (CustomActivityStep step in (IEnumerable<CustomActivityStep>) ProfilerManagementUtility.GetWorkflowActivitySteps(this._service, this.Xaml).Values)
        list.Add(new ExistingWorkflowStep(step, this._refreshOkButton));
      this._steps = list.ToArray();
    }
  }
}
