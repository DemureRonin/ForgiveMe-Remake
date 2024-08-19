using System.Collections;
using _Scripts.PlayerScripts;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Scripts.EnemyScripts
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private GameObject _projectile;
        private Transform _player;

        private void Start()
        {
          //  StartCoroutine(Shoot());
            _player = FindObjectOfType<PlayerController>().transform;
        }

        private IEnumerator Shoot()
        {
            while (enabled)
            {
                yield return new WaitForSeconds(Random.Range(3, 6));
                var position = transform.position;
                var obj = Instantiate(_projectile, position, quaternion.identity);
                var projectile = obj.GetComponent<Projectile>();
                projectile.Initialize(_player.position - position);
            }
        }
    }
}