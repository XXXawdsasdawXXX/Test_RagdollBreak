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
        private InteractionJoint _currentJoint;
        
        public bool IsUsed { get; private set; }
        public event Action<InteractionJoint> OnStartUse; 
        public event Action OnStopUse; 


        private void Awake()
        {
            _camera = Camera.main;
            _rigidbody.isKinematic = false;
            transform.position = _interactionObject.position;
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
            Ray ray = _camera.ScreenPointToRay(mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.TryGetComponent(out _currentJoint))
                {
                    IsUsed = true;
                    _rigidbody.isKinematic = true;
                   //_currentJoint.SetConnectedAnchor(hit.transform.InverseTransformPoint(hit.point));
                    _currentJoint.SetEnable(true);
                    OnStartUse?.Invoke(_currentJoint);
                }
                else
                {
                    Debug.Log("Raycast did not hit the interaction object.");
                }
            }
        }

        private void Move(Vector3 mousePosition)
        {
            if (!IsUsed)
            {
                return;
            }
            mousePosition.z = _camera.WorldToScreenPoint(transform.position).z;
            var target = _camera.ScreenToWorldPoint(mousePosition);
            transform.position = target;
        }

        private void StopMove(Vector3 mousePosition)
        {
            if (!IsUsed)
            {
                return;
            }
           
            IsUsed = false;
            _rigidbody.isKinematic = false;
            
            OnStopUse?.Invoke();
            
            _currentJoint.SetEnable(false);
            _currentJoint = null;
            
            //transform.position = _interactionObject.position;
        }
    }
}