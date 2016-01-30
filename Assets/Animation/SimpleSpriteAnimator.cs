﻿using UnityEngine;

namespace WizardDuel.Animation
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class SimpleSpriteAnimator : MonoBehaviour
    {
        public Sprite[] Sprites;
        public int FramesPerSecond = 4;

        private SpriteRenderer _renderer;

        private void Awake()
        {
            _renderer = GetComponent<SpriteRenderer>();
        }

        private void Update()
        {
            int elapsedFrames = (int)(Time.time * FramesPerSecond);
            int currentFrame = elapsedFrames % Sprites.Length;
            _renderer.sprite = Sprites[currentFrame];
        }
    }
}
