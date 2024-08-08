using UnityEngine;

namespace Code.Character
{
    public class RagDollController : MonoBehaviour
    {
        [SerializeField] private RagDollStateChecker _stateChecker;
        [SerializeField] private Rigidbody[] _bodies;
        [SerializeField] private Collider[] _ragDollColliders;
    
        [SerializeField] private bool _isActive;
    
        private void OnEnable()
        {
            SetEnable(_isActive);
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
                _stateChecker.OnBroken += OnBroken;
            }
            else
            {
                _stateChecker.OnBroken -= OnBroken;

            }
        }

        private void OnBroken()
        {
        }

        public void SetEnable(bool enable)
        {
            foreach (var rb in _bodies)
            {
                rb.isKinematic = !enable;
            }
            foreach (var col in _ragDollColliders)
            {
                col.enabled = enable;
            }
            
            _isActive = enable;
        }

        #region Editor
#if UNITY_EDITOR
    
        public void FindAllRigidBodies()
        {
            _bodies = GetComponentsInChildren<Rigidbody>();
        }


        public void FindColliders()
        {
            _ragDollColliders = GetComponentsInChildren<Collider>();
        }
#endif

        #endregion
    }
}
