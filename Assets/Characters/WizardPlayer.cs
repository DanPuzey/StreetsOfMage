using UnityEngine;
using WizardDuel.Animation;
using WizardDuel.Audio;
using WizardDuel.Spells;

namespace WizardDuel
{
    public class WizardPlayer : MonoBehaviour
    {
        [Header("Components")]
        public ComboInput Input;
        public WizardAnimations Animations;
        public SigilAnimations Sigil;
        public WizardAudio Audio;
        public SimpleSpriteAnimator Lightning;
        public LevitateAnimation Levitation;
        public Bolt Bolt;

        [Header("Combos")]
        public ComboMatcher WinningCombo;

        [Header("Settings")]
        public float HitEffectDuration = 0.5f;

        private void Awake()
        {
            Lightning.enabled = false;
        }

        public void HitBySpell()
        {
            BroadcastMessage("ResetCombo");
            Input.enabled = false;
            Invoke("EnableInput", HitEffectDuration);
        }

        private void EnableInput()
        {
            Input.enabled = true;
        }
    }
}
