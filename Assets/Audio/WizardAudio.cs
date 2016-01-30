using UnityEngine;

namespace WizardDuel.Audio
{
    [RequireComponent(typeof(AudioSource))]
    public class WizardAudio : MonoBehaviour
    {
        public AudioClip[] GlyphSounds;
        public AudioClip[] DeathSounds;
        public AudioClip[] PainSounds;

        private AudioSource _audio;

        private void Awake()
        {
            _audio = GetComponent<AudioSource>();
        }

        public void PlayDeath()
        {
            PlayRandomSoundFromList(DeathSounds);
        }

        public void PlayPain()
        {
            PlayRandomSoundFromList(PainSounds);
        }

        private void PlayRandomSoundFromList(AudioClip[] sounds)
        {
            var index = Random.Range(0, sounds.Length);
            var clip = sounds[index];
            PlayClip(clip);
        }

        private void AddGlyph(int markIndex)
        {
            var clip = GlyphSounds[markIndex];
            PlayClip(clip);
        }

        private void PlayClip(AudioClip clip)
        {
            _audio.clip = clip;
            _audio.Play();
        }
    }
}
