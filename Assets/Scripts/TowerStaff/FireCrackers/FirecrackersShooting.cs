using TowerStaff.Catapult;
using UnityEngine;

namespace TowerStaff.FireCrackers
{
    public class FirecrackersShooting : BasicTowerShooting
    {
        public override void Upgrade()
        {
            _reloadSpeed -= 0.5f;
            _damage += 0.5f;
        }
        
        protected override void Shoot()
        {
            PlaySound(_sounds[0], 0.3f);
            FirecrackersShot projectile = Instantiate(_projectilePrefab, transform.position, Quaternion.identity).GetComponent<FirecrackersShot>();
            projectile.SetDamage(GetDamage());
            _nextFireTime = Time.time + _reloadSpeed;
            if(_level.GetLevel() < 3 && projectile.GetTarget() != null) _ammo.DecreaseAmmo();
        }
        
        protected override void Update()
        {
            if (_ammo.GetAmmo() <= 0 || _builderTile.IsBroken())
            {
                _spriteRenderer.material = _materialOff;
                return;
            }
            _spriteRenderer.material = _materialDefault;
            if (Time.time < _nextFireTime)
            {
                return;
            }
            Shoot();
        }
        
    }
}
