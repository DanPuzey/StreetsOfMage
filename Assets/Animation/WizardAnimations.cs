using System.Collections.Generic;
using UnityEngine;

namespace WizardDuel.Animation
{
    [RequireComponent(typeof(SimpleSpriteAnimator))]
    public class WizardAnimations : MonoBehaviour
    {
        public Sprite[] IdleFrames;
        public List<Sprite[]> GlyphCastFrames;

        private SimpleSpriteAnimator _animator;

        private void Awake()
        {
            _animator = GetComponent<SimpleSpriteAnimator>();
        }

        private void Start()
        {
            _animator.Sprites = IdleFrames;
        }
    }
}
