using UnityEngine;

namespace PlayerStaff
{
    public class TakingDamage : SoundsCont
    {
        [SerializeField] private int _health;
        [SerializeField] private float _invincibilityTime;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] Material _materialBlink;
        [SerializeField] private GameObject _deathEffect;
        Material _materialDefault;
        private bool _isInvincible;
        private float _endInvincibilityTime;
        
        private void Start()
        {
            _endInvincibilityTime = Time.time + _invincibilityTime;
            _materialDefault = _spriteRenderer.material;
        }
        
        public void Iamdead()
        {
            Instantiate(_deathEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        private void Update()
        {
            if (_health <= 0)
            {
                Iamdead();
            }
            if (!(Time.time >= _endInvincibilityTime))
            {
                return;
            }
            _isInvincible = false;
        }

        private void OnCollisionStay2D(Collision2D collision)
        {
            if (!collision.gameObject.CompareTag("Enemy") || _isInvincible)
            {
                return;
            }

            TakeDamage(1);
        }
        
        private void OnTriggerStay2D(Collider2D collider)
        {
            if (!collider.gameObject.CompareTag("Enemy") || _isInvincible)
            {
                return;
            }
            TakeDamage(1);
        }
        
        private void MaterialReset()
        {
            _spriteRenderer.material = _materialDefault;
        }
        
        public int GetHealth()
        {
            return _health;
        }
        
        public void TakeDamage(int damage)
        {
            PlaySound(_sounds[0]);
            _health -= damage;
            _isInvincible = true;
            _spriteRenderer.material = _materialBlink;
            _endInvincibilityTime = Time.time + _invincibilityTime;
            Invoke(nameof(MaterialReset), 1f);
        }
        
        public void AddHealth(int amount)
        {
            _health += amount;
        }
    }
}
