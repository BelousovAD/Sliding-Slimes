namespace Audio
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using Random = UnityEngine.Random;

    [RequireComponent(typeof(AudioSource))]
    internal class MusicSource : MonoBehaviour
    {
        [SerializeField] private bool _playRandom;
        [SerializeField] private List<AudioClip> _playlist = new();

        private static MusicSource _instance;
        private AudioSource _audioSource;
        private int _index = -1;

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
                return;
            }

            _audioSource = GetComponent<AudioSource>();
        }

        private void Start () =>
            StartCoroutine(PlayTrack());

        private void UpdateIndex()
        {
            if (_playRandom)
            {
                _index = Random.Range(0, _playlist.Count);
            }
            else
            {
                _index = ++_index % _playlist.Count;
            }
        }

        private IEnumerator PlayTrack()
        {
            while (isActiveAndEnabled)
            {
                UpdateIndex();
                _audioSource.clip = _playlist[_index];
                _audioSource.Play();
                
                yield return new WaitForSeconds(_audioSource.clip.length);
            }
        }
    }
}