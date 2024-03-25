using UnityEngine;
using static System.Math;

namespace TowerStaff.Ballista
{
    public class BallistaShot : BasicShot
    {
        [SerializeField] private float _speed;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        private int _level;
        private int _health = 1;
        private bool _isBlocked;
        private void Move()
        {
            transform.position += new Vector3(_speed * Time.deltaTime, 0, 0);
            
        }
        private void Start()
        {
            _isBlocked = false;
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Ground"))
            {
                Destroy(gameObject);
            }
        }
        
        private void Update()
        {
             Move();
             if(Abs(transform.position.x) > 20) Destroy(gameObject);
        }
        
        public void SetDirection(bool direction)
        {
            if (direction)
            {
                _speed *= -1;
                _spriteRenderer.flipX = true;
            }
        }
        
        public void TakeDamage()
        {
            _health -= 1;
            if(_health == 0) Destroy(gameObject);
        }
        public void Upgrade()
        {
            _health = 3;
        }
        
        public void Block(bool block)
        {
            _isBlocked = block;
        }
        
        public bool IsBlocked()
        {
            return _isBlocked;
        }
        
        
    }
}
