using UnityEngine;
using WizardDuel.Animation;
using WizardDuel.Audio;

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

        [Header("Combos")]
        public ComboMatcher WinningCombo;

        private void Awake()
        {
            Lightning.enabled = false;
        }
    }
}
