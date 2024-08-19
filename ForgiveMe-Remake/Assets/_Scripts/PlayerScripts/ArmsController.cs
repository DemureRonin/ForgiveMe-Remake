using System;
using UnityEngine;

namespace _Scripts.PlayerScripts
{
    public class ArmsController : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private Collider _hurtBox;
        [SerializeField] private GameObject _swordArmsModel;
        [SerializeField] private Projectile _swordProjectilePrefab;
        [SerializeField] private LayerMask _rayCastLayers;
        
        private float _maxDistance = 100f;
        private UnityEngine.Camera _camera;

        private void Awake()
        {
            _camera = UnityEngine.Camera.main;
        }

        public void Attack()
        {
            _animator.Play("attack1");
        }


        public void EnableHurtBox()
        {
            _hurtBox.enabled = true;
        }

        public void DisableHurtBox()
        {
            _hurtBox.enabled = false;
        }

        public void OnThrowSword()
        {
            _animator.Play("throw");
        }

        public void ThrowSword()
        {
            _swordArmsModel.SetActive(false);
            var sword = Instantiate(_swordProjectilePrefab, _swordArmsModel.transform.position,
                _swordArmsModel.transform.rotation); 
            var ray = _camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            var targetPoint =
                Physics.Raycast(ray, out var hit, _maxDistance, _rayCastLayers, QueryTriggerInteraction.Ignore)
                    ? hit.point
                    : ray.GetPoint(_maxDistance);
            var direction = targetPoint - transform.position;
        sword.Initialize(direction);
           
        }
    }
}