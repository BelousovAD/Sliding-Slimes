namespace Map
{
    internal class TravelatorProvider : AbstractProvider<Travelator>
    {
        public new Travelator Model => base.Model as Travelator;
    }
}