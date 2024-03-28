using System;
using UnityEngine;
using static System.Math;
namespace TowerStaff.FireCrackers
{
    public class FirecrackersShot : BasicShot
    {
        [SerializeField] private float _speed;
        [SerializeField] private GameObject _batPrefab;

        private Rigidbody2D _rigidbody;
        private Transform _target;
        
        public Transform GetTarget()
        {
            return _target;
        }
        private void Move()
        {
            Vector3 position = _target.position;
            Vector2 direction = (position - transform.position).normalized;
            _rigidbody.MovePosition(Vector2.MoveTowards(_rigidbody.position, position, _speed * Time.deltaTime));
            _rigidbody.rotation = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
        }

        private void MoveUp()
        {
            _rigidbody.MovePosition(_rigidbody.position + Vector2.up * _speed * Time.deltaTime);
            _rigidbody.rotation = Mathf.Atan2(Vector2.up.y, Vector2.up.x) * Mathf.Rad2Deg - 90;
        }
        
        private Transform FindTarget()
        {
            var enemies = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (GameObject enemy in enemies)
            {
                if (enemy.transform.position.y > transform.position.y + 2f && enemy.name == "EnemyBat(Clone)")
                {
                    return enemy.transform;
                }
            }
            return null;
        }
        
        private void Update()
        {
            if(_target == null) _target = FindTarget();
            if (_target == null) MoveUp();
            else Move();
            if(Abs(transform.position.x) > 20 || Abs(transform.position.y) > 5) Destroy(gameObject);
        }

        private void Awake()
        {
            _target = FindTarget();
            if (_target == null)
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Ground") || other.CompareTag("Enemy"))
            {
                Destroy(gameObject);
            }
        }
        
    }
}
