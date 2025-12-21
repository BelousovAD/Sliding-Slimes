namespace Audio
{
    using System.Collections;
    using Reflex.Attributes;
    using UnityEngine;

    internal class MusicPlaylist : MonoBehaviour
    {
        private static MusicPlaylist _instance;
        private Audio _audio;

        [Inject]
        private void Initialize(Music music) =>
            _audio = music;
        
        private void Awake()
        {
            if (_instance is null)
            {
                transform.SetParent(null);
                DontDestroyOnLoad(gameObject);
                _instance = this;
            }
            else if (_instance != this)
            {
                Destroy(gameObject);
            }
        }
        
        private void Start () =>
            StartCoroutine(PlayTrack());

        private IEnumerator PlayTrack()
        {
            while (isActiveAndEnabled)
            {
                foreach (AudioClipKey trackKey in _audio.TrackKeys)
                {
                    yield return new WaitForSeconds(_audio.Play(trackKey));
                }
            }
        }
    }
}