using TowerStaff.Taser;
using UnityEngine;

namespace TowerStaff.Mine
{
    public class MineShooting : BasicTowerShooting
    {
        public override void Upgrade()
        {
            _damage += 3f;
        }
        
        protected override void Shoot()
        {
            PlaySound(_sounds[0], 0.3f);
            TaserShot projectile = Instantiate(_projectilePrefab, transform.position, Quaternion.identity).GetComponent<TaserShot>();
            projectile.SetDamage(GetDamage());
            _builderTile.SetTowerType(Enums.TowerType.NotSet);
            Destroy(gameObject);
        }
        
        protected override void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Enemy") && other.gameObject.name != "PickaxeAttack")
            {
                Shoot();
            }
        }

        protected override void Start()
        {
            _ammo = GetComponent<BasicTowerAmmo>();
            _level = GetComponent<BasicTowerLevel>();
            _level.SetLevel(3);
        }

        protected override void Update()
        {
        }
        protected override void ShootingSide()
        {
        }

    }
}
