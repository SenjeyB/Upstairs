using System;
using UnityEngine;

namespace EnemyAI.Miner
{
    public class MinerMovement : MonoBehaviour
    {
        [SerializeField] public Transform _player;
        [SerializeField] private float _attackSpeed;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private GameObject _pickaxeHandPrefab;
        [SerializeField] private GameObject _pickaxePrefab;
        private Rigidbody2D _rigidbody;
        public GameObject _pickaxe;
        private bool _isReady;
        private float _nextAttack;
        
        
        private void Start()
        {
            
            _rigidbody = GetComponent<Rigidbody2D>();
            _player = GameObject.FindGameObjectWithTag("Player").transform;
            _isReady = false;
            Invoke(nameof(TakePickaxe), 2f);
            _nextAttack = Time.time + 2f + _attackSpeed;
        }
        
        private void TakePickaxe()
        {
            _pickaxe = Instantiate(_pickaxeHandPrefab, transform.position - new Vector3(-0.1f, 0.1f, 0), Quaternion.identity);
        }
        
        private void Update()
        {
            if (Math.Abs(_rigidbody.velocity.y) < 0.1f && !_isReady)
            {
                
                _isReady = true;
            }
            _spriteRenderer.flipX = _player.transform.position.x < transform.position.x;
            if(transform.position.y < -10) Destroy(this);
            if (Time.time + 0.5f > _nextAttack)
            {
                _pickaxe.GetComponent<PickaxeHand>().GetReady();
                _nextAttack = _attackSpeed + Time.time + 1.7f;
                Invoke(nameof(Attack), 0.5f);
            }
           
        }

        private void Attack()
        {
            GameObject pickaxe = Instantiate(_pickaxePrefab, transform.position, Quaternion.identity);
            pickaxe.GetComponent<PickaxeAttack>().SetDirection(_spriteRenderer.flipX);
            
        }
    }
}
