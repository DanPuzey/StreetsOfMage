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

        public bool OpponentIsShielded;

        private SpriteRenderer _renderer;

        private void Awake()
        {
            _renderer = GetComponent<SpriteRenderer>();
            _renderer.enabled = false;
        }

        public void Fire()
        {
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
                Debug.Break();
                yield return null; 
            }

            if (OpponentIsShielded)
            {
                if (HitShield != null) HitShield(this, EventArgs.Empty);
            }
            else
            {
                if (HitOpponent != null) HitOpponent(this, EventArgs.Empty);
            }

            _renderer.enabled = false;
        }

        public event EventHandler HitShield;
        public event EventHandler HitOpponent;
    }
}
