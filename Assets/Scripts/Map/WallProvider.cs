namespace Map
{
    internal class WallProvider : AbstractProvider<Wall>
    {
        public new Wall Model => base.Model as Wall;
    }
}