namespace Map
{
    using System;

    internal class TravelatorView : AbstractView<AbstractModel>
    {
        private Travelator _model;
        
        public override void Initialize(AbstractModel model)
        {
            if (model is not Travelator travelator)
            {
                throw new ArgumentException($"Must inherit {nameof(Travelator)}", nameof(model));
            }
            
            _model = travelator;
        }
    }
}