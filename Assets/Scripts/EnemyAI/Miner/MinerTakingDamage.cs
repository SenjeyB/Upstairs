using GameCont;
using UnityEngine;

namespace EnemyAI.Miner
{
    public class MinerTakingDamage : EnemyTakingDamage
    {
        private MinerMovement _minerMovement;
        
        
        protected override void Killed()
        {
            _gameInfo.AddEssence(1);
            _gameInfo.AddScore(15);
            Destroy(_minerMovement._pickaxe);
        }
        
        private void Start()
        {
            _gameInfo = GameObject.FindGameObjectWithTag("GameController").GetComponent<PlayerInfo>(); 
            IncreaseHealth();
            _minerMovement = GetComponent<MinerMovement>();
            _materialDefault = _spriteRenderer.material;
        }
    }
}
