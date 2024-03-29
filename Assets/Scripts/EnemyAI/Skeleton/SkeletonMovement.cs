using System;
using GameCont;
using UnityEngine;

namespace EnemyAI.Skeleton
{
    public class SkeletonMovement : SoundsCont
    {
        private GameObject _player;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        private PlayerInfo _gameInfo;
        private Vector2 _transformMove;
        private float _speed;
        private Rigidbody2D _rigidbody;
        private float _nextSoundTime;
        private float _nextSoundTimeReload = 0.5f;
        private void Move()
        {
            Vector3 position = transform.position;
            _transformMove = Vector2.MoveTowards(position, _player.transform.position, _speed * Time.deltaTime);
            _spriteRenderer.flipX = _transformMove.x < position.x;
            transform.position = _transformMove;
            if (Math.Abs(_rigidbody.velocity.y) <= 0.1 && !_audioSource.isPlaying && Time.time > _nextSoundTime)
            {
                PlaySound(_sounds[0], 0.05f);
                _nextSoundTime = Time.time + _nextSoundTimeReload;
            }
        }
        private void Start()
        {
            _player = GameObject.FindGameObjectWithTag("Player");
            _gameInfo = GameObject.FindGameObjectWithTag("GameController").GetComponent<PlayerInfo>();
            _speed = Mathf.Max(_gameInfo.GetCoefficient() * 0.4f, 1);
            _rigidbody = GetComponent<Rigidbody2D>();
            _nextSoundTime = Time.time + _nextSoundTimeReload;
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
