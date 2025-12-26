using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Reflex.Attributes;
using UnityEngine;

namespace Audio
{
    internal class MusicPlaylist : MonoBehaviour
    {
        private const AudioType MusicType = AudioType.Music;

        private static MusicPlaylist _instance;
        private Audio _audio;

        [Inject]
        private void Initialize(IEnumerable<Audio> audios) =>
            _audio = audios.FirstOrDefault(audioObject => audioObject.Type == MusicType);

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

        private void Start() =>
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