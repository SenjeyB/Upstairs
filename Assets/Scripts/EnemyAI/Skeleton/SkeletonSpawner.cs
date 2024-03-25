
using UnityEngine;

namespace EnemyAI.Skeleton
{
    public class SkeletonSpawner : MonoBehaviour
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
            float rand = Random.Range(0.7f, 1f);
            rand *= Random.value > 0.5 ? 1f : -1f;
            Instantiate(_enemyPrefab, new Vector3(6.5f * rand, 6), Quaternion.identity);
        }
    }
}