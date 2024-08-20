using UnityEngine;
using UnityEngine.InputSystem;

namespace _Scripts.PlayerScripts
{
    [RequireComponent(typeof(PlayerController), typeof(PlayerActions))]
    public class InputManager : MonoBehaviour
    {
        private PlayerController _playerController;
        private PlayerActions _playerActions;

        private void Awake()
        {
            _playerController = GetComponent<PlayerController>();
            _playerActions = GetComponent<PlayerActions>();
        }

        public void HandleMovement(InputAction.CallbackContext context)
        {
            var direction = context.ReadValue<Vector3>();
            _playerController.SetMoveDirection(direction);
        }

        public void OnJump(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                _playerController.OnJump();
            }
        }
        public void OnDash(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                _playerController.OnDash();
            }
        }
        public void OnAttack(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                _playerActions.OnAttack();
            }
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