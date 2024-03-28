using UnityEngine;

namespace EnemyAI.Bat
{
    public class BatSpawner : MonoBehaviour
    {
        [SerializeField] public GameObject _enemyPrefab; 
        
        public void SpawnEnemy()
        {
            float rand = Random.value * 20 - 10; 
            Instantiate(_enemyPrefab, new Vector3(rand, 6), Quaternion.identity);
        }
    }
}
