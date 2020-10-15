using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Mindscape.Raygun4Unity.Messages
{
  public class RaygunClientMessage
  {
    public RaygunClientMessage()
    {
      name = "Raygun4Unity-JCore";
      version = "0.1.0.0";
      clientUrl = @"https://github.com/MindscapeHQ/raygun4unity";
    }

    public string name { get; set; }

    public string version { get; set; }

    public string clientUrl { get; set; }
  }
}
