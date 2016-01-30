using UnityEngine;
using WizardDuel.Animation;

namespace WizardDuel
{
    public class WizardPlayer : MonoBehaviour
    {
        [Header("Components")]
        public ComboInput Input;
        public WizardAnimations Animations;
        public SigilAnimations Sigil;

        [Header("Combos")]
        public ComboMatcher WinningCombo;

        public ComboMatcher SpeedCombo;
        public ComboMatcher HealCombo;
        public ComboMatcher IronBarCombo;
        public ComboMatcher KnockBackCombo;
        public ComboMatcher ReviveCombo;
    }
}
