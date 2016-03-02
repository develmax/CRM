// Decompiled with JetBrains decompiler
// Type: Microsoft.Crm.Tools.PluginRegistration.ExistingWorkflowStep
// Assembly: PluginRegistration, Version=7.1.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: BDC54875-46F6-4E1E-9048-C47CF582701C
// Assembly location: C:\Distribs\CRM\sdk\SDK\Tools\PluginRegistration\PluginRegistration.exe

using Microsoft.Crm.Tools.Libraries;
using PluginProfiler.Library;
using System;
using System.Globalization;

namespace Microsoft.Crm.Tools.PluginRegistration
{
  public sealed class ExistingWorkflowStep : BaseNotifyable
  {
    private Activity _refreshButton;
    private bool _isSelected;

    public CustomActivityStep Step { get; private set; }

    public string StepTypeName
    {
      get
      {
        return string.Format((IFormatProvider) CultureInfo.InvariantCulture, "{0} (Type = {1})", new object[2]
        {
          (object) this.Step.StepId,
          (object) this.Step.TypeName
        });
      }
    }

    public bool IsSelected
    {
      get
      {
        return this._isSelected;
      }
      set
      {
        this._refreshButton();
        this.SetProperty<bool>(ref this._isSelected, value, "IsSelected");
      }
    }

    public ExistingWorkflowStep(CustomActivityStep step, Activity refreshButton)
    {
      this.Step = step;
      this._refreshButton = refreshButton;
    }

    public override string ToString()
    {
      return string.Format((IFormatProvider) CultureInfo.InvariantCulture, "{0} (Type = {1})", new object[2]
      {
        (object) this.Step.StepId,
        (object) this.Step.TypeName
      });
    }
  }
}
