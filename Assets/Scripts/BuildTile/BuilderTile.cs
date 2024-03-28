using System;
using System.Collections.Generic;
using BuildButtons;
using GameCont;
using TowerStaff;
using TowerStaff.Taser;
using UnityEngine;
using TowerType = Enums.TowerType; 
namespace BuildTile
{
    public class BuilderTile : MonoBehaviour
    {
        [SerializeField] private GameObject _buildButton;
        [SerializeField] private GameObject _ballistaTower;
        [SerializeField] private GameObject _catapultTower;
        [SerializeField] private GameObject _taserTower;
        [SerializeField] private GameObject _firecrackerTower;
        private SpriteRenderer _spriteRenderer;
        private PlayerInfo _playerInfo;
        private bool _isRatted;
        private bool _isBroken;
        private GameObject _myTower;
        private TowerType _towerType;
        private bool _showing;
        private bool _canShowing;
        private Dictionary<TowerType, GameObject> _towers;

        private void DrawShop()
        {
            const int numberOfTurrets = 4;
            const float buttonSpacing = 2f;
            for (int i = 0; i < numberOfTurrets; i++)
            {
                GameObject button = Instantiate(_buildButton);
                button.transform.position = new Vector3(i * buttonSpacing - 3, 3, 0);
                button.GetComponent<ButtonWorking>().SetButtonType(i + 3);
                button.GetComponent<ButtonWorking>().SetParentPlatform(gameObject);
            }
        }

        private void DrawUpgrade()
        {
            const float buttonSpacing = 2f;
            for (int i = 0; i < 3; i++)
            {
                GameObject button = Instantiate(_buildButton);
                button.transform.position = new Vector3(i * buttonSpacing - 2, 3, 0);
                button.GetComponent<ButtonWorking>().SetButtonType(i);
                button.GetComponent<ButtonWorking>().SetParentPlatform(gameObject);
            }
        }

        private static void DestroyDraw()
        {
            var buttons = GameObject.FindGameObjectsWithTag("BuildButton");
            foreach (GameObject button in buttons)
            {
                Destroy(button);
            }
        }

        private void ShopActivation()
        {
            if (!_showing)
            {
                _showing = true;
                if (_towerType == TowerType.NotSet)
                {
                    DrawShop();
                }
                else
                {
                    DrawUpgrade();
                }
            }
            else
            {
                _showing = false;
                DestroyDraw();
            }
        }
        
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if(!other.CompareTag("Player")) return;
            _canShowing = true;
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (!other.CompareTag("Player")) return;
            if(_showing) DestroyDraw();
            _showing = false;
            _canShowing = false;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                if (_canShowing) ShopActivation();
            }
        }
        
        public void SetTowerType(TowerType towerType)
        {
            _towerType = towerType;
            if (_towerType == TowerType.NotSet)
            {
                _spriteRenderer.enabled = true;
                _spriteRenderer.gameObject.SetActive(true);
                RepairTower();
                Destroy(_myTower);
                DestroyDraw();
                DrawShop();
            }
            else 
            {
                _spriteRenderer.enabled = false;
                Vector3 transform1 = transform.position;
                _myTower = Instantiate(_towers[_towerType], new Vector3(transform1.x, transform1.y + 0.68f, 0), Quaternion.identity);
                _myTower.GetComponent<BasicTowerShooting>().SetBuilderTile(gameObject);
                DestroyDraw();
                DrawUpgrade();
            }
        }

        public void UpgradeTower(int cost)
        {
            if (_myTower.GetComponent<BasicTowerLevel>().GetLevel() < 3)
            { 
                _playerInfo.AddEssence(-cost);
                _playerInfo.Upgrade();
                _myTower.GetComponent<BasicTowerLevel>().Upgrade();
                DestroyDraw();
                DrawUpgrade();
            }
            else
            {
                Debug.Log("Max level");
            }
        }

        public TowerType GetTowerType()
        {
            
            return _towerType;
        }

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            SetTowerType(TowerType.NotSet);
            DestroyDraw();
        }

        private void Start()
        {
            _showing = false;
            _canShowing = false;
            _isRatted = false;
            _isBroken = false;
            _playerInfo = GameObject.FindWithTag("GameController").GetComponent<PlayerInfo>();
            _towers = new Dictionary<TowerType, GameObject>()
            {
                [TowerType.Ballista] = _ballistaTower,
                [TowerType.Catapult] = _catapultTower,
                [TowerType.Taser] = _taserTower,
                [TowerType.Firecracker] = _firecrackerTower
            };
        }
        
        public int GetLevel()
        {
            return _towerType != TowerType.NotSet ? _myTower.GetComponent<BasicTowerLevel>().GetLevel() : 0;
        }
        
        public void ReloadTower()
        {
            RepairTower();
            _myTower.GetComponent<BasicTowerAmmo>().AddAmmo();
        }

        public void RepairTower()
        {
            _isRatted = false;
            _isBroken = false;
        }
        public bool IsRatted()
        {
            return _isRatted;
        }
        
        public void GetRatted()
        {
            _isRatted = true;
        }
        
        public void SetRatted()
        {
            _isBroken = true;
        }
        
        public bool IsBroken()
        {
            return _isBroken;
        }
    }
}
