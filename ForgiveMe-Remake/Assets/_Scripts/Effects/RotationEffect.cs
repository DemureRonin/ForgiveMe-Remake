using UnityEngine;

namespace _Scripts.Effects
{
    public class RotationEffect : MonoBehaviour
    {
       [SerializeField] private float _rotationSpeedZ;
       [SerializeField] private float _rotationSpeedY = 0;
       [SerializeField] private float _rotationSpeedX = 0;

        private void Update()
        {
            transform.Rotate(_rotationSpeedX * Time.deltaTime,_rotationSpeedY * Time.deltaTime,_rotationSpeedZ * Time.deltaTime);
        }
        
    }
}
