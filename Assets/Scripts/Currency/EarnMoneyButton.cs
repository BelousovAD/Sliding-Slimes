using Common;
using Reflex.Attributes;
using UnityEngine;

namespace Currency
{
    internal class EarnMoneyButton : AbstractButton
    {
        [SerializeField][Min(0)] private int _amount;

        private Money _money;

        [Inject]
        private void Initialize(Money money) =>
            _money = money;

        protected override void HandleClick() =>
            _money.Earn(_amount);
    }
}