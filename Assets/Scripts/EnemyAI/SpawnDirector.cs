using EnemyAI.Bat;
using EnemyAI.Miner;
using EnemyAI.Rat;
using EnemyAI.Skeleton;
using GameCont;
using UnityEngine;

namespace EnemyAI
{
    public class SpawnDirector : MonoBehaviour
    {
        [SerializeField] private int[] _enemyCosts;
        private float _diffCoefficient;
        private float _spawnPoints;
        private PlayerInfo _playerInfo;
        private float _nextSpawnTime;
        private int _tries;
        private void Start()
        {
            _diffCoefficient = (GameObject.FindWithTag("ScoreKeeper").GetComponent<ScoreKeeper>().GetDifficulty() - 1) * 0.25f + 0.75f;
            _playerInfo = GetComponent<PlayerInfo>();
            _nextSpawnTime = Time.time + 8f;
        }
        
        private void Update()
        {
            _spawnPoints += Time.deltaTime * ((_diffCoefficient - 1) * 2 + 1) * _playerInfo.GetCoefficient();
            if (_nextSpawnTime >= Time.time) return;
            TrySummon();
            _nextSpawnTime = Time.time + 2f;
        }
        
        private void TrySummon()
        {
            int chance = Random.Range(0, 3);
            if (chance == 0 || _tries == 2)
            {
                _tries = 0;
                if (_playerInfo.GetGameTime() < 50 / _diffCoefficient)
                {
                    int i = 0;
                    while (true)
                    {
                        if(_spawnPoints < _enemyCosts[0]) break;
                        _spawnPoints -= _enemyCosts[0];
                        Invoke(nameof(SpawnSkeleton), 0.3f * i);
                        i++;
                    }
                }
                else if (_playerInfo.GetGameTime() < 100 / _diffCoefficient)
                {
                    int i = 0;
                    while (true)
                    {
                        int rand = Random.Range(0, 3);
                        if (rand < 2)
                        {
                            if(_spawnPoints < _enemyCosts[0]) break;
                            _spawnPoints -= _enemyCosts[0];
                            Invoke(nameof(SpawnSkeleton), 0.3f * i);
                        }
                        else
                        {
                            if(_spawnPoints < _enemyCosts[1]) break;
                            _spawnPoints -= _enemyCosts[1];
                            Invoke(nameof(SpawnMiner), 0.3f * i);
                        }
                        
                        i++;
                    }
                }
                else if (_playerInfo.GetGameTime() < 150 / _diffCoefficient)
                {
                    int i = 0;
                    while (true)
                    {
                        int rand = Random.Range(0, 6);
                        if (rand < 3)
                        {
                            if(_spawnPoints < _enemyCosts[0]) break;
                            _spawnPoints -= _enemyCosts[0];
                            Invoke(nameof(SpawnSkeleton), 0.3f * i);
                        }
                        else if (rand < 5)
                        {
                            if(_spawnPoints < _enemyCosts[1]) break;
                            _spawnPoints -= _enemyCosts[1];
                            Invoke(nameof(SpawnMiner), 0.3f * i);
                        }
                        else
                        {
                            if(_spawnPoints < _enemyCosts[2]) break;
                            _spawnPoints -= _enemyCosts[2];
                            Invoke(nameof(SpawnRat), 0.3f * i);
                        }
                        
                        i++;
                    }
                }
                else
                {
                    int i = 0;
                    while (true)
                    {
                        int rand = Random.Range(0, 12);
                        if (rand < 4)
                        {
                            if(_spawnPoints < _enemyCosts[0]) break;
                            _spawnPoints -= _enemyCosts[0];
                            Invoke(nameof(SpawnSkeleton), 0.3f * i);
                        }
                        else if (rand < 7)
                        {
                            if(_spawnPoints < _enemyCosts[1]) break;
                            _spawnPoints -= _enemyCosts[1];
                            Invoke(nameof(SpawnMiner), 0.3f * i);
                        }
                        else if (rand < 10)
                        {
                            if(_spawnPoints < _enemyCosts[3]) break;
                            _spawnPoints -= _enemyCosts[3];
                            Invoke(nameof(SpawnBat), 0.3f * i);
                        }
                        else
                        {
                            if(_spawnPoints < _enemyCosts[2]) break;
                            _spawnPoints -= _enemyCosts[2];
                            Invoke(nameof(SpawnRat), 0.3f * i);
                        }
                        i++;
                    }
                }
            }
            else
            {
                _tries++;
            }
        }

        private void SpawnSkeleton()
        {
            GetComponent<SkeletonSpawner>().SpawnEnemy();
        }
        
        private void SpawnMiner()
        {
            GetComponent<MinerSpawner>().SpawnEnemy();
        }
        
        private void SpawnRat()
        {
            GetComponent<RatSpawner>().SpawnEnemy();
        }
        
        private void SpawnBat()
        {
            GetComponent<BatSpawner>().SpawnEnemy();
        }
    }
}
