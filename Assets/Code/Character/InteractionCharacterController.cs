using System;
using Code.Interaction;
using UnityEngine;

namespace Code.Character
{
    public class InteractionCharacterController : MonoBehaviour
    {
        [SerializeField] private RagDollController _ragDollController;
        [SerializeField] private InteractionJoint[] _interactionJoints;
        [SerializeField] private RagDollFootController[] _foots;

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
                foreach (var interactionJoint in _interactionJoints)
                {
                    interactionJoint.OnStartInteraction += OnStartInteraction;
                    interactionJoint.OnStopInteraction += OnStopInteraction;
                }        
            }
            else
            {
                foreach (var interactionJoint in _interactionJoints)
                {
                    interactionJoint.OnStartInteraction -= OnStartInteraction;
                    interactionJoint.OnStopInteraction -= OnStopInteraction;
                }        
            }
        }

        private void OnStopInteraction()
        {
            foreach (var foot in _foots)
            {
                foot.SetEnable(true);
            }
        }

        private void OnStartInteraction()
        {
            foreach (var foot in _foots)
            {
                foot.SetEnable(false);
            }
        }


        #region Editor
#if UNITY_EDITOR

        public void FindInteractionJoints()
        {
            _interactionJoints = GetComponentsInChildren<InteractionJoint>();
        }
        
        public void FindFoots()
        {
            _foots = GetComponentsInChildren<RagDollFootController>();
        }
#endif
        #endregion
    }
}