using GameCont;
using UnityEngine;

namespace Bonuses
{
    public class BonusSpawn : MonoBehaviour
    {
        [SerializeField] private GameObject _bonusPrefab;
        private int _bonusType;
        private GameObject _bonus;
        private float _nextSpawnTime;
        private PlayerInfo _playerInfo;
        private float _timeMult;
        
        private void Spawn()
        {
            int bonusType = Random.Range(0, 2);
            float spawnCord = Random.value * 14 - 7;
            _bonus = Instantiate(_bonusPrefab, new Vector3(spawnCord, 7, 0), Quaternion.identity);
            _bonus.GetComponent<Bonus>().SetBonusType(bonusType);
            _nextSpawnTime = Time.time + (int)(Random.Range(80, 100) * _timeMult);
        }
        private void Start()
        {
            _playerInfo = GameObject.FindGameObjectWithTag("GameController").GetComponent<PlayerInfo>();
            _timeMult = _playerInfo.GetDifficultyMult();
            _timeMult *= 2;
            _timeMult += 1;
            _nextSpawnTime = Time.time + (int)(Random.Range(70, 90) * _timeMult);
        }
        private void Update()
        {
            if (Time.time >= _nextSpawnTime)
            {
                Spawn();
            }
        }
        
    }
}
