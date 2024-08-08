using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Code.Components.Cameras
{
    public class CameraShaker : MonoBehaviour
    {
        [SerializeField] private float _minDuration = 0.5f;
        [SerializeField] private float _maxDuration = 0.75f;
        [SerializeField] private float _mimMagnitude = 0.7f;
        [SerializeField] private float _maxMagnitude = 1.3f;
        private Vector3 _originalPosition;
        private Coroutine _coroutine;
        
        private void Awake()
        {
            _originalPosition = transform.localPosition;
        }
        
        public void Shake()
        {
            if (_coroutine != null)
            {
                StopCoroutine(_coroutine);
            }
            
            _coroutine = StartCoroutine(ShakeCoroutine());
        }

        private IEnumerator ShakeCoroutine()
        {
            float elapsed = 0f;

            var magnitude = Random.Range(_mimMagnitude, _maxMagnitude);
            var duration = Random.Range(_minDuration, _maxDuration);
            
            while (elapsed < duration)
            {
                float x = Random.Range(-1f, 1f) * magnitude;
                float y = Random.Range(-1f, 1f) * magnitude;

                transform.localPosition = _originalPosition + new Vector3(x, y, 0);

                elapsed += Time.deltaTime;

                yield return null;
            }

            transform.localPosition = _originalPosition;
        }
    }
}