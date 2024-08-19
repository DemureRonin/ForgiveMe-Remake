using UnityEngine;

namespace _Scripts.Camera
{
    public class CameraRotation : MonoBehaviour
    {
        [SerializeField] private float _sensitivityX;
        [SerializeField] private float _sensitivityY;
        [SerializeField] private Transform _orientation;
        private Vector2 _rotation;

        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        private void Update()
        {
            HandleMouseLook();
        }

        private void HandleMouseLook()
        {
            var mouseAxisX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * _sensitivityX;
            var mouseAxisY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * _sensitivityY;

            _rotation.y += mouseAxisX;
            _rotation.x -= mouseAxisY;

            _rotation.x = Mathf.Clamp(_rotation.x, -90, 90);

            transform.rotation = Quaternion.Euler(_rotation.x, _rotation.y, 0);
            _orientation.rotation = Quaternion.Euler(0, _rotation.y, 0);
        }
    }
}