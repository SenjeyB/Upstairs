using UnityEngine;

namespace PlayerStaff
{
    
    public class PlayerAnimation : MonoBehaviour
    {
        private Animator _animator;
        public bool IsMoving { get; set; }
        
        private static readonly int is_moving = Animator.StringToHash("IsMoving");
        
        private void Start()
        {
            _animator = GetComponent<Animator>();
        }
        
        private void Update()
        {
            _animator.SetBool(is_moving, IsMoving);
        }
    }
}   
