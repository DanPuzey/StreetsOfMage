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
        public AudioSource MusicAudio;

        private int[] _wizardScores;

        private void Awake()
        {
            _wizardScores = new int[Wizards.Length];

            foreach (var w in Wizards)
            {
                w.WinningCombo.ComboCompleted += PlayerWins;
            }
        }

        private void Start()
        {
            SetNewWinningCombo();
        }

        private void PlayerWins(object sender, EventArgs e)
        {
            var c = sender as ComboMatcher;
            var winnerTransform = c.transform.parent.parent;
            var winner = winnerTransform.GetComponent<WizardPlayer>();

            var index = Array.IndexOf(Wizards, winner);
            _wizardScores[index] += 1;

            if (_wizardScores[index] < 4)
            {
                winner.Sigil.SetStage(_wizardScores[index]);
                SetNewWinningCombo();
            }
            else
            {
                SetVictor(winner);
            }
        }

        private void SpellCast(object sender, EventArgs e)
        {
            var c = sender as ComboMatcher;
            var w = c.transform.parent.parent;
            Debug.LogFormat("{0} casts spell", w.name);
        }

        private void SetVictor(WizardPlayer victor)
        {
            foreach (var wiz in Wizards)
            {
                wiz.Input.enabled = false;
            
                if (wiz == victor)
                {
                    wiz.Animations.PlayWinAnim();
                }
                else
                {
                    wiz.Animations.PlayLoseAnim();
                    wiz.Audio.PlayDeath();
                    wiz.Sigil.SetStage(0);
                }
            }

            MusicAudio.Stop();
        }

        private void SetNewWinningCombo()
        {
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
    }
}
