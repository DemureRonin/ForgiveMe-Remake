using System.Collections;
using _Scripts.Camera;
using UnityEngine;
using UnityEngine.SceneManagement;
using Vector3 = UnityEngine.Vector3;

namespace _Scripts.PlayerScripts
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerController : MonoBehaviour
    {
        [Header("Components")] [SerializeField]
        private Transform _orientation;

        [SerializeField] private GroundCheck _groundCheck;

        [Header("Movement")] [SerializeField] private float _moveSpeed = 200;
        [SerializeField] private float _maxSpeed = 10;
        [SerializeField] private float _groundDrag = 10;
        [SerializeField] private float _airMoveResistance = 0.1f;
        [SerializeField] private float _maxAngle = 25;

        [Header("Jumping")] [SerializeField] private float _jumpForce = 14;
        [SerializeField] private float _fallPersistence = 0.4f;
        [SerializeField] private float _risePersistence = 0.4f;
        [SerializeField] private float _jumpBufferTime = 0.2f;
        [SerializeField] private float _coyoteTime = 0.2f;
        [Header("Dashing")] [SerializeField] private float _dashingForce = 40f;
        [SerializeField] private float _dashingLerpSpeed = 100;
        [SerializeField] private float _dashingTime = 0.2f;
        [SerializeField] private float _verticalDashingForce;

        [Header("Camera")] [SerializeField] private float _headbobFrequency = 1.5f;
        [SerializeField] private float _headbobAmplitude = 0.1f;


        private Rigidbody _rigidbody;
        private Vector3 _inputDirection;
        private Vector3 _movementDirection;


        private readonly WaitForSeconds _jumpDelay = new(0.5f);
        private float _coyoteTimeCounter;
        private float _jumpBufferCounter;

        private bool _canJump = true;
        private bool _dashing;

        private bool Grounded => _groundCheck.IsGrounded();

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }

            CalculateJumpingUtils();
        }

        private void FixedUpdate()
        {
            Move();
            CalculateYVelocity();
        }

        private void CalculateYVelocity()
        {
            if (_rigidbody.velocity.y < 0 && !Grounded)
            {
                _rigidbody.velocity = new Vector3(_rigidbody.velocity.x, _rigidbody.velocity.y - _fallPersistence,
                    _rigidbody.velocity.z);
                return;
            }

            if (_rigidbody.velocity.y > 0 && !Grounded)
            {
                _rigidbody.velocity = new Vector3(_rigidbody.velocity.x, _rigidbody.velocity.y - _risePersistence,
                    _rigidbody.velocity.z);
            }
        }

        private void Move()
        {
            var orientation = _orientation.transform;
            _movementDirection = orientation.forward * _inputDirection.z +
                                 orientation.right * _inputDirection.x;
            _movementDirection.Normalize();

            var airResistance = Grounded ? 1 : _airMoveResistance;
            var velocity = new Vector3(_movementDirection.x, 0, _movementDirection.z) * (_moveSpeed * airResistance);
            Vector3 projection;
            _rigidbody.useGravity = true;
            if (_groundCheck.InOnSlope(_maxAngle, _movementDirection, out projection))
            {
                _rigidbody.useGravity = false;
                velocity = projection * (_moveSpeed * airResistance);
            }


            _rigidbody.AddForce(velocity, ForceMode.Force);
            ControlSpeed();
        }

        private void ControlSpeed()
        {
            if (Grounded && _canJump) _rigidbody.drag = _groundDrag;
            else _rigidbody.drag = 0;
            if (_dashing) return;
            var velocity = _rigidbody.velocity;
            var flatVelocity = new Vector3(velocity.x, 0f, velocity.z);

            if (!(flatVelocity.magnitude > _maxSpeed)) return;
            var limitedVel = flatVelocity.normalized * _maxSpeed;
            _rigidbody.velocity = new Vector3(limitedVel.x, _rigidbody.velocity.y, limitedVel.z);
        }

        public void SetMoveDirection(Vector3 direction)
        {
            _inputDirection = direction;
        }

        public void OnJump()
        {
            _jumpBufferCounter = _jumpBufferTime;
        }

        private void CalculateJumpingUtils()
        {
            if ((Grounded || _coyoteTimeCounter > 0f) && _jumpBufferCounter > 0f)
            {
                Jump();
                _jumpBufferCounter = 0f;
            }

            if (_jumpBufferCounter > 0f)
            {
                _jumpBufferCounter -= Time.deltaTime;
            }

            if (Grounded)
            {
                _coyoteTimeCounter = _coyoteTime;
            }
            else
            {
                _coyoteTimeCounter -= Time.deltaTime;
            }
        }

        private void Jump()
        {
            if (!_canJump) return;
            _canJump = false;
            StartCoroutine(JumpCooldown());
            _rigidbody.velocity = new Vector3(_rigidbody.velocity.x, 0f, _rigidbody.velocity.z);

            _rigidbody.AddForce(transform.up * _jumpForce, ForceMode.Impulse);
        }

        private IEnumerator JumpCooldown()
        {
            yield return _jumpDelay;
            _canJump = true;
        }

        public void OnDash()
        {
            Vector3 vector;
            if (_inputDirection == Vector3.zero)
            {
                var orientationForward = _orientation.forward;
                vector = new Vector3(orientationForward.x, _verticalDashingForce, orientationForward.z);
            }
            else
            {
                var dashingDirection = _movementDirection.normalized;
                vector = new Vector3(dashingDirection.x, _verticalDashingForce, dashingDirection.z);
            }

            StartCoroutine(DashingCoroutine(vector));
        }

        private IEnumerator DashingCoroutine(Vector3 vector)
        {
            var time = Time.time;
            _dashing = true;
            _rigidbody.useGravity = false;
            while (Time.time - time < _dashingTime)
            {
                _rigidbody.velocity = vector * _dashingForce;
                yield return null;
            }

            StartCoroutine(LerpToMaxSpeed());
        }

        private IEnumerator LerpToMaxSpeed()
        {
            while (_rigidbody.velocity.magnitude > _maxSpeed)
            {
                var velocity = _rigidbody.velocity;
                _rigidbody.velocity = Vector3.Lerp(velocity, velocity.normalized * _maxSpeed, _dashingLerpSpeed);
                yield return null;
            }

            _rigidbody.useGravity = true;
            _dashing = false;
        }
    }
}