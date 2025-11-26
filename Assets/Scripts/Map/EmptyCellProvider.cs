namespace Map
{
    internal class EmptyCellProvider : AbstractProvider<EmptyCell>
    {
        public new EmptyCell Model => base.Model as EmptyCell;
    }
}