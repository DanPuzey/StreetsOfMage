using UnityEngine;

namespace WizardDuel.Spells
{
    public class BoltInput : MonoBehaviour
    {
        public Bolt Bolt;

        public string ButtonName = "joystick 1 button 4";
        public float SecondsBetweenShots = 2f;

        private float _nextShotAllowedTime = 0;

        private void Update()
        {
            if (Input.GetKeyDown(ButtonName) && _nextShotAllowedTime < Time.time)
            {
                _nextShotAllowedTime = Time.time + SecondsBetweenShots;
                Bolt.Fire();
            }
        }
    }
}
