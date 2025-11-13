namespace Window
{
    internal interface IWindowService
    {
        public void CloseCurrent();
        
        public void Open(string id, bool needCloseCurrent);
    }
}