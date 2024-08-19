using System;
using UnityEngine;

namespace _Scripts
{
    [RequireComponent(typeof(Rigidbody))]
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private float _speed;
        private Rigidbody _rigidbody;
        private Vector3 _direction;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _rigidbody.useGravity = false;
        }

        private void FixedUpdate()
        {
            _rigidbody.velocity = _direction.normalized * _speed;
        }

        public void Initialize(Vector3 direction)
        {
            _direction = direction;
        }

        public void Initialize(Vector3 direction, float shootingSpeed)
        {
            _direction = direction;
            _speed = shootingSpeed;
        }
    }
}