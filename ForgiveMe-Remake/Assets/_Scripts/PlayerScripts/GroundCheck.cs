using UnityEngine;

namespace _Scripts.PlayerScripts
{
    public class GroundCheck : MonoBehaviour
    {
        [SerializeField] private float _groundCheckDistance;
        [SerializeField] private float _slopeCheckDistance;
        [SerializeField] private LayerMask _interactionLayer;
        [SerializeField] private Vector3 _boxSize = new(0.5f, 0.1f, 0.5f);
        [SerializeField] private Vector3 _boxOffset = new(0f, -1.1f, 0f);

        public bool IsGrounded()
        {
            RaycastHit result;
            var hit = Physics.BoxCast(transform.position + _boxOffset, _boxSize / 2, Vector3.down, out result,
                Quaternion.identity,
                _groundCheckDistance,
                _interactionLayer);
            return hit;
        }

        public bool InOnSlope(float maxAngle, Vector3 direction, out Vector3 projection)
        {
            RaycastHit result;
            projection = Vector3.zero;
            var hit = Physics.BoxCast(transform.position + _boxOffset, _boxSize / 2, Vector3.down, out result,
                Quaternion.identity,
                _slopeCheckDistance,
                _interactionLayer);
            
            if (!hit) return false;
            var angle = Vector3.Angle(Vector3.up, result.normal);
            if (angle < maxAngle && angle != 0)
            {
                projection = Vector3.ProjectOnPlane(direction, result.normal).normalized;
                return true;
            }

            return false;
        }

        void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(transform.position + _boxOffset, _boxSize);
        }
    }
}