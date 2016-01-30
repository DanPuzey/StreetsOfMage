using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WizardDuel.Animation
{
    [RequireComponent(typeof(SimpleSpriteAnimator))]
    public class WizardAnimations : MonoBehaviour
    {
        [Serializable]
        public struct AnimFrames
        {
            public Sprite[] Frames;
        }

        public Sprite[] IdleFrames;
        public AnimFrames[] GlyphCastFrames;
        public Sprite[] WinFrames;
        public Sprite[] LoseFrames;

        private SimpleSpriteAnimator _animator;

        #region MonoBehaviour methods
        private void Awake()
        {
            _animator = GetComponent<SimpleSpriteAnimator>();
        }

        private void Start()
        {
            _animator.Sprites = IdleFrames;
        } 
        #endregion

        public void PlayWinAnim()
        {
            _animator.Sprites = WinFrames;
        }

        public void PlayLoseAnim()
        {
            _animator.Sprites = LoseFrames;
        }

        private void AddGlyph(int markIndex)
        {
            StartCoroutine(ShowGlyphAnim(markIndex));
        }

        private IEnumerator ShowGlyphAnim(int index)
        {
            _animator.Sprites = GlyphCastFrames[index].Frames;
            yield return new WaitForSeconds(0.8f);
            _animator.Sprites = IdleFrames;
        }
    }
}
