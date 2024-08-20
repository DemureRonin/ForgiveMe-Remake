using System;
using _Scripts.PlayerScripts.Sphere;
using UnityEngine;

namespace _Scripts.PlayerScripts
{
    public class PlayerActions : MonoBehaviour
    {
        [SerializeField] private GameObject _shootingSphere;
        [SerializeField] private SkinnedMeshRenderer _sphereHolderMeshRenderer;
        private bool _isReadyToShoot = true;
        private GameObject _sphereProjectile;
       [SerializeField] private Transform _shootingPosition;

        public delegate void ActionEvent();


        public static event ActionEvent OnRoundSphereReturn;

        public void OnAttack()
        {
            if (_isReadyToShoot)
            {
                // _playAudioComponent.Play("ThrowRound");
                _sphereProjectile =
                    Instantiate(_shootingSphere, _shootingPosition.transform.position, Quaternion.identity);
                _sphereProjectile.GetComponent<RoundSphereProjectile>().Initialize( _shootingPosition.transform.position);
                _isReadyToShoot = false;
                _sphereHolderMeshRenderer.enabled = false;
                return;
            }

            _isReadyToShoot = true;
            _sphereHolderMeshRenderer.enabled = true;
          //  _isIcoSphereActive = false;
           // _playAudioComponent.Play("Return");
            OnRoundSphereReturn?.Invoke();
        }
    }
}