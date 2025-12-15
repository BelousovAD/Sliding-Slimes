namespace Audio
{
    using System;
    using UnityEngine;

    [Serializable]
    internal struct Track
    {
        public AudioClipKey Key;
        public AudioClip Clip;
    }
}