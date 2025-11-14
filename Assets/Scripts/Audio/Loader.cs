namespace Audio
{
    using Bootstrap;
    using Reflex.Attributes;using UnityEngine;

    public class Loader : MonoBehaviour, ILoadable
    {
        private Audio _music;
        private Audio _sound;

        [Inject]
        private void Initialize(Music music, Sound sound)
        {
            _music = music;
            _sound = sound;
        }
        
        public void Load()
        {
            _music.Load();
            _sound.Load();
        }
    }
}