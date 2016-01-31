using System;
using System.Collections;
using UnityEngine;

namespace WizardDuel.UI
{
    public class ReadyScreen: MonoBehaviour
    {
        public SpriteRenderer[] TitleRenderers;
        public GameObject[] Prompts;
        public AudioSource Audio;

        public float FadeTime = 2f;
        public float AudioFadeTime = 2f;

        private bool _waitingForKey = false;
        private float _alpha;
        private float _audioInitialVolume;
        private bool _isFading;

        private void Awake()
        {
            gameObject.SetActive(false);
            _audioInitialVolume = Audio.volume;
        }

        private void Update()
        {
            if (_waitingForKey && Input.GetButtonDown("Fire1"))
            {
                _waitingForKey = false;
                SetPromptsEnabled(false);

                StartFade(false);
            }

            ProcessFade();
        }

        public void Show()
        {
            gameObject.SetActive(true);

            Audio.volume = 1;
            Audio.Play();

            _waitingForKey = true;
            StartFade(true);
            SetPromptsEnabled(true);
        }

        private void StartFade(bool up)
        {
            _alpha = up ? 0 : 1;
            _isFading = true;
        }

        private void ProcessFade()
        {
            if (_isFading)
            {
                var lerp = Time.deltaTime / FadeTime;

                if (!_waitingForKey)
                {
                    lerp *= -1;
                }

                _alpha += lerp;

                if (_alpha < 0)
                {
                    _alpha = 0;
                    _isFading = false;

                    if (Readied != null)
                    {
                        Readied(this, EventArgs.Empty);
                        StartCoroutine(FadeAudio());
                    }
                }
                else if (_alpha > 1)
                {
                    _alpha = 1;
                    _isFading = false;
                }

                foreach (var r in TitleRenderers)
                {
                    var color = new Color(r.color.r, r.color.g, r.color.b, _alpha);
                    r.color = color;
                }
            }
        }

        private IEnumerator FadeAudio()
        {
            var startTime = Time.time;
            var endTime = startTime + AudioFadeTime;

            while (Time.time < endTime)
            {
                var delta = Time.time - startTime;
                var normalisedVolume = (AudioFadeTime - delta) / AudioFadeTime;
                Audio.volume = normalisedVolume * _audioInitialVolume;
                yield return null;
            }

            Audio.volume = 0;
        }

        private void SetPromptsEnabled(bool enabled)
        {
            foreach (var r in Prompts)
            {
                r.SetActive(enabled);
            }
        }

        public event EventHandler Readied;
    }
}
