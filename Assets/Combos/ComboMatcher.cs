using System;
using UnityEngine;

namespace WizardDuel
{
    public class ComboMatcher : MonoBehaviour
    {
        private int[] _combo = { 1, 2, 3, 4 };

        [Header("Behaviour")]
        public bool ResetOnMiss = true;

        private int _currentSymbolIndex = 0;

        public void SetCombo(int[] combo)
        {
            _combo = combo;
            ResetCombo();
        }

        public void AddGlyph(int markIndex)
        {
            if (markIndex == _combo[_currentSymbolIndex])
            {
                _currentSymbolIndex++;
                
                if (_currentSymbolIndex >= _combo.Length)
                {
                    CompleteCombo();
                }
            }
            else if (ResetOnMiss && _currentSymbolIndex > 0)
            {
                Debug.LogFormat("Combo expected {0} and got {1} - resetting", _combo[_currentSymbolIndex], markIndex);

                ResetCombo();

                if (markIndex == _combo[_currentSymbolIndex])
                {
                    _currentSymbolIndex++;
                }
            }
        }

        private void ResetCombo()
        {
            _currentSymbolIndex = 0;
        }

        private void CompleteCombo()
        {
            if (ComboCompleted != null)
            {
                ComboCompleted(this, EventArgs.Empty);
            }

            _currentSymbolIndex = 0;
        }

        public event EventHandler ComboCompleted;
    }
}
