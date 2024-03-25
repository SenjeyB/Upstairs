using UnityEngine;

namespace TowerStaff.Taser
{
    public class TaserShooting : BasicTowerShooting
    {
        public override void Upgrade()
        {
            _reloadSpeed -= 0.5f;
            _damage += 0.7f;
            _ammoCount += 5;
        }
        
        protected override void Shoot()
        {
            PlaySound(_sounds[0], 0.3f);
            TaserShot projectile = Instantiate(_projectilePrefab, transform.position, Quaternion.identity).GetComponent<TaserShot>();
            projectile.SetDamage(GetDamage());
            if (_level.GetLevel() == 3) projectile.Upgrade();
            _nextFireTime = Time.time + _reloadSpeed;
            _ammo.DecreaseAmmo();
        }
        
        protected override void ShootingSide()
        {
        }
    }
}
