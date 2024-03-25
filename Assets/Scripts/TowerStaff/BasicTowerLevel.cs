using UnityEngine;

namespace TowerStaff
{
    public class BasicTowerLevel : MonoBehaviour
    {
        private int _level;
        private BasicTowerShooting _shooting;
        
        public int GetLevel()
        {
            return _level;
        }
        
        public void Upgrade()
        {
            _level++; 
            _shooting.Upgrade();
        }
        
        public void SetLevel(int level)
        {
            _level = level;
        }
        
        private void Start()
        {
            _shooting = GetComponent<BasicTowerShooting>();
        }
    }
}
