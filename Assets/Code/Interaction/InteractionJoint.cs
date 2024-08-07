﻿using System;
using UnityEngine;

namespace Code.Interaction
{
    
    [RequireComponent(typeof(SpringJoint))]
    public class InteractionJoint : MonoBehaviour
    {
        [SerializeField] private SpringJoint _springJoint;
        [SerializeField] private InteractionPoint _interactionPoint;
        [SerializeField, Range(50, 10000)] private float _defaultSpring = 100;
        [SerializeField, Range(50, 10000)] private float _defaultDamper = 50;
        [SerializeField,Range(0,5)] private float _maxDistance = 2;

        public event Action OnStartInteraction;
        public event Action OnStopInteraction;
        
        private void Awake()
        {
            SetEnable(false);
            _springJoint.autoConfigureConnectedAnchor = false;
            _springJoint.maxDistance = _maxDistance;
        }
        
        public void SetEnable(bool enable)
        {
            _springJoint.spring = enable ? _defaultSpring : 0;
            _springJoint.damper = enable ? _defaultDamper : 0;
            var action = enable ? OnStartInteraction : OnStopInteraction;
            action?.Invoke();
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
        }
    }
}