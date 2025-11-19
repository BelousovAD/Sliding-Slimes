namespace Mediation
{
    using Currency;
    using Reflex.Attributes;
    using UnityEngine;
    using UnityEngine.SceneManagement;

    internal class MoneyRewardedAdButton : AbstractRewardedAdButton
    {
        [SerializeField, Min(0)] private int _amount;
        [SerializeField] private string _sceneToLoad;
        
        private Money _money;
        
        [Inject]
        private void Initialize(Money money) =>
            _money = money;
        
        protected override void HandleComplete()
        {
            _money.Earn(_amount);
            SceneManager.LoadScene(_sceneToLoad);
        }
    }
}