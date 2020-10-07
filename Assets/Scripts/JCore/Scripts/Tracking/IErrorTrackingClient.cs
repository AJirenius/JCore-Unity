using UnityEngine;

namespace JCore.Tracking
{
    public interface IErrorTrackingClient
    {
        void Init(string apiKey);
        void Log(string condition, string stacktrace, LogType type);
        void SetUser(string userId, string userName);
    }
}