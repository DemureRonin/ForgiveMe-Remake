using UnityEngine;
using UnityEngine.InputSystem;

namespace _Scripts.PlayerScripts
{
    [RequireComponent(typeof(PlayerController))]
    public class InputManager : MonoBehaviour
    {
        private PlayerController _player;

        private void Awake()
        {
            _player = GetComponent<PlayerController>();
        }

        public void HandleMovement(InputAction.CallbackContext context)
        {
            var direction = context.ReadValue<Vector3>();
            _player.SetMoveDirection(direction);
        }

        public void OnJump(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                _player.OnJump();
            }
        }
        public void OnDash(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                _player.OnDash();
            }
        }
        public void OnAttack(InputAction.CallbackContext context)
        {
          
        }
        public void OnThrow(InputAction.CallbackContext context)
        {
          
        }
        public void OnWeaponSwitch(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
            
            }
        }
    }
}