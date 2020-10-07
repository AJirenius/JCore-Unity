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
        
        public void Log(string condition, string stacktrace, LogType type)
        {
            Debug.Log("Will send:"+type);
            if (type == LogType.Exception || type == LogType.Error)
            {
                _client.Send(condition, stacktrace);
            }
        }
        
        public void SetUser(string userId, string userName)
        {
            RaygunIdentifierMessage msg = new RaygunIdentifierMessage(userId);
            msg.Identifier = userId;
            msg.FullName = userName;
            
            _client.UserInfo = msg;
            _client.User = userName;
        }

        public void SetBreadCrumb()
        {
        }
    }
}