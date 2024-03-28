using UnityEngine;
using System;
namespace PlayerStaff
{
    public class PlayerMovement : SoundsCont
    {
        [SerializeField] private float _speed;
        [SerializeField] private float _jumpForce;
        private SpriteRenderer _spriteRenderer;
        private PlayerAnimation _animations;
        private Vector3 _input; 
        private Rigidbody2D _rigidbody;
        private bool _isOnGround;
        private float _nextSoundTime;
        private float _nextSoundTimeReload = 0.7f;
    
        private void Move()
        {
            float horizontal = Input.GetAxis("Horizontal");
            _input = new Vector3(horizontal, 0, 0);
            transform.position += _input * (_speed * Time.deltaTime);
            if (horizontal != 0 && !IsOnAir() && !_audioSource.isPlaying && Time.time > _nextSoundTime)
            {
                PlaySound(_sounds[0], 0.05f);
                _nextSoundTime = Time.time + _nextSoundTimeReload;
            }
            if (horizontal == 0)
            {
                _audioSource.volume = 0f;
                _nextSoundTime = Time.time + _nextSoundTimeReload;
            }
            if (horizontal != 0 || IsOnAir())
            {
                _spriteRenderer.flipX = _input.x < 0;
                _animations.IsMoving = true;
            }
            else    
            {
                _animations.IsMoving = false;
            }
        }
    
        private void Jump()
        {
            _rigidbody.AddForce(transform.up * _jumpForce, ForceMode2D.Impulse);
        }

        private bool IsOnAir()
        {
            
            return Math.Abs(_rigidbody.velocity.y) > 0.1;
        }

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _animations = GetComponent<PlayerAnimation>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _nextSoundTime = Time.time + _nextSoundTimeReload;
        }
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (!IsOnAir())
                {
                    Jump();
                }
            }

            Move();
            
            if (transform.position.y < -5)
            {
                GetComponent<TakingDamage>().Iamdead();
            }
        }
    }
}
