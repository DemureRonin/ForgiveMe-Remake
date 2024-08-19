using System;
using _Scripts.PlayerScripts;
using UnityEngine;

namespace _Scripts.Effects
{
    public class LookAtEffect : MonoBehaviour
    {
        private PlayerController _player;

        private void Awake()
        {
            _player = FindObjectOfType<PlayerController>();
        }

        private void Update()
        {
            LookAtPlayer();
        }


        private void LookAtPlayer()
        {
            transform.LookAt(_player.transform, Vector3.up);
        }
    }
}