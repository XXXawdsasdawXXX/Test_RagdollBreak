using System;
using Code.Services;
using UnityEngine;

namespace Code.Interaction
{
    public class InteractionPoint : MonoBehaviour
    {
        [Header("Services")]
        [SerializeField] private InputService _inputService;
        
        [Header("Components")]
        [SerializeField] private Rigidbody _rigidbody;
        private Camera _camera;

        private float _zPosition;
        
        public bool IsUsed { get; private set; }

        private void Awake()
        {
            _camera = Camera.main;
            _zPosition = transform.position.z;
        }

        private void OnEnable()
        {
            SubscribeToEvents(true);
        }

        private void OnDisable()
        {
            SubscribeToEvents(false);
        }

        private void SubscribeToEvents(bool flag)
        {
            if (flag)
            {
                _inputService.OnStartTouch += StartMove;
                _inputService.OnTouch += Move;
                _inputService.OnEndTouch += StopMove;
            }
            else
            {
                _inputService.OnStartTouch -= StartMove;
                _inputService.OnTouch -= Move;
                _inputService.OnEndTouch -= StopMove;
            }
        }

        private void StartMove(Vector3 mousePosition)
        {
            IsUsed = true;
            _rigidbody.isKinematic = true;
        }

        private void Move(Vector3 mousePosition)
        {
            // Устанавливаем Z компоненту в глубину от камеры до целевого объекта
            mousePosition.z = _camera.WorldToScreenPoint(transform.position).z;
            var target = _camera.ScreenToWorldPoint(mousePosition);
            transform.position = target;
        }

        private void StopMove(Vector3 mousePosition)
        {
            IsUsed = false;
            _rigidbody.isKinematic = false;
        }
        
    }
}