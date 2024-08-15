using GameCont;
using UnityEngine;

namespace TowerStaff.Essence
{
    public class EssenceShooting : BasicTowerShooting
    {
        private PlayerInfo _playerInfo; 
        protected override void Start()
        {
            _ammo = GetComponent<BasicTowerAmmo>();
            _level = GetComponent<BasicTowerLevel>();
            _animations = GetComponent<BasicTowerAnimation>();
            _playerInfo = GameObject.FindGameObjectWithTag("GameController").GetComponent<PlayerInfo>();
            _level.SetLevel(1);
            _materialDefault = _spriteRenderer.material;
            _nextFireTime = Time.time + _reloadSpeed;
        }
        
        public override void Upgrade()
        {
            _damage += 1f;
        }
        protected override void Shoot()
        {
            PlaySound(_sounds[0]);
            _playerInfo.AddEssence((int)GetDamage());
            _nextFireTime = Time.time + _reloadSpeed;
        }
        protected override void ShootingSide() {}
    }
}
