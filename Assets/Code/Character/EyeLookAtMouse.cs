using System;
using UnityEngine;

namespace Code.Character
{
    public class EyeLookAtMouse: MonoBehaviour
    {
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
            Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
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