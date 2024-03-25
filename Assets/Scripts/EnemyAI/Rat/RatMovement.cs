
using System;
using System.Collections.Generic;
using System.Numerics;
using BuildTile;
using GameCont;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

namespace EnemyAI.Rat
{
    public class RatMovement : MonoBehaviour
    {
        [SerializeField] public GameObject _turret;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        private PlayerInfo _gameInfo;
        private Vector2 _transformMove;
        private float _speed;
        private bool _jumped;
        private Rigidbody2D _rigidbody;
        private void Move()
        {
            Vector3 position = transform.position;
            _transformMove = Vector2.MoveTowards(position, _turret.transform.position, _speed * Time.deltaTime);
            _spriteRenderer.flipX = _transformMove.x < position.x;
            position = _transformMove;
            transform.position = position;
        }
        private void Start()
        {
            _gameInfo = GameObject.FindGameObjectWithTag("GameController").GetComponent<PlayerInfo>();
            _speed = Mathf.Max(_gameInfo.GetCoefficient() * 0.8f, 1);
            _rigidbody = GetComponent<Rigidbody2D>();
            FindNearestTurret();
        }
        private void Update()
        {
            Move();
            if(!_jumped && Vector3.Distance(transform.position, _turret.transform.position) < 1.5f)
            {
                _jumped = true;
                _rigidbody.AddForce(transform.up * 5f, ForceMode2D.Impulse);
            }
            if(transform.position.y < -5) Destroy(gameObject);
        }
        
        private void FindNearestTurret()
        {
            var turrets = GameObject.FindGameObjectsWithTag("BuilderTile");
            var availableTurrets = new List<GameObject>();
            foreach (GameObject turret in turrets)
            {
                if (turret.GetComponent<BuilderTile>().GetTowerType() == Enums.TowerType.NotSet || turret.GetComponent<BuilderTile>().IsRatted())
                {
                    continue;
                }
                availableTurrets.Add(turret);
            }
            if (availableTurrets.Count > 0)
            {
                _turret = availableTurrets[Random.Range(0, availableTurrets.Count)];
                _turret.GetComponent<BuilderTile>().GetRatted();
            }
            else
            {
                Destroy(gameObject);
            }
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject != _turret) return;
            _turret.GetComponent<BuilderTile>().SetRatted();
            Destroy(gameObject);
        }
        
        public void Slow()
        {
            _speed /= 2;
        }
        
        public GameObject GetTurret()
        {
            return _turret;
        }
    }
}
