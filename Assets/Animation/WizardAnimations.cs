using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WizardDuel.Animation
{
    [RequireComponent(typeof(SimpleSpriteAnimator))]
    public class WizardAnimations : MonoBehaviour
    {
        public Sprite[] IdleFrames;
        public AnimFrames[] GlyphCastFrames;
        public Sprite[] WinFrames;
        public Sprite[] LoseFrames;
        public Sprite[] LevitateFrames;

        private SimpleSpriteAnimator _animator;
        private bool _animLocked = false;

        #region MonoBehaviour methods
        private void Awake()
        {
            _animator = GetComponent<SimpleSpriteAnimator>();
        }

        private void Start()
        {
            StartIdle();
        } 
        #endregion

        public void PlayWinAnim()
        {
            StopAllCoroutines();
            _animLocked = true;
            _animator.SetSprites(WinFrames);
        }

        public void PlayLoseAnim()
        {
            StopAllCoroutines();
            _animLocked = true;
            _animator.SetSprites(LoseFrames);
        }

        public void PlayLevitateAnim()
        {
            StopAllCoroutines();
            _animator.SetSprites(LevitateFrames);
        }

        public void StartIdle()
        {
            _animator.SetSprites(IdleFrames);
        }

        private void AddGlyph(int markIndex)
        {
            if (_animLocked)
            {
                return;
            }

            StopAllCoroutines();
            StartCoroutine("ShowGlyphAnim", markIndex);
        }

        private IEnumerator ShowGlyphAnim(int index)
        {
            _animator.SetSprites(GlyphCastFrames[index].Frames);
            yield return new WaitForSeconds(0.8f);
            _animator.SetSprites(IdleFrames);
        }
    }
}
