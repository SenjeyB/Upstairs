using UnityEngine;

namespace EnemyAI.Rat
{
    public class RatSpawner : MonoBehaviour
    {
        [SerializeField] public GameObject _enemyPrefab; 
        /*[SerializeField] public float _spawnInterval;
        private float _nextSpawnTime;
        private void Start()
        {
            _nextSpawnTime = Time.time + _spawnInterval;
        }
        
        private void Update()
        {
            if (!(Time.time >= _nextSpawnTime))
            {
                return;
            }
            SpawnEnemy();
            _nextSpawnTime = Time.time + _spawnInterval;
        }*/

        public void SpawnEnemy()
        {
            int rand = Random.value > 0.5 ? 1 : -1;
            Instantiate(_enemyPrefab, new Vector3(6.5f * rand, 6), Quaternion.identity);
        }
    }
}
