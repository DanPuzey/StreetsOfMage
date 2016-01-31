using UnityEngine;

namespace WizardDuel.Animation
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class SimpleSpriteAnimator : MonoBehaviour
    {
        public Sprite[] Sprites;
        public int FramesPerSecond = 4;
        public bool Repeat = true;

        private SpriteRenderer _renderer;
        private int _currentFrameIndex = -1;
        private float _startTime;

        private void Awake()
        {
            _renderer = GetComponent<SpriteRenderer>();
        }

        private void OnEnable()
        {
            _startTime = Time.time;
        }

        private void Start()
        {
            _renderer.sprite = null;
        }

        private void Update()
        {
            if (Sprites.Length == 0)
            {
                _renderer.sprite = null;
            }
            else
            {
                int elapsedFrames = (int)((Time.time - _startTime) * FramesPerSecond);
                int currentFrame = elapsedFrames % Sprites.Length;
                SetCurrentFrame(currentFrame);
            }
        }

        private void SetCurrentFrame(int frame)
        {
            if (frame == _currentFrameIndex) return;

            if (!Repeat && frame == 0 && _currentFrameIndex != -1)
            {
                _currentFrameIndex = -1;
                _renderer.sprite = null;
                enabled = false;
            }
            else
            {
                _currentFrameIndex = frame;
                _renderer.sprite = Sprites[frame];
            }
        }
    }
}
