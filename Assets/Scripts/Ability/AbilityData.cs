namespace Ability
{
    using UnityEngine;

    [CreateAssetMenu(fileName = nameof(AbilityData), menuName = nameof(Ability) + "/" + nameof(AbilityData))]
    public class AbilityData : ScriptableObject
    {
        [SerializeField] private Sprite _icon;
        [SerializeField] private bool _isRewardForAd;
        [SerializeField, Min(0)] private int _price;
        [SerializeField] private string _saveKey;
        [SerializeField, Min(0)] private int _startCount;
        [SerializeField, Min(1)] private int _unlockLevel = 1;

        public Sprite Icon => _icon;

        public bool IsRewardForAd => _isRewardForAd;

        public int Price => _price;

        public string SaveKey => _saveKey;

        public int StartCount => _startCount;

        public int UnlockLevel => _unlockLevel;
    }
}