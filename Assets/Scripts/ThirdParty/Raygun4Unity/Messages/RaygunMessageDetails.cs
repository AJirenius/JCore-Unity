using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using JCore.Tracking;
using JCore.Tracking.Raygun;

namespace Mindscape.Raygun4Unity.Messages
{
  public class RaygunMessageDetails
  {
    public string machineName { get; set; }

    public string version { get; set; }

    public RaygunErrorMessage error { get; set; }

    public RaygunEnvironmentMessage environment { get; set; }

    public RaygunClientMessage client { get; set; }

    public IList<string> tags { get; set; }

    public IDictionary userCustomData { get; set; }

    public RaygunIdentifierMessage user { get; set; }

    public IList<RaygunBreadcrumb> breadcrumbs { get; set; }
    
  }
}
