using UnityEngine;

namespace _Scripts.Effects
{
    public class VerticalLevitationComponent : MonoBehaviour
    {
        [SerializeField] private float _frequency = 1f;
        [SerializeField] private float _amplitude = 1f;
        [SerializeField] private bool _randomize;

        private float _originalY;
        private float _seed;

        private void Awake()
        {
           
            _originalY = transform.localPosition.y;
            if (_randomize)
                _seed = Random.value * Mathf.PI * 2;
        }

        private void Update()
        {
            var position = transform.localPosition;
            position.y = _originalY + Mathf.Sin(_seed + Time.time * _frequency) * _amplitude;
            transform.localPosition = position;
        }
    }
}