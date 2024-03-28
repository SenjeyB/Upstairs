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

        protected override void ShootingSide()
        {
            var enemies = GameObject.FindGameObjectsWithTag("Enemy");
            GameObject closestEnemy = null;
            float closestDistance = float.MaxValue;

            foreach (GameObject enemy in enemies)
            {
                if (enemy.transform.position.y < transform.position.y - 1f || enemy.transform.position.y > transform.position.y + 2f)
                {
                    continue;
                }
                float distance = Vector2.Distance(transform.position, enemy.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestEnemy = enemy;
                }
            }
            if (closestEnemy == null)
            {
                return;
            }
            if (closestEnemy.transform.position.x >= transform.position.x && !_spriteRenderer.flipX)
            {
                return;
            }
            if (closestEnemy.transform.position.x <= transform.position.x && _spriteRenderer.flipX)
            {
                return;
            }

            _spriteRenderer.flipX = !_spriteRenderer.flipX;
        }
    }
}
