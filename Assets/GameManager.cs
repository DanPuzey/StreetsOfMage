﻿using System;
using UnityEngine;
using WizardDuel.UI;

namespace WizardDuel
{
    public class GameManager : MonoBehaviour
    {
        [Header("Game settings")]
        public bool RandomizeSpells = false;
        public int AlphabetSize = 4;
        public int WinningComboMinSize = 6;
        public int WinningComboMaxSize = 16;

        [Header("Component references")]
        public WizardPlayer[] Wizards = new WizardPlayer[0];
        public ComboDisplay WinningComboDisplay;
        public AudioSource MusicAudio;
        public AudioSource EndGameSound;
        public Countdown Countdown;

        private int[] _wizardScores;

        private void Awake()
        {
            _wizardScores = new int[Wizards.Length];

            foreach (var w in Wizards)
            {
                w.WinningCombo.ComboCompleted += PlayerWins;
            }

            Countdown.Completed += CountdownCompleted;
        }

        private void Start()
        {
            BeginCountdown();
        }

        private void BeginCountdown()
        {
            SetWizardInputEnabled(false);

            foreach (var w in Wizards)
            {
                w.Levitation.StartAnimation();
            }

            Countdown.StartCountdown();
            MusicAudio.Stop();
        }

        private void CountdownCompleted(object sender, EventArgs e)
        {
            SetNewWinningCombo();
            SetWizardInputEnabled(true);
            MusicAudio.Play();
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
            SetWizardInputEnabled(false);

            foreach (var wiz in Wizards)
            {
                if (wiz == victor)
                {
                    wiz.Animations.PlayWinAnim();
                }
                else
                {
                    wiz.Animations.PlayLoseAnim();
                    wiz.Audio.PlayDeath();
                    wiz.Sigil.SetStage(0);
                    wiz.Lightning.enabled = true;
                }
            }

            MusicAudio.Stop();
            EndGameSound.Play();
        }

        private void SetNewWinningCombo()
        {
            var comboSize = UnityEngine.Random.Range(WinningComboMinSize, WinningComboMaxSize);
            var winningCombo = new int[comboSize];

            for (var i = 0; i < comboSize; i++)
            {
                winningCombo[i] = UnityEngine.Random.Range(0, AlphabetSize);
            }

            foreach (var w in Wizards)
            {
                w.WinningCombo.SetCombo(winningCombo);
            }

            WinningComboDisplay.Combo = winningCombo;
            WinningComboDisplay.DrawCombo();
        }

        private void SetWizardInputEnabled(bool enabled)
        {
            foreach (var w in Wizards)
            {
                w.Input.enabled = enabled;
            }
        }
    }
}
