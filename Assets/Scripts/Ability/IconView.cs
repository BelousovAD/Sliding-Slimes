namespace Ability
{
    using UnityEngine;
    using UnityEngine.UI;

    [RequireComponent(typeof(Image))]
    public class IconView : MonoBehaviour
    {
        [SerializeField] private AbilityProvider _abilityProvider;
        
        private Image _image;

        private void Awake() =>
            _image = GetComponent<Image>();

        private void Start() =>
            _image.sprite = _abilityProvider.Ability.Icon;
    }
}