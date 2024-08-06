using System;
using UnityEngine;

namespace Code.Interaction
{
    public class InteractionObject: MonoBehaviour
    {
        [Header("Services")]
        [SerializeField] private InteractionPoint _interactionPoint;

        [Header("Components")] 
        [SerializeField] private HingeJoint _hingeJoint;
        
        [Header("Params")]
        [SerializeField] private float _startDistance;
        [SerializeField] private float _maxDistance = 10;
        private float _currentDistance;

        private void Update()
        {
            
        }
    }
}