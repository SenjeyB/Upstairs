using System;
using GameCont;
using PlayerStaff;
using UnityEngine;

namespace Bonuses
{
    public class Bonus : SoundsCont
    {
        [SerializeField] private Sprite[] _spriteRenderers;
        private SpriteRenderer _spriteRenderer => GetComponent<SpriteRenderer>();
        private int _bonusType;
        
        public void SetBonusType(int bonusType)
        {
            _bonusType = bonusType;
            _spriteRenderer.sprite = _spriteRenderers[_bonusType];
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                if (_bonusType == 0)
                {
                    PlayerInfo playerInfo = GameObject.FindWithTag("GameController").GetComponent<PlayerInfo>();
                    playerInfo.AddEssence(7 * Mathf.FloorToInt(Mathf.Min(5, playerInfo.GetCoefficient() * 2f)));
                }
                if (_bonusType == 1)
                {
                    GameObject.FindWithTag("Player").GetComponent<TakingDamage>().AddHealth(1);
                }
                PlaySound(_sounds[0], 1.0f, true);
                Destroy(gameObject);
            }
        }
        
        private void Update()
        {
            transform.Rotate(0, 0, 1000 * Time.deltaTime);
            if (transform.position.y < -6) Destroy(gameObject);
        }
    }
}
