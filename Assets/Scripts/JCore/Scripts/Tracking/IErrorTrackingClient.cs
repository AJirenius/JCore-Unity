using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JCore.Tracking
{
    public interface IErrorTrackingClient
    {
        void Init(string apiKey);
        void Log(string condition, string stacktrace, LogType type, IList<Breadcrumb> breadcrumbs, IList<string> tags);
        void SetUser(string userId, string userName);
    }
}