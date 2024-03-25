using UnityEngine;

namespace TowerStaff.Ballista
{
    public class BallistaShooting : BasicTowerShooting
    {
        
        public override void Upgrade()
        {
            _reloadSpeed -= 0.6f;
            _damage += 0.4f;
            _ammoCount += 6;
        }
        protected override void Shoot()
        {
            PlaySound(_sounds[0], 0.3f);
            BallistaShot projectile = Instantiate(_projectilePrefab, transform.position, Quaternion.identity).GetComponent<BallistaShot>();
            projectile.SetDamage(GetDamage());
            if (_level.GetLevel() == 3) projectile.Upgrade();
            projectile.SetDirection(_spriteRenderer.flipX);
            _nextFireTime = Time.time + _reloadSpeed;
            _ammo.DecreaseAmmo();
        }
        
    }
}
