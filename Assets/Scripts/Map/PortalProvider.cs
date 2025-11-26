namespace Map
{
    internal class PortalProvider : AbstractProvider<Portal>
    {
        public new Portal Model => base.Model as Portal;
    }
}