using UnityEngine;

namespace WizardDuel.Animation
{
    [RequireComponent(typeof(SimpleSpriteAnimator))]
    public class SigilAnimations : MonoBehaviour
    {
        public AnimFrames[] Stages;

        private int _currentStage;
        private SimpleSpriteAnimator _animator;

        #region MonoBehaviour methods
        private void Awake()
        {
            _animator = GetComponent<SimpleSpriteAnimator>();
        }

        private void Start()
        {
            SetCurrentStage();
        }
        #endregion

        public void SetStage(int number)
        {
            _currentStage = number;
            SetCurrentStage();
        }

        private void SetCurrentStage()
        { 
            _animator.Sprites = Stages[_currentStage].Frames;
        }
    }
}
