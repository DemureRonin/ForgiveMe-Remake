using System;
using _Scripts.EnemyScripts.UIEditor;
using UnityEngine;
using UnityEngine.Events;

namespace _Scripts.Components.Health
{
    public class HealthComponent : MonoBehaviour
    {
        [SerializeField] private int _health;
        [SerializeField] private UnityEvent _onDamage;
        [SerializeField] private UnityEvent _onDie;
        [SerializeField] private bool _notifyUI;
        [SerializeField] private HpBar _hpBar;
        private int _maxHp;

        private void Start()
        {
            _maxHp = _health;
        }

        public void ModifyHealth(int damage)
        {
            _health += damage;

            if (_health <= 0)
                _onDie?.Invoke();
            if (damage >= 0) throw new Exception("Damage delta is more than zero");
            _onDamage?.Invoke();
            if (_notifyUI) _hpBar.SetBar(_maxHp, _health);
        }
    }
}