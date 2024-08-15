using UnityEngine;

namespace TowerStaff
{
    public class BasicTowerAnimation : MonoBehaviour
    {
        private Animator _animator;
        private SpriteRenderer _spriteRenderer;
        private Sprite _originalSprite;
        public bool Shoots { get; set; }
        
        private static readonly int shoots = Animator.StringToHash("Shoots");
        private void Start()
        {
            _animator = GetComponent<Animator>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _originalSprite = _spriteRenderer.sprite;
        }
        
        private void Update()
        {
            _animator.SetBool(shoots, Shoots);
        }
        
        public void DisableAnimator()
        {
            _animator.enabled = false;
            _spriteRenderer.sprite = _originalSprite;
        }
        
        public void EnableAnimator()
        {
            _animator.enabled = true;
        }
        
        public void RestartAnimation()
        {
            _animator.Play(_animator.GetCurrentAnimatorStateInfo(0).fullPathHash, -1, 0f);
        }
    }
}
