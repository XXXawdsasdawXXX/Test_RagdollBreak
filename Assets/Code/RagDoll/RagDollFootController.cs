using UnityEngine;

namespace Code.Character
{
    [RequireComponent(typeof(Rigidbody))]
    public class RagDollFootController: MonoBehaviour
    {
        [SerializeField] private CharacterJoint _joint;
        [SerializeField] private float _raycastDistance = 1.2f;
        [SerializeField] private LayerMask _groundLayerMask;
        [SerializeField] private Rigidbody _rigidbody;

        private bool _isEnable = true;
        private void Update()
        {
            /*_rigidbody.isKinematic = _joint != null && _isEnable && 
                                     Physics.Raycast(transform.position, Vector3.down, _raycastDistance, _groundLayerMask);
            /*if (_rigidbody.isKinematic)
            {
                _rigidbody.velocity = Vector3.zero;
            }#1#*/
        }

        public void SetEnable(bool enable)
        {
            _isEnable = enable;
            _rigidbody.freezeRotation = _isEnable;
        }
        
        private Vector3 GetTarget()
        {
            return transform.position + transform.up * -1 * _raycastDistance;
        }
        
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, transform.position + transform.up * -1 * _raycastDistance);
        }
        private void OnValidate()
        {
            if (_rigidbody == null)
            {
                _rigidbody = GetComponent<Rigidbody>();
            }

            if (_joint == null)
            {
                _joint = GetComponent<CharacterJoint>();
            }
        }
    }
}