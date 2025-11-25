namespace Map
{
    using System;

    internal class PortalView : AbstractView<AbstractModel>
    {
        private Portal _model;
        
        public override void Initialize(AbstractModel model)
        {
            if (model is not Portal portal)
            {
                throw new ArgumentException($"Must inherit {nameof(Portal)}", nameof(model));
            }
            
            _model = portal;
        }
    }
}