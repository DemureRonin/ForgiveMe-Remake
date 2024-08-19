using Unity.Mathematics;
using UnityEngine;

namespace _Scripts.Components
{
    public class Spawner : MonoBehaviour
    {
        [SerializeField] private GameObject _objectToSpawn;

        public void Spawn()
        {
            Instantiate(_objectToSpawn, transform.position, quaternion.identity);
        }
    }
}