using System;
using UnityEngine;

namespace BackGround
{
    public class MoveBg : MonoBehaviour
    {
        [SerializeField] private float _speed;
        [SerializeField] private MeshRenderer _meshRenderer;
        
        private Vector2 _offset;
        private void Start()
        {
            _meshRenderer.sortingOrder = -20;
        }

        private void Update()
        {
            float y = Mathf.Repeat(Time.time * _speed, 1);
            _meshRenderer.sharedMaterial.mainTextureOffset = new Vector2(_offset.x, y);
        }
        
    }
}
