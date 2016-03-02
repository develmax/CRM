// Decompiled with JetBrains decompiler
// Type: Microsoft.Crm.Tools.PluginRegistration.AboutViewModel
// Assembly: PluginRegistration, Version=7.1.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: BDC54875-46F6-4E1E-9048-C47CF582701C
// Assembly location: C:\Distribs\CRM\sdk\SDK\Tools\PluginRegistration\PluginRegistration.exe

using Microsoft.Crm.Tools.Libraries;
using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Reflection;

namespace Microsoft.Crm.Tools.PluginRegistration
{
  internal class AboutViewModel : BaseNotifyable
  {
    private string _title;
    private string _version;
    private RelayCommand _btnClose;
    private string windowsVersionInfo;
    private string aboutWindowTitle;

    public string AboutWindowTitle
    {
      get
      {
        return this.aboutWindowTitle;
      }
      set
      {
        this.SetProperty<string>(ref this.aboutWindowTitle, value, "AboutWindowTitle");
      }
    }

    public string Title
    {
      get
      {
        return this._title;
      }
      set
      {
        this.SetProperty<string>(ref this._title, value, "Title");
      }
    }

    public string ToolVersion
    {
      get
      {
        return this._version;
      }
      set
      {
        this.SetProperty<string>(ref this._version, value, "ToolVersion");
      }
    }

    public string WindowsVersionInfo
    {
      get
      {
        return this.windowsVersionInfo;
      }
      set
      {
        this.SetProperty<string>(ref this.windowsVersionInfo, value, "WindowsVersionInfo");
      }
    }

    public RelayCommand CmdClose
    {
      get
      {
        return this._btnClose ?? (this._btnClose = new RelayCommand((Action<object>) (s => this.BtnCloseClick())));
      }
    }

    public AboutViewModel()
    {
      this.AboutWindowTitle = "About Plugin Registration Tool";
      this.Title = "Microsoft Dynamics CRM Plugin Registration Tool";
      this.WindowsVersionInfo = string.Format((IFormatProvider) CultureInfo.InvariantCulture, "{0} {1}", new object[2]
      {
        (object) Environment.OSVersion.VersionString,
        Environment.Is64BitOperatingSystem ? (object) "64bit" : (object) "32bit"
      });
      Version version = new AssemblyName(Assembly.GetExecutingAssembly().FullName).Version;
      string str = string.Empty;
      Uri result = (Uri) null;
      if (Uri.TryCreate(Assembly.GetExecutingAssembly().CodeBase, UriKind.Absolute, out result))
      {
        if (result.IsFile)
          str = result.LocalPath;
        if (!string.IsNullOrEmpty(str) && File.Exists(str))
        {
          FileVersionInfo versionInfo = FileVersionInfo.GetVersionInfo(str);
          if (versionInfo != null)
            version = new Version(versionInfo.FileVersion);
        }
      }
      this.ToolVersion = string.Format((IFormatProvider) CultureInfo.InvariantCulture, "{0} {1}", new object[2]
      {
        (object) version.ToString(),
        Environment.Is64BitProcess ? (object) "64bit" : (object) "32bit"
      });
    }

    private void BtnCloseClick()
    {
    }

    public void Window_Loaded(object sender, EventArgs e)
    {
    }
  }
}
