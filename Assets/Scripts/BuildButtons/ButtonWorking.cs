using System.Collections.Generic;
using BuildTile;
using GameCont;
using ToolTips;
using UnityEngine;
using TowerType = Enums.TowerType;

namespace BuildButtons
{
    public class ButtonWorking : SoundsCont
    {
        [SerializeField] private Sprite[] _buttonSprite;
        [SerializeField] private Material _materialBlink;
        [SerializeField] private GameObject _toolTip;
        private int _buttonType;
        private SpriteRenderer _spriteRenderer;
        private BuilderTile _builderTile;
        private PlayerInfo _playerInfo;
        private Material _materialDefault;
        private TowerType _towerType;
        private Dictionary<TowerType, int> _costs;
        private Dictionary<TowerType, int> _costsReload;
        private Dictionary<TowerType, int> _costsUpgrade;
        private Dictionary<int, TowerType> _towers;
        private GameObject _toolTipPanel;
        public void SetButtonType(int buttonType)
        {
            _buttonType = buttonType;
        }
        public void SetParentPlatform(GameObject builderTile)
        {
            _builderTile = builderTile.GetComponent<BuilderTile>();
        }

        private void Start()
        {
            
            FillCosts();
            _materialDefault = GetComponent<SpriteRenderer>().material;
            _playerInfo = GameObject.FindWithTag("GameController").GetComponent<PlayerInfo>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _spriteRenderer.sprite = _buttonSprite[_buttonType];
        }


        private void FillCosts()
        {
            _costs = new Dictionary<TowerType, int>
            {
                [TowerType.Ballista] = 8,
                [TowerType.Taser] = 12,
                [TowerType.Catapult] = 16,
                [TowerType.Firecracker] = 16
            };
            _costsUpgrade = new Dictionary<TowerType, int>
            {
                [TowerType.Ballista] = 12,
                [TowerType.Taser] = 15,
                [TowerType.Catapult] = 18,
                [TowerType.Firecracker] = 18
            };
            _costsReload = new Dictionary<TowerType, int>
            {
                [TowerType.Ballista] = 0,
                [TowerType.Taser] = 1,
                [TowerType.Catapult] = 2,
                [TowerType.Firecracker] = 2
            };
            _towers = new Dictionary<int, TowerType>
            {
                [3] = TowerType.Ballista,
                [5] = TowerType.Taser,
                [4] = TowerType.Catapult,
                [6] = TowerType.Firecracker
            };
        }

        private void OnMouseUp()
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Collider2D collider = GetComponent<Collider2D>();
            if (!collider.OverlapPoint(mousePos)) return;
            _spriteRenderer.material = _materialDefault;
            if (_buttonType == 0)
            {
                _playerInfo.ReturnUpgrade(_builderTile.GetLevel() - 1);
                _builderTile.SetTowerType(TowerType.NotSet);
            }
            else if (_buttonType == 1)
            {
                TowerType towerType = _builderTile.GetTowerType();
                if (_playerInfo.GetEssence() >= _costsUpgrade[towerType] * _builderTile.GetLevel() && _builderTile.GetLevel() < 3 && _playerInfo.GetUpgradesLeft() > 0)
                {
                    _builderTile.UpgradeTower(_costsUpgrade[towerType] * _builderTile.GetLevel());
                    
                }
                else
                {
                    PlaySound(_sounds[0]);
                    Debug.Log("Not enough essence");
                }
            }
            else if (_buttonType == 2)
            {
                TowerType towerType = _builderTile.GetTowerType();
                if (_costsReload[towerType] <= _playerInfo.GetEssence())
                {
                    _playerInfo.AddEssence(-_costsReload[towerType]);
                    _builderTile.ReloadTower();
                }
                else
                {
                    PlaySound(_sounds[0]);
                    Debug.Log("Not enough essence");
                }
            }
            else
            {
                if (_playerInfo.GetEssence() >= _costs[_towers[_buttonType]])
                {
                    _playerInfo.AddEssence(-_costs[_towers[_buttonType]]);
                    _builderTile.SetTowerType(_towers[_buttonType]);
                }
                else
                {
                    PlaySound(_sounds[0]);
                    Debug.Log("Not enough essence");
                }
            }
        }
    
        private void OnMouseEnter()
        {
            _spriteRenderer.color = new Color(0.5f, 0.5f, 0.5f);
            Instantiate(_toolTip);
            _toolTipPanel = GameObject.FindGameObjectWithTag("ToolTip");
            _toolTipPanel.GetComponent<ToolTip>().SetParent(gameObject);
            if(_buttonType == 0)
            {
                _toolTipPanel.GetComponent<ToolTip>().ShowTooltip("Destroy tower", 0);
            }
            if(_buttonType == 1)
            {
                
                if (_builderTile.GetLevel() < 3)
                {
                    string tip = "Upgrade tower to level " + (_builderTile.GetLevel() + 1) + "\n";
                    if (_builderTile.GetLevel() == 2)
                    {
                        if (_builderTile.GetTowerType() == TowerType.Ballista)
                        {
                            tip += "Bonus: Bolts pierces through multiple enemies!";
                        }
                        if (_builderTile.GetTowerType() == TowerType.Catapult)
                        {
                            tip += "Bonus: Slows down survived enemies!";
                        }
                        if (_builderTile.GetTowerType() == TowerType.Taser)
                        {
                            tip += "Bonus: Repairs broken tower after shooting!";
                        }
                        if (_builderTile.GetTowerType() == TowerType.Firecracker)
                        {
                            tip += "Bonus: Infinity ammo!";
                        }
                    }
                    _toolTipPanel.GetComponent<ToolTip>().ShowTooltip(tip, _costsUpgrade[_builderTile.GetTowerType()] * _builderTile.GetLevel());
                }
                else
                {
                    _toolTipPanel.GetComponent<ToolTip>().ShowTooltip("Max level", 0);
                }
            }
            if (_buttonType == 2)
            {
                _toolTipPanel.GetComponent<ToolTip>().ShowTooltip("Reload tower ammo or repair it", _costsReload[_builderTile.GetTowerType()]);
            }
            if (_buttonType == 3)
            {
                _toolTipPanel.GetComponent<ToolTip>().ShowTooltip("Ballista shoots fast, deal low damage and have high ammo capacity", _costs[TowerType.Ballista]);
            }
            if (_buttonType == 4)
            {
                _toolTipPanel.GetComponent<ToolTip>().ShowTooltip("Catapult slowly shoots with high damage stones", _costs[TowerType.Catapult]);
            }
            if (_buttonType == 5)
            {
                _toolTipPanel.GetComponent<ToolTip>().ShowTooltip("Taser attacks enemies nearby. Can easily kill rats!", _costs[TowerType.Taser]);
            } 
            if (_buttonType == 6)
            {
                _toolTipPanel.GetComponent<ToolTip>().ShowTooltip("Firecrackers shoots high in the air, dealing damage to enemies above", _costs[TowerType.Firecracker]);
            }
        }
        
        private void OnMouseExit()
        {
            _spriteRenderer.color = new Color(1, 1, 1);
            _spriteRenderer.material = _materialDefault;
            if (_toolTipPanel != null)
            {
                _toolTipPanel.GetComponent<ToolTip>().SetParent(null);
                Destroy(_toolTipPanel.transform.parent.gameObject);
            }
        }

        private void OnMouseDown()
        {
            _spriteRenderer.material = _materialBlink;
        }

        private void OnDestroy()
        {
           if(_toolTipPanel != null) Destroy(_toolTipPanel.transform.parent.gameObject);
        }
    }
}
