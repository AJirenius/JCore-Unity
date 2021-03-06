using UnityEngine;
namespace JCore.Tracking
{
    
    public class ErrorTracking : SingletonMonoBehaviour<ErrorTracking>
    {
        public bool sendFromEditor = false;
        public bool sendFromBuild = true;
        
        public string apiKey = "K968eOZlfeJdX1Y22pGT2g";
        public string devApiKey = "";
        
        public int maxBreadcrumbs = 100;

        public Breadcrumbs breadcrumbs;
        public Tags tags;
        public UserCustomData userCustomData;
        
        private static IErrorTrackingClient client;
        private bool enabled = false;
        private string apiKeyInUse;

        protected override void Init()
        {
            if (client == null)
            {

                breadcrumbs = new Breadcrumbs(maxBreadcrumbs);
                tags = new Tags();
                userCustomData = new UserCustomData();
                
#if UNITY_EDITOR
                if (sendFromEditor)
                {
                    enabled = true;
                    
                }
#else
                if (sendFromBuild) enabled = true;
                
#endif
                

#if JCORE_RAYGUN
                client = new Raygun.Raygun();
                Debug.Log("Raygun created as ErrorTracking");
#elif SOMETHING_ELSE

#else
    // client = new ErrorTrackingMock();
#endif
                if (enabled)
                {
                    // TODO: Fix some way of determine DEBUG or RELEASE mode.
                    if (devApiKey != "")
                    {
                        apiKeyInUse = devApiKey;
                    }
                    else
                    {
                        apiKeyInUse = apiKey;
                    }
                    Application.logMessageReceived += Log;
                    client.Init(apiKeyInUse);
                    Debug.Log("ErrorTracking started");
                }
            }
        }

        public void Init(string apiKey)
        {
            // Not implemented as IErrorTracking as it only concerns clients
        }

        public void Log(string condition, string stacktrace, LogType type)
        {
            if (type == LogType.Exception || type == LogType.Error)
            {
                if (client == null) Init();
                if (enabled) client.Log(condition, stacktrace, type, breadcrumbs.AllBreadcrumbs, tags.AllTags, userCustomData.Data);
            }
        }

        public void SetUser(string userId, string userName)
        {
            if (client == null) Init();
            if (enabled) client.SetUser( userId, userName);
        }
    }
}