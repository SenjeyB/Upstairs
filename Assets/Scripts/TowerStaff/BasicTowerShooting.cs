using System;
using BuildTile;
using TowerStaff.Taser;
using UnityEngine;

namespace TowerStaff
{
    public class BasicTowerShooting : SoundsCont
    {
        [SerializeField] protected GameObject _projectilePrefab;
        [SerializeField] protected float _reloadSpeed;
        [SerializeField] protected SpriteRenderer _spriteRenderer;
        [SerializeField] protected Material _materialOff;
        [SerializeField] protected int _ammoCount;
        [SerializeField] protected float _damage;
        [SerializeField] protected string _name;
        private BasicTowerAnimation _animations;
        protected Material _materialDefault;
        protected float _nextFireTime;
        protected BasicTowerLevel _level;
        protected BasicTowerAmmo _ammo;
        protected BuilderTile _builderTile;
        
        public string GetName()
        {
            return _name;
        }
        public float GetDamage()
        {
            return _damage;
        }
        protected virtual void Shoot()
        {
            
        }
        
        public virtual void Upgrade()
        {

        }
        
        public float GetReloadTime()
        {
            return _reloadSpeed;
        }   
        
        public void SetBuilderTile(GameObject builderTile)
        {
            _builderTile = builderTile.GetComponent<BuilderTile>();
        }
        protected virtual void Update()
        {
            if (_ammo.GetAmmo() <= 0 || _builderTile.IsBroken())
            {
                _animations.Shoots = false;
                _spriteRenderer.material = _materialOff;
                return;
            }
            _spriteRenderer.material = _materialDefault;
            _animations.Shoots = !(Time.time < _nextFireTime - 0.3f);
            ShootingSide();
            if (Time.time < _nextFireTime)
            {
                return;
            }
            Shoot();
        }

        // ReSharper disable Unity.PerformanceAnalysis
        protected virtual void ShootingSide()
        {
            var enemies = GameObject.FindGameObjectsWithTag("Enemy");
            GameObject closestEnemy = null;
            float closestDistance = float.MaxValue;

            foreach (GameObject enemy in enemies)
            {
                if (enemy.transform.position.y < transform.position.y - 1f)
                {
                    continue;
                }
                float distance = Vector2.Distance(transform.position, enemy.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestEnemy = enemy;
                }
            }
            if (closestEnemy == null)
            {
                return;
            }
            if (closestEnemy.transform.position.x >= transform.position.x && !_spriteRenderer.flipX)
            {
                return;
            }
            if (closestEnemy.transform.position.x <= transform.position.x && _spriteRenderer.flipX)
            {
                return;
            }

            _spriteRenderer.flipX = !_spriteRenderer.flipX;
        }
        protected virtual void Start()
        {
            _ammo = GetComponent<BasicTowerAmmo>();
            _level = GetComponent<BasicTowerLevel>();
            _animations = GetComponent<BasicTowerAnimation>();
            _level.SetLevel(1);
            _materialDefault = _spriteRenderer.material;
            _nextFireTime = Time.time + _reloadSpeed;
        }

        protected void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.CompareTag("TaserShot")) return;
            if (other.GetComponent<TaserShot>().IsRepairing() && _builderTile.IsBroken())
            {
                _builderTile.RepairTower();
            }
        }

        public void Reload()
        {
            _nextFireTime = Time.time + _reloadSpeed;
        }
        
        public int GetAmmoCount()
        {
            return _ammoCount;
        }
        

    }
}
