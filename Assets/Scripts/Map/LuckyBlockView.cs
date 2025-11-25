namespace Map
{
    using System;

    internal class LuckyBlockView : AbstractView<AbstractModel>
    {
        private LuckyBlock _model;
        
        public override void Initialize(AbstractModel model)
        {
            if (model is not LuckyBlock luckyBlock)
            {
                throw new ArgumentException($"Must inherit {nameof(LuckyBlock)}", nameof(model));
            }
            
            _model = luckyBlock;
        }
    }
}