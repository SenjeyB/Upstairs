using System;
using System.Reflection.Emit;
using GameCont;
using TowerStaff;
using TowerStaff.Ballista;
using TowerStaff.Catapult;
using UnityEngine;

namespace EnemyAI
{
    public abstract class EnemyTakingDamage : SoundsCont
    {
        [SerializeField] protected float _health;
        [SerializeField] protected SpriteRenderer _spriteRenderer;
        [SerializeField] protected Material _materialBlink;
        [SerializeField] protected GameObject _deathEffect;
        protected Material _materialDefault;
        protected PlayerInfo _gameInfo;
        

        public float GetHealth()
        {
            return _health;
        }

        protected virtual void Killed()
        {
            
        }
        
        protected virtual void Slowing()
        {
            
        }

        protected void IncreaseHealth()
        {
            _health *= _gameInfo.GetCoefficient();
        }
        
        protected void Dying()
        {
            Killed();
            PlaySound(_sounds[0], 0.5f, true);
            Instantiate(_deathEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        
        protected void TakeDamage(float damage)
        {
            PlaySound(_sounds[0], 0.3f);
            _health -= damage;
            if (_health <= 0f)
            {
                Dying();
            }
            _spriteRenderer.material = _materialBlink;
            Invoke(nameof(MaterialReset), 0.2f);
        }
        protected void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.CompareTag("BallistaShot1") && !other.CompareTag("CatapultShot") && !other.CompareTag("TaserShot") && !other.CompareTag("FirecrackerShot")) return;
            if (other.CompareTag("BallistaShot1"))
            {
                other.GetComponent<BallistaShot>().TakeDamage();
            }
            if (other.CompareTag("CatapultShot"))
            {
                if (other.GetComponent<CatapultShot>().IsSlowing())
                {
                    Slowing();
                }
            }
            TakeDamage(other.GetComponent<BasicShot>().GetDamage());
            if (other.CompareTag("CatapultShot") || other.CompareTag("FirecrackerShot"))
            {
                Destroy(other);
            }
        }
        
        protected void MaterialReset()
        {
            _spriteRenderer.material = _materialDefault;
        }
        
        
    }
}
