using UnityEngine;

namespace WizardDuel.Spells
{
    public class ShieldInput : MonoBehaviour
    {
        public SpriteRenderer ShieldRenderer;
        public Bolt OpponentBolt;

        public string ButtonName = "joystick 1 button 5";
        public float ShieldDuration = 1f;
        public float SecondsBetweenShots = 2f;

        private float _nextShotAllowedTime = 0;

        private void Awake()
        {
            ShieldRenderer.gameObject.SetActive(false);
        }

        private void Update()
        {
            if (Input.GetKeyDown(ButtonName) && _nextShotAllowedTime < Time.time)
            {
                ActivateShield();
            }
        }

        private void ActivateShield()
        {
            _nextShotAllowedTime = Time.time + SecondsBetweenShots + ShieldDuration;
            ShieldRenderer.gameObject.SetActive(true);
            OpponentBolt.OpponentIsShielded = true;

            Invoke("DeactivateShield", ShieldDuration);
        }

        private void DeactivateShield()
        {
            ShieldRenderer.gameObject.SetActive(false);
            OpponentBolt.OpponentIsShielded = false;
        }
    }
}
