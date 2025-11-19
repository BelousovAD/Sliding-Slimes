namespace Window
{
    public interface IWindowService
    {
        public void CloseCurrent();
        
        public void Open(string id, bool needCloseCurrent);
    }
}