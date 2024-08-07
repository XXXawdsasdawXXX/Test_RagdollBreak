using System;
using UnityEngine;

namespace Code.Character
{
    public class RagDollStateChecker : MonoBehaviour
    {
        [SerializeField] private CharacterJoint[] _jointToCheck;

        private bool _isBroken;
        public event Action OnBroken;
        
        private void Update()
        {
            if (_isBroken)
            {
                return;
            }

            for (int i = 0; i < _jointToCheck.Length; i++)
            {
                if (IsBroken(_jointToCheck[i]))
                {
                    _isBroken = true;
                    OnBroken?.Invoke();
                }
            }
        }

        private bool IsBroken(CharacterJoint joint)
        {
            return joint == null || joint.connectedBody == null;
        }
    }
}