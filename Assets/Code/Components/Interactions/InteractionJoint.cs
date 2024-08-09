using System;
using UnityEngine;

namespace Code.Interaction
{
    
    [RequireComponent(typeof(SpringJoint))]
    public class InteractionJoint : MonoBehaviour
    {
        [SerializeField] private SpringJoint _springJoint;
        [SerializeField] private InteractionPoint _interactionPoint;
        [SerializeField, Range(0, 10000)] private float _defaultSpring = 100;
        [SerializeField, Range(0, 10000)] private float _defaultDamper = 50;
        [SerializeField,Range(0,5)] private float _maxDistance = 2;
        [SerializeField,Range(0,5)] private float _minDistance = 2;

        public bool IsUse { get; private set; }
        public event Action OnStartInteraction;
        public event Action OnStopInteraction;
        
        private void Awake()
        {
            SetEnable(false);
            _springJoint.autoConfigureConnectedAnchor = false;
            _springJoint.maxDistance = _maxDistance;
            _springJoint.minDistance = _minDistance;
        }
        
        public void SetEnable(bool enable)
        {
            _springJoint.spring = enable ? _defaultSpring : 0;
            _springJoint.damper = enable ? _defaultDamper : 0;
            var action = enable ? OnStartInteraction : OnStopInteraction;
            action?.Invoke();
            IsUse = enable;
        }
        
        public Vector3 GetAnchor()
        {
            return  _springJoint.anchor;
        }

        private void OnValidate()
        {
            if (_interactionPoint == null)
            {
                _interactionPoint = FindObjectOfType<InteractionPoint>();
            }

            if (_springJoint == null)
            {
                _springJoint = GetComponent<SpringJoint>();
                _springJoint.connectedBody = FindObjectOfType<InteractionPoint>().GetComponent<Rigidbody>();
            }

            if (_springJoint != null)
            {
                _springJoint.damper = _defaultDamper;
                _springJoint.spring = _defaultSpring;
                _springJoint.maxDistance = _maxDistance;
                _springJoint.minDistance = _minDistance;
            }
        }
    }
}