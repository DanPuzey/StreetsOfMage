using System;
using UnityEngine;
using WizardDuel.UI;

namespace WizardDuel
{
    public class GameManager : MonoBehaviour
    {
        [Header("Game settings")]
        public bool RandomizeSpells = false;
        public int AlphabetSize = 4;
        public int WinningComboSize = 10;

        [Header("Component references")]
        public WizardPlayer[] Wizards = new WizardPlayer[0];
        public ComboDisplay WinningComboDisplay;

        private void Awake()
        {
            // wire up event handlers
            foreach (var w in Wizards)
            {
                w.WinningCombo.ComboCompleted += PlayerWins;
            }
        }

        private void Start()
        {
            // randomise combos
            var winningCombo = new int[WinningComboSize];

            for (var i = 0; i < WinningComboSize; i++)
            {
                winningCombo[i] = UnityEngine.Random.Range(0, AlphabetSize);
            }

            foreach (var w in Wizards)
            {
                w.WinningCombo.Combo = winningCombo;
            }

            WinningComboDisplay.Combo = winningCombo;
            WinningComboDisplay.DrawCombo();
        }

        private void PlayerWins(object sender, EventArgs e)
        {
            var c = sender as ComboMatcher;
            var w = c.transform.parent.parent;
            Debug.LogFormat("WINNER! {0}", w.name);
        }

        private void SpellCast(object sender, EventArgs e)
        {
            var c = sender as ComboMatcher;
            var w = c.transform.parent.parent;
            Debug.LogFormat("{0} casts spell", w.name);
        }
    }
}
