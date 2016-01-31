using System;
using System.Collections;
using UnityEngine;

namespace WizardDuel.Spells
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class Bolt : MonoBehaviour
    {
        public Vector3 StartPosition;
        public Vector3 EndPosition;
        public float TravelTime;

        public AudioSource TargetAudio;
        public AudioClip HitShieldSound;
        public AudioClip HitOpponentSound;

        public WizardPlayer Opponent;

        public bool OpponentIsShielded;

        public int InitialAmmo = 2;
        public int CurrentAmmo;

        private SpriteRenderer _renderer;

        private void Awake()
        {
            _renderer = GetComponent<SpriteRenderer>();
            gameObject.SetActive(false);
        }

        public void Fire()
        {
            if (CurrentAmmo <= 0) return;

            CurrentAmmo -= 1;
            gameObject.SetActive(true);
            StartCoroutine(ShootBolt());
        }

        private IEnumerator ShootBolt()
        {
            var startTime = Time.time;
            var endTime = startTime + TravelTime;

            _renderer.enabled = true;

            while (Time.time < endTime)
            {
                var lerp = (Time.time - startTime) / TravelTime;
                var position = Vector3.Lerp(StartPosition, EndPosition, lerp);

                transform.position = position;
                yield return null; 
            }

            if (OpponentIsShielded)
            {
                TargetAudio.clip = HitShieldSound;
                TargetAudio.Play();

                if (HitShield != null) HitShield(this, EventArgs.Empty);
            }
            else
            {
                TargetAudio.clip = HitOpponentSound;
                TargetAudio.Play();

                Opponent.HitBySpell();

                if (HitOpponent != null) HitOpponent(this, EventArgs.Empty);
            }

            _renderer.enabled = false;
            gameObject.SetActive(false);
        }



        public event EventHandler HitShield;
        public event EventHandler HitOpponent;
    }
}
