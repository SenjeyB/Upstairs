using UnityEngine;
using System;
namespace PlayerStaff
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float _speed;
        [SerializeField] private float _jumpForce;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        private PlayerAnimation _animations;
        private Vector3 _input; 
        private Rigidbody2D _rigidbody;
        private bool _isMoving;
        private bool _isOnGround;
    
        private void Move()
        {
            float horizontal = Input.GetAxis("Horizontal");
            _input = new Vector3(horizontal, 0, 0);
            transform.position += _input * (_speed * Time.deltaTime);
            _isMoving = (_input.x != 0);
            if (_isMoving || IsOnAir())
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
