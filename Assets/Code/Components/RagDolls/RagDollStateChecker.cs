using System;
using System.Collections.Generic;
using System.Linq;
using Code.Components.Cameras;
using UnityEngine;

namespace Code.Character
{
    public class RagDollStateChecker : MonoBehaviour
    {
        [SerializeField] private CharacterJoint[] _jointToCheck;
        [SerializeField] private CameraShaker _cameraShaker;
        private List<CharacterJoint> _activeJoints;

        private bool _isBroken;
        public event Action OnBroken;

        private void Awake()
        {
            _activeJoints = _jointToCheck.ToList();
        }

        private void Update()
        {
            if (_activeJoints == null || _activeJoints.Count == 0)
            {
                return;
            }
            
            for (int i = 0; i < _activeJoints.Count; i++)
            {
                var joint = _activeJoints[i];
                if (IsBroken(joint))
                {
                    _cameraShaker.Shake();
                    _activeJoints.Remove(joint);
                    
                    if (!_isBroken)
                    {
                        _isBroken = true;
                        OnBroken?.Invoke();
                    }
                    
                    break;
                }
            }
        }

        private bool IsBroken(CharacterJoint joint)
        {
            return joint == null || joint.connectedBody == null;
        }
    }
}