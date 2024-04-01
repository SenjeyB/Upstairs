using UnityEngine;

namespace TowerStaff.Catapult
{
    public class CatapultShooting : BasicTowerShooting
    {
        public override void Upgrade()
        {
            _reloadSpeed -= 0.7f;
            _damage += 1f;
            _ammo.IncreaseAmmoCount(4);
        }
        
        protected override void Shoot()
        {
            PlaySound(_sounds[0], 0.3f);
            CatapultShot projectile = Instantiate(_projectilePrefab, transform.position, Quaternion.identity).GetComponent<CatapultShot>();
            projectile.SetDamage(GetDamage());
            if (_level.GetLevel() == 3) projectile.Upgrade();
            projectile.SetDirection(_spriteRenderer.flipX);
            _nextFireTime = Time.time + _reloadSpeed;
            _ammo.DecreaseAmmo();
        }
    }
}
