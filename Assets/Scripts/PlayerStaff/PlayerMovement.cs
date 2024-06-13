using UnityEngine;
using System;
using System.Collections;

namespace PlayerStaff
{
    public class PlayerMovement : SoundsCont
    {
        [SerializeField] private float _speed;
        [SerializeField] private float _jumpForce;
        [SerializeField] private float _dashRechargeTime;
        [SerializeField] private float _dashInvincibilityTime;
        private SpriteRenderer _spriteRenderer;
        private PlayerAnimation _animations;
        private Vector3 _input; 
        private Rigidbody2D _rigidbody;
        private TakingDamage _takingDamage;
        private bool _isOnGround;
        private float _nextSoundTime;
        private float _nextSoundTimeReload = 0.7f;
        private float _nextDashTime;
    
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
            if (horizontal != 0)
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
            if (!IsOnAir())
            {
                _rigidbody.AddForce(transform.up * _jumpForce, ForceMode2D.Impulse);
            }
        }

        private bool IsOnAir()
        {
            
            return Math.Abs(_rigidbody.velocity.y) > 0;
        }

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _animations = GetComponent<PlayerAnimation>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _takingDamage = GetComponent<TakingDamage>();
            _nextSoundTime = Time.time + _nextSoundTimeReload;
        }
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Jump();
            }
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                Dash();
            }
            Move();
            if (transform.position.y < -5)
            {
                GetComponent<TakingDamage>().Iamdead();
            }
        }
        
        private void Dash()
        {
            if (Time.time < _nextDashTime)
            {
                return;
            }
            _rigidbody.AddForce(transform.right * (4 * (_spriteRenderer.flipX ? -1 : 1)), ForceMode2D.Impulse);
            _nextDashTime = Time.time + _dashRechargeTime;
            if (IsOnAir())
            {
                StartCoroutine(DashEffect());
            }
            _takingDamage.IFramesProc(_dashInvincibilityTime);
        }
        
        private IEnumerator DashEffect()
        {
            Vector3 originalScale = transform.localScale;
            Vector3 targetScale = new Vector3(originalScale.x, originalScale.y * 0.55f, originalScale.z);

            float time = 0;
            while (time <= 1)
            {
                transform.localScale = Vector3.Lerp(originalScale, targetScale, time);
                time += Time.deltaTime * 10; // 2 - скорость сжатия
                yield return null;
            }
            yield return new WaitUntil(() => !IsOnAir());
            time = 0;
            while (time <= 1)
            {
                transform.localScale = Vector3.Lerp(targetScale, originalScale, time);
                time += Time.deltaTime * 20; // 2 - скорость возвращения в исходное состояние
                yield return null;
            }
        }
    }
}
