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

        protected override void ShootingSide()
        {
            var enemies = GameObject.FindGameObjectsWithTag("Enemy");
            GameObject farEnemy = null;
            float longestDistance = float.MinValue;

            foreach (GameObject enemy in enemies)
            {
                if (enemy.transform.position.y < transform.position.y - 1f)
                {
                    continue;
                }
                float distance = Vector2.Distance(transform.position, enemy.transform.position);
                if (distance > longestDistance)
                {
                    longestDistance = distance;
                    farEnemy = enemy;
                }
            }
            if (farEnemy == null)
            {
                return;
            }
            if (farEnemy.transform.position.x >= transform.position.x && !_spriteRenderer.flipX)
            {
                return;
            }
            if (farEnemy.transform.position.x <= transform.position.x && _spriteRenderer.flipX)
            {
                return;
            }

            _spriteRenderer.flipX = !_spriteRenderer.flipX;
        }
    }
}
