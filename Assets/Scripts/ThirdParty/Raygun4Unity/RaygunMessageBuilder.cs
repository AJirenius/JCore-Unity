using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using JCore.Tracking;
using JCore.Tracking.Raygun;
using Mindscape.Raygun4Unity.Messages;

namespace Mindscape.Raygun4Unity
{
  public class RaygunMessageBuilder : IRaygunMessageBuilder
  {
    public static RaygunMessageBuilder New
    {
      get
      {
        return new RaygunMessageBuilder();
      }
    }

    private readonly RaygunMessage _raygunMessage;

    private RaygunMessageBuilder()
    {
      _raygunMessage = new RaygunMessage();
    }

    public RaygunMessage Build()
    {
      return _raygunMessage;
    }

    public IRaygunMessageBuilder SetMachineName(string machineName)
    {
      _raygunMessage.Details.machineName = machineName;
      return this;
    }

    public IRaygunMessageBuilder SetEnvironmentDetails()
    {
      _raygunMessage.Details.environment = new RaygunEnvironmentMessage();
      return this;
    }

    public IRaygunMessageBuilder SetExceptionDetails(string message, string stackTrace)
    {
      _raygunMessage.Details.error = new RaygunErrorMessage(message, stackTrace);
      return this;
    }

    public IRaygunMessageBuilder SetExceptionDetails(Exception exception)
    {
      _raygunMessage.Details.error = new RaygunErrorMessage(exception);
      return this;
    }

    public IRaygunMessageBuilder SetClientDetails()
    {
      _raygunMessage.Details.client = new RaygunClientMessage();
      return this;
    }

    public IRaygunMessageBuilder SetUserCustomData(IDictionary userCustomData)
    {
      _raygunMessage.Details.userCustomData = userCustomData;
      return this;
    }

    public IRaygunMessageBuilder SetTags(IList<string> tags)
    {
      _raygunMessage.Details.tags = tags;
      return this;
    }

    public IRaygunMessageBuilder SetUser(RaygunIdentifierMessage user)
    {
      _raygunMessage.Details.user = user;
      return this;
    }

    public IRaygunMessageBuilder SetVersion(string version)
    {
      if (!String.IsNullOrEmpty(version))
      {
        _raygunMessage.Details.version = version;
      }
      else
      {
        _raygunMessage.Details.version = "Not supplied";
      }
      return this;
    }

    public IRaygunMessageBuilder SetBreadcrumbs(IList<RaygunBreadcrumb> breadcrumbs)
    {
      _raygunMessage.Details.breadcrumbs = breadcrumbs;
      return this;
    }
  }
}
