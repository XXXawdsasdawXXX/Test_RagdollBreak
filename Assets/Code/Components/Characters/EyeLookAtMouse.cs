using Code.Services;
using UnityEngine;

namespace Code.Character
{
    public class EyeLookAtMouse : MonoBehaviour
    {
        [SerializeField] private InputService _inputService;
        [SerializeField] private Transform _leftEye;
        [SerializeField] private Transform _rightEye;
        [SerializeField] private float _distance = 40.0f;
        [SerializeField] private float _maxAngle = 70.0f; 

        private Camera _mainCamera;

        private void Awake()
        {
            _mainCamera = Camera.main;
        }

        private void Update()
        {
            if (_inputService.MousePosition == Vector3.zero)
            {
                return;
            }

            Ray ray = _mainCamera.ScreenPointToRay(_inputService.MousePosition);
            Vector3 targetPosition = ray.GetPoint(_distance);

            LookAtPoint(_leftEye, targetPosition);
            LookAtPoint(_rightEye, targetPosition);
        }

        private void LookAtPoint(Transform eye, Vector3 targetPosition)
        {
            Vector3 direction = targetPosition - eye.position;
            Quaternion lookRotation = Quaternion.LookRotation(direction);

            Vector3 limitedEuler = lookRotation.eulerAngles;
            limitedEuler.x = ClampAngle(limitedEuler.x);
            limitedEuler.y = ClampAngle(limitedEuler.y);

            eye.rotation = Quaternion.Euler(limitedEuler);
        }

        private float ClampAngle(float angle)
        {
            switch (angle)
            {
                case < 90 or > 270:
                {
                    if (angle > 180) angle -= 360;
                    break;
                }
                case > 180:
                    angle -= 360;
                    break;
            }

            return Mathf.Clamp(-angle, -_maxAngle, _maxAngle);
        }
    }
}
