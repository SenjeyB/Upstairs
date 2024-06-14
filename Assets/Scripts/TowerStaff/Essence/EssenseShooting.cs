using GameCont;
using Unity.VisualScripting;
using UnityEngine;

namespace TowerStaff.Essence
{
    public class EssenceShooting : BasicTowerShooting
    {
        private PlayerInfo _playerInfo = GameObject.FindGameObjectWithTag("GameController").GetComponent<PlayerInfo>();
        public override void Upgrade()
        {
            _reloadSpeed -= 2f;
        }
        protected override void Shoot()
        {
            PlaySound(_sounds[0]);
            _playerInfo.AddEssence((int)GetDamage());
        }
    }
}
