using System.Collections;
using UnityEngine;

namespace _Scripts.Camera
{
    public class CameraMovement : MonoBehaviour
    {
        [SerializeField] private float _lerpTime;

        private float _bobTimer;

        private Vector3 _initialCameraPosition;


        private void Start()
        {
            _initialCameraPosition = transform.localPosition;
        }

        public void HandleHeadbob(float frequency, float amplitude, bool running)
        {
            if (running)
            {
                _bobTimer += Time.deltaTime * frequency;

                var offsetY = Mathf.Sin(_bobTimer) * amplitude;
                transform.localPosition = _initialCameraPosition + new Vector3(0, offsetY, 0);
            }
            else
            {
                _bobTimer = 0;
                StartCoroutine(LerpToInitialPosition());
                transform.localPosition = _initialCameraPosition;
            }
        }

        private IEnumerator LerpToInitialPosition()
        {
            while (Vector3.Distance(transform.localPosition, _initialCameraPosition) > 0.1f)
            {
                transform.localPosition =
                    Vector3.Lerp(transform.localPosition, _initialCameraPosition, _lerpTime * Time.deltaTime);
                yield return null;
            }
        }
    }
}