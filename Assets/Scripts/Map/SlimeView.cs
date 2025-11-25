namespace Map
{
    using System;

    internal class SlimeView : AbstractView<AbstractModel>
    {
        private Slime _model;
        
        public override void Initialize(AbstractModel model)
        {
            if (model is not Slime slime)
            {
                throw new ArgumentException($"Must inherit {nameof(Slime)}", nameof(model));
            }
            
            _model = slime;
        }
    }
}