using UnityEngine;

namespace TowerStaff
{
    public class BasicTowerAnimation : MonoBehaviour
    {
        private Animator _animator;
        public bool Shoots { get; set; }
        
        private static readonly int shoots = Animator.StringToHash("Shoots");
        private void Start()
        {
            _animator = GetComponent<Animator>();
        }
        
        private void Update()
        {
            _animator.SetBool(shoots, Shoots);
        }
    }
}
