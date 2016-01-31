using System;
using System.Collections;
using UnityEngine;

namespace WizardDuel.UI
{
    [RequireComponent(typeof(SpriteRenderer))]
    [RequireComponent(typeof(AudioSource))]
    public class Countdown : MonoBehaviour
    {
        public Sprite[] Sprites;
        public AudioClip[] Sounds;
        public float SecondsPerSprite = 1f;

        private SpriteRenderer _renderer;
        private AudioSource _audio;

        private void Awake()
        {
            _renderer = GetComponent<SpriteRenderer>();
            _audio = GetComponent<AudioSource>();
        }

        private void Start()
        {
            _renderer.sprite = null;
        }

        public void StartCountdown()
        {
            StopAllCoroutines();
            StartCoroutine("RunCountdown");
        }

        private IEnumerator RunCountdown()
        {
            for (var i = 0; i < Sprites.Length; i++)
            {
                _renderer.sprite = Sprites[i];
                _audio.clip = Sounds[i];
                _audio.Play();

                yield return new WaitForSeconds(SecondsPerSprite);
            }

            _renderer.sprite = null;

            if (Completed != null)
            {
                Completed(this, EventArgs.Empty);
            }
        }

        public event EventHandler Completed;
    }
}
