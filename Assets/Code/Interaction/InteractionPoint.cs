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
        [SerializeField] private Transform _interactionObject;

        private Camera _camera;

        public bool IsUsed { get; private set; }

        private void Awake()
        {
            _camera = Camera.main;
            StopMove(Vector3.zero);
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
            mousePosition.z = _camera.WorldToScreenPoint(transform.position).z;
            var target = _camera.ScreenToWorldPoint(mousePosition);
            transform.position = target;
        }

        private void StopMove(Vector3 mousePosition)
        {
            IsUsed = false;
            _rigidbody.isKinematic = false;
            transform.position = _interactionObject.position;
        }
    }
}