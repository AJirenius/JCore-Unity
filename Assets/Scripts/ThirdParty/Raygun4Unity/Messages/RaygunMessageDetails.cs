﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using JCore.Tracking;

namespace Mindscape.Raygun4Unity.Messages
{
  public class RaygunMessageDetails
  {
    public string MachineName { get; set; }

    public string Version { get; set; }

    public RaygunErrorMessage Error { get; set; }

    public RaygunEnvironmentMessage Environment { get; set; }

    public RaygunClientMessage Client { get; set; }

    public IList<string> Tags { get; set; }

    public IDictionary UserCustomData { get; set; }

    public RaygunIdentifierMessage User { get; set; }

    public IList<Breadcrumb> BreadCrumbs { get; set; }
    
  }
}
