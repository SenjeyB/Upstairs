using UnityEngine;

namespace EnemyAI.Miner
{
    public class PickaxeAttack : MonoBehaviour
    {
        [SerializeField] private float _speed;
        private float _direction;
        private float _isNotBack;

        private void Start()
        {
            _isNotBack = 1;
            Invoke(nameof(AttackMiddle), 0.9f);
        }

        private void AttackMiddle()
        {
            _isNotBack = 0;
            _direction *= -1f;
            Invoke(nameof(AttackEnding), 0.4f);
        }

        private void AttackEnding()
        {
            _isNotBack = -1;
            Invoke(nameof(AttackEnd), 0.9f);
        }

        private void AttackEnd()
        {
            Destroy(gameObject);
        }

        private void Update()
        {
            transform.Rotate(0, 0, 1000 * Time.deltaTime);
            if (_isNotBack == 0) return;
            transform.position += new Vector3(_speed * Time.deltaTime * _direction, 0, 0);
        }

        public void SetDirection(bool direction)
        {
            _direction = direction ? -1 : 1;
        }
    }
}
