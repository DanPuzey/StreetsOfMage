using System.Collections;
using UnityEngine;

namespace WizardDuel.Animation
{
    public class LevitateAnimation : MonoBehaviour
    {
        public Transform RootTransform;
        public Transform ShadowTransform;

        public Vector2 LevitateStartPosition;

        public float Duration = 3f;

        private Vector3 _targetPosition;

        private void Awake()
        {
            _targetPosition = transform.position;
            ShadowTransform.gameObject.SetActive(false);
        }

        public void StartAnimation()
        {
            StartCoroutine(Animate());
        }

        private IEnumerator Animate()
        {
            var startTime = Time.time;
            var dropTime = startTime + Duration - 0.3f;
            var slideDuration = dropTime - startTime;
            var dropStartPoint = new Vector3(_targetPosition.x, LevitateStartPosition.y, 0);
            var animator = GetComponent<WizardPlayer>().Animations;

            animator.PlayLevitateAnim();

            transform.position = LevitateStartPosition;
            ShadowTransform.gameObject.SetActive(true);
            ShadowTransform.localPosition = new Vector3(0, _targetPosition.y - LevitateStartPosition.y, 0);

            yield return null;

            while (Time.time < dropTime)
            {
                var deltaTime = Time.time - startTime;
                var lerp = deltaTime / slideDuration;
                var newPos = Vector3.Lerp(LevitateStartPosition, dropStartPoint, lerp);
                transform.position = newPos;
                yield return null;
            }

            transform.position = dropStartPoint;
            yield return new WaitForSeconds(0.1f);

            var endTime = startTime + Duration;
            startTime = Time.time;
            var dropDuration = endTime - startTime;

            while (Time.time < endTime)
            {
                var deltaTime = Time.time - startTime;
                var lerp = deltaTime / dropDuration;

                var newPos = Vector3.Lerp(dropStartPoint, _targetPosition, lerp);
                transform.position = newPos;
                ShadowTransform.localPosition = new Vector3(0, _targetPosition.y - newPos.y, 0);
                yield return null;
            }

            ShadowTransform.gameObject.SetActive(false);
            transform.position = _targetPosition;
            animator.StartIdle();
        }
    }
}
