using System;
using UnityEngine;
using static System.Math;
namespace TowerStaff.Catapult
{
    public class CatapultShot : BasicShot
    {
        [SerializeField] private float _speed;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        private bool _slowing;
        private int _direction = -1;
        private Rigidbody2D _rigidbody;
        private void Move()
        {
            transform.position += new Vector3(_speed * Time.deltaTime, 0, 0);
            transform.Rotate(0, 0, 1000 * Time.deltaTime * _direction);
        }
        
        private void Update()
        {
            Move();
            if(Abs(transform.position.x) > 20 || transform.position.y < -5) Destroy(gameObject);
        }

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _rigidbody.AddForce(transform.up * 8.7f, ForceMode2D.Impulse);
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Ground") || other.CompareTag("Enemy"))
            {
                Destroy(gameObject);
            }
        }

        public void SetDirection(bool direction)
        {
            if (direction)
            {
                _speed *= -1;
                _direction = 1;
                _spriteRenderer.flipX = true;
            }
        }
        
        public void Upgrade()
        {
            _slowing = true;
        }
        
        public bool IsSlowing()
        {
            return _slowing;
        }
        
    }
}
