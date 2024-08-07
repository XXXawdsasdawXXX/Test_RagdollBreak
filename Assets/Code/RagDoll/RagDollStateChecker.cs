using System;
using UnityEngine;

namespace Code.Character
{
    public class RagDollStateChecker : MonoBehaviour
    {
        [SerializeField] private CharacterJoint _jointToCheck;

        private bool _isBroken;
        public event Action OnBroken;
        
        private void Update()
        {
            if (_isBroken)
            {
                return;
            }
            
            if (_jointToCheck == null ||  _jointToCheck.connectedBody == null)
            {
                _isBroken = true;
                OnBroken?.Invoke();
            }
        }
    }
}