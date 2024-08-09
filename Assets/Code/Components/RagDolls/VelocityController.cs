using System;
using System.Linq;
using Code.Interaction;
using UnityEngine;

namespace Code.Character
{
    public class VelocityController : MonoBehaviour
    {
        [SerializeField] private Rigidbody[] _rigidbody;
        [SerializeField] private InteractionJoint[] _interactionJoint;
        [SerializeField] private float _treshould;
        [SerializeField] private Vector3 _value;

        private void Update()
        {
            if (!_interactionJoint.Any(j => j.IsUse))
            {
                
            foreach (var body in _rigidbody)
            {
                if (body.velocity.y > _treshould)
                {
                
           Debug.Log("!!!!!!");
                    body.AddForce(_value * body.mass);
                }
                
            }
            }
        }

        private void OnValidate()
        {
            if (_rigidbody == null)
            {
                _rigidbody = GetComponentsInChildren<Rigidbody>();
            }

            if (_interactionJoint == null || _interactionJoint.Length == 0) 
            {
                _interactionJoint = GetComponentsInChildren<InteractionJoint>();
            }
        }
    }
}