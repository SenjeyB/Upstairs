using System;
using GameCont;
using UnityEngine;

namespace EnemyAI.Skeleton
{
    public class SkeletonMovement : MonoBehaviour
    {
        private GameObject _player;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        private PlayerInfo _gameInfo;
        private Vector2 _transformMove;
        private float _speed;
        private void Move()
        {
            Vector3 position = transform.position;
            _transformMove = Vector2.MoveTowards(position, _player.transform.position, _speed * Time.deltaTime);
            
            _spriteRenderer.flipX = _transformMove.x < position.x;
            position = _transformMove;
            transform.position = position;
        }
        private void Start()
        {
            _player = GameObject.FindGameObjectWithTag("Player");
            _gameInfo = GameObject.FindGameObjectWithTag("GameController").GetComponent<PlayerInfo>();
            _speed = Mathf.Max(_gameInfo.GetCoefficient() * 0.7f, 1);
        }
        private void Update()
        {
            
            _player.transform.position = _player != null ? _player.transform.position : new Vector3(0, 0, 0);
            Move();
            if(transform.position.y < -5) Destroy(gameObject);
        }

        public void Slow()
        {
            _speed /= 2;
        }
    }
}
