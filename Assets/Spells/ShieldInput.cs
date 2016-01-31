using UnityEngine;

namespace WizardDuel.Spells
{
    public class ShieldInput : MonoBehaviour
    {
        public SpriteRenderer ShieldRenderer;
        public Bolt OpponentBolt;

        private void Awake()
        {
            ShieldRenderer.enabled = false;
        }
    }
}
