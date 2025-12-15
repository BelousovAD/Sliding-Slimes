namespace Audio
{
    using System.Collections.Generic;
    using UnityEngine.Audio;

    internal class Sound : Audio
    {
        public Sound(AudioMixerGroup group, AudioSourceSpawner spawner, IEnumerable<Track> tracks)
            : base(AudioType.Sound, group, spawner, tracks)
        { }
    }
}