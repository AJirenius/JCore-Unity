using System.Collections.Generic;
using UnityEngine;
using Mindscape.Raygun4Unity;
using Mindscape.Raygun4Unity.Messages;

namespace JCore.Tracking.Raygun
{
    public class Raygun : IErrorTrackingClient
    {
        private RaygunClient _client;
        
        public void Init(string apikey)
        {
            _client = new RaygunClient(apikey);
            _client.ApplicationVersion = Application.version;
        }
        
        public void Log(string condition, string stacktrace, LogType type, IList<Breadcrumb> breadcrumbs, IList<string> tags, Dictionary<string, string> userCustomData)
        {
            Debug.Log("Will send:"+type);
            
            // convert Breadcrumbs to Raygun format
            IList<RaygunBreadcrumb> rgBreadcrumbs = new List<RaygunBreadcrumb>();
            foreach (Breadcrumb bc in breadcrumbs)
            {
                rgBreadcrumbs.Add(new RaygunBreadcrumb(bc));
            }
            
            _client.Send(condition, stacktrace, rgBreadcrumbs, tags, userCustomData);
        }
        
        public void SetUser(string userId, string userName)
        {
            RaygunIdentifierMessage msg = new RaygunIdentifierMessage(userId);
            msg.identifier = userId;
            msg.fullName = userName;
            
            _client.UserInfo = msg;
            _client.User = userName;
        }
    }
}