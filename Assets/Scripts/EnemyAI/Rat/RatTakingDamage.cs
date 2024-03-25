using BuildTile;
using GameCont;
using UnityEngine;

namespace EnemyAI.Rat
{
    public class RatTakingDamage : EnemyTakingDamage
    {
        protected override void Slowing()
        {
            GetComponent<RatMovement>().Slow();
        }
        protected override void Killed()
        {
            GetComponent<RatMovement>().GetTurret().GetComponent<BuilderTile>().RepairTower();
            _gameInfo.AddEssence(2);
            _gameInfo.AddScore(20);
        }
        private void Start()
        {
            _gameInfo = GameObject.FindGameObjectWithTag("GameController").GetComponent<PlayerInfo>();
            IncreaseHealth();
            _materialDefault = _spriteRenderer.material;
        }
    }
}
