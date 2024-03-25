using GameCont;
using UnityEngine;

namespace EnemyAI.Skeleton
{
    public class SkeletonTakingDamage : EnemyTakingDamage
    {
        protected override void Slowing()
        {
            GetComponent<SkeletonMovement>().Slow();
        }

        protected override void Killed()
        {
            _gameInfo.AddEssence(1);
            _gameInfo.AddScore(10);
        }
        private void Start()
        {
            _gameInfo = GameObject.FindGameObjectWithTag("GameController").GetComponent<PlayerInfo>();
            IncreaseHealth();
            _materialDefault = _spriteRenderer.material;
        }
    }
}
