using System;
using UnityEngine;

namespace WizardDuel
{
    public class ComboMatcher : MonoBehaviour
    {
        [Header("Combo setup")]
        public int AlphabetSize = 8;
        public string[] Alphabet = { "A", "B", "X", "Y", "up", "down", "left", "right" };

        public int[] Combo = { 1, 2, 3, 4 };

        [Header("Behaviour")]
        public bool ResetOnMiss = true;

        private int _currentSymbolIndex = 0;

        public void AddGlyph(int markIndex)
        {
            if (markIndex == _currentSymbolIndex)
            {
                _currentSymbolIndex++;
                
                if (_currentSymbolIndex >= Combo.Length)
                {
                    CompleteCombo();
                }
            }
            else
            {
                if (ResetOnMiss)
                {
                    Reset();
                }
            }
        }

        public void Reset()
        {
            _currentSymbolIndex = 0;
            Debug.Log("Combo reset");
        }

        private void CompleteCombo()
        {
            Debug.LogFormat("Combo complete!", Combo);

            if (ComboCompleted != null)
            {
                ComboCompleted(this, EventArgs.Empty);
            }

            _currentSymbolIndex = 0;
        }

        public event EventHandler ComboCompleted;
    }
}
