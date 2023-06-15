public class SoundPlayer
{
    private SoundPlayerEmitter _emitter;

    public SoundPlayer(SoundPlayerEmitter emitter)
    {
        _emitter = emitter;
    }

    public void PlayMessageSound()
    {
        _emitter.SoundAudioSource.PlayOneShot(_emitter.MessageSoundClip);
    }
}
