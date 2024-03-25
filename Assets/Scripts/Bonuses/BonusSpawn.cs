using UnityEngine;

namespace Bonuses
{
    public class BonusSpawn : MonoBehaviour
    {
        [SerializeField] private GameObject _bonusPrefab;
        private int _bonusType;
        private GameObject _bonus;
        private float _nextSpawnTime;
        
        private void Spawn()
        {
            int bonusType = Random.Range(0, 2);
            float spawnCord = Random.value * 14 - 7;
            _bonus = Instantiate(_bonusPrefab, new Vector3(spawnCord, 7, 0), Quaternion.identity);
            _bonus.GetComponent<Bonus>().SetBonusType(bonusType);
            _nextSpawnTime = Time.time + Random.Range(70, 110);
        }
        private void Start()
        {
            _nextSpawnTime = Time.time + Random.Range(45, 75);
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
