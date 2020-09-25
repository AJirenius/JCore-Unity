namespace JCore.UI
{
    public class Panel<T> : APanel where T : AViewParams
    {
        public T param;
        public override void SetParams(AViewParams param)
        {
            this.param = param as T;
        }
    }
}