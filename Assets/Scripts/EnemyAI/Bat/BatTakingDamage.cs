using GameCont;
using UnityEngine;

namespace EnemyAI.Bat
{
    public class BatTakingDamage : EnemyTakingDamage
    {
        protected override void Slowing()
        {
            GetComponent<BatMovement>().Slow();
        }
        protected override void Killed()
        {
            _gameInfo.AddEssence(1);
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
