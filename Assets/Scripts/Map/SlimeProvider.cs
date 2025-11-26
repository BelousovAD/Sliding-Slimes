namespace Map
{
    internal class SlimeProvider : AbstractProvider<Slime>
    {
        public new Slime Model => base.Model as Slime;
    }
}