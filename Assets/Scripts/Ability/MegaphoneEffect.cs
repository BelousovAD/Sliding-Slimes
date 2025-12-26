using AssetKits.ParticleImage;
using Reflex.Attributes;
using UnityEngine;

namespace Ability
{
    [RequireComponent(typeof(ParticleImage))]
    internal class MegaphoneEffect : MonoBehaviour
    {
        private ParticleImage _particleImage;
        private Ability _ability;

        [Inject]
        private void Initialize(Megaphone megaphone) =>
            _ability = megaphone;

        private void Awake() =>
            _particleImage = GetComponent<ParticleImage>();

        private void OnEnable() =>
            _ability.Used += PlayParticles;

        private void OnDisable() =>
            _ability.Used -= PlayParticles;

        private void PlayParticles() =>
            _particleImage.Play();
    }
}