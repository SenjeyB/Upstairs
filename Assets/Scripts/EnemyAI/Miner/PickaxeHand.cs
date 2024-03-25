using System;
using UnityEngine;

namespace EnemyAI.Miner
{
    public class PickaxeHand : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;
        private bool _isReady;
        private Transform _player;
        private void Start()
        {
            _isReady = false;
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _player = GameObject.FindGameObjectWithTag("Player").transform;
        }
        
        public void GetReady()
        {
            _isReady = true;
            _spriteRenderer.flipX = !_spriteRenderer.flipX;
            transform.position += new Vector3(0, 0.2f, 0);
            Invoke(nameof(Hide), 0.5f);
        }

        private void Hide()
        {
            _isReady = false;
            _spriteRenderer.flipX = !_spriteRenderer.flipX;
            transform.position -= new Vector3(0, 0.2f, 0);
            _spriteRenderer.color  = new Color(1f, 1f, 1f, 0f);
            Invoke(nameof(Show), 2.4f);
        }
        
        private void Show()
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, 0);
            _spriteRenderer.color = new Color(1f, 1f, 1f, 1f);
        }

        private void Update()
        {
            if (_player != null)
            {
                if (!_isReady)
                {
                    _spriteRenderer.flipX = _player.position.x < transform.position.x;
                }
                else
                {
                    _spriteRenderer.flipX = _player.position.x >= transform.position.x;
                }
            }
        }
    }
}
