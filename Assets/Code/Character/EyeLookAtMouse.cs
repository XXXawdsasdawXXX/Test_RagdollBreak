using System;
using Code.Services;
using UnityEngine;

namespace Code.Character
{
    public class EyeLookAtMouse: MonoBehaviour
    {
        [SerializeField] private InputService _inputService;
        [SerializeField] private  Transform _leftEye; 
        [SerializeField] private  Transform _rightEye;
        [SerializeField] private float _distance = 40.0f;
        
        private  Camera _mainCamera; 
        
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
            eye.rotation = Quaternion.Euler(-lookRotation.eulerAngles.x, -lookRotation.eulerAngles.y, -lookRotation.eulerAngles.z);
        }
    }
}