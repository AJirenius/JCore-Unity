namespace JCore.Tracking.Raygun
{
    public struct RaygunBreadcrumb
    {
        public long timeStamp;
        public string type;
        public string level;
        public string category; 
        public string message;

        public RaygunBreadcrumb(Breadcrumb bc)
        {
            this.timeStamp = bc.timeStamp-2000;
            this.type = bc.type.ToString().ToLower();
            this.level = "info";
            this.category = bc.context;
            this.message = bc.longMessage;
        }
    }
}