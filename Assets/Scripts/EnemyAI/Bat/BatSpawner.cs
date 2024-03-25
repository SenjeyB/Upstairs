using UnityEngine;

namespace EnemyAI.Bat
{
    public class BatSpawner : MonoBehaviour
    {
        [SerializeField] public GameObject _enemyPrefab; 
        
        public void SpawnEnemy()
        {
            float rand = Random.value * 14 - 7; 
            Instantiate(_enemyPrefab, new Vector3(7 * rand, 6), Quaternion.identity);
        }
    }
}
