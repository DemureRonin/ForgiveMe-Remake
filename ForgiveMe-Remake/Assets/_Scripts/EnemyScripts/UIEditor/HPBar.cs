using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.EnemyScripts.UIEditor
{
    public class HpBar : MonoBehaviour
    {
        [SerializeField] private Image _hpBarImage;

        public void SetBar(int maxHp, int health)
        {
            _hpBarImage.fillAmount = (float)health / (float)maxHp;
        }
    }
}