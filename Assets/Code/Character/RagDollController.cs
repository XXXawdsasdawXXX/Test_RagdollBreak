using UnityEngine;

namespace Code.Character
{
    public class RagDollController : MonoBehaviour
    {
        [SerializeField] private Rigidbody[] _bodies;
        [SerializeField] private Rigidbody[] _footBodies;
        [SerializeField] private Collider[] _ragDollColliders;
    
        [SerializeField] private bool _isActive;
    
        private void OnEnable()
        {
            SetEnable(_isActive);
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

            foreach (var footBody in _footBodies)
            {
                footBody.isKinematic = true;
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
