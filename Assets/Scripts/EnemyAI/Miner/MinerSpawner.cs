using UnityEngine;

namespace EnemyAI.Miner
{
    public class MinerSpawner : MonoBehaviour
    {
        [SerializeField] public GameObject _enemyPrefab; 
        [SerializeField] public float _spawnInterval;
        private GameObject _minerLeft;
        private GameObject _minerRight;
        private float _nextSpawnTime;
        private int _spawnSide;
        /*private void Start()
        {
            _nextSpawnTime = Time.time + _spawnInterval;
            _spawnSide = 0;
        }
        
        private void Update()
        {
            if (!(Time.time >= _nextSpawnTime))
            {
                return;
            }
            SpawnEnemy();
            
        }*/
        
        public void SpawnEnemy()
        {
            int rand = Random.value > 0.5 ? 1 : -1; 
            _minerRight = Instantiate(_enemyPrefab, new Vector3(7 * rand, 6), Quaternion.identity);
            _nextSpawnTime = Time.time + _spawnInterval;
        }

        private void OldSpawnEnemy()
        {
            if ((_spawnSide == 0 && _minerLeft == null) || (_minerRight != null && _minerLeft == null))
            {
                _minerLeft = Instantiate(_enemyPrefab, new Vector3(-7, 6), Quaternion.identity);
                _spawnSide = 1;
            }
            else if ((_spawnSide == 1 && _minerRight == null) || (_minerLeft != null && _minerRight == null))
            {
                _minerRight = Instantiate(_enemyPrefab, new Vector3(7, 6), Quaternion.identity);
                _spawnSide = 0;
            }
            _nextSpawnTime = Time.time + _spawnInterval;
        }
    }
}
