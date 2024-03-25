using UnityEngine;

namespace TowerStaff
{
    public class BasicShot : MonoBehaviour
    {
        private float _damage;
        
        public float GetDamage()
        {
            return _damage;
        }
        
        public void SetDamage(float damage)
        {
            _damage = damage;
        }
  
    }
}
