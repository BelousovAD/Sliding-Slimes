namespace Audio
{
    using Spawn;

    internal class AudioSourceSpawner : SiblingsSpawner
    {
        public new PooledAudioSource Spawn() =>
            base.Spawn().GetComponent<PooledAudioSource>();
    }
}