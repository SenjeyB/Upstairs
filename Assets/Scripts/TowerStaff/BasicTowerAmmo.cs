using UnityEngine;

namespace TowerStaff
{
    public class BasicTowerAmmo : MonoBehaviour 
    {
        private BasicTowerShooting _shooting;
        private int _ammoCount;
        private int _ammo;
        public void AddAmmo()
        {
            _ammo = _ammoCount;
            _shooting.Reload();
        }
        
        private void Start()
        {
            _shooting = GetComponent<BasicTowerShooting>();
            _ammoCount = _shooting.GetAmmoCount();
            _ammo = _ammoCount;
        }
        
        public int GetAmmo()
        {
            return _ammo;
        }
        
        public void DecreaseAmmo()
        {
            _ammo--;
        }
    }
}