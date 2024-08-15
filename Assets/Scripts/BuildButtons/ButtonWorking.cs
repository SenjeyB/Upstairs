using System;
using System.Collections.Generic;
using BuildTile;
using GameCont;
using MainMenu;
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
        private Dictionary<TowerType, String> _towerTips;
        private Dictionary<TowerType, String> _towerBonuses;
        private Dictionary<int, TowerType> _towers;
        private GameObject _toolTipPanel;
        private PauseButton _pauseButton;
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
            _pauseButton = GameObject.FindWithTag("UI").GetComponent<PauseButton>();
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
                [TowerType.Firecracker] = 12,
                [TowerType.Mine] = 1,
                [TowerType.EssenceGetter] = 8
            };
            _costsUpgrade = new Dictionary<TowerType, int>
            {
                [TowerType.Ballista] = 12,
                [TowerType.Taser] = 15,
                [TowerType.Catapult] = 18,
                [TowerType.Firecracker] = 15,
                [TowerType.Mine] = 1,
                [TowerType.EssenceGetter] = 10
            };
            _costsReload = new Dictionary<TowerType, int>
            {
                [TowerType.Ballista] = 0,
                [TowerType.Taser] = 1,
                [TowerType.Catapult] = 2,
                [TowerType.Firecracker] = 2,
                [TowerType.Mine] = 0,
                [TowerType.EssenceGetter] = 0
                
            };
            _towers = new Dictionary<int, TowerType>
            {
                [3] = TowerType.Ballista,
                [5] = TowerType.Taser,
                [4] = TowerType.Catapult,
                [6] = TowerType.Firecracker,
                [7] = TowerType.Mine,
                [8] = TowerType.EssenceGetter
            };
            _towerTips = new Dictionary<TowerType, String>
            {
                [TowerType.Ballista] = "Ballista shoots fast, deals low damage and has high ammo capacity",
                [TowerType.Catapult] = "Catapult slowly shoots with high damage stones",
                [TowerType.Taser] = "Taser attacks enemies nearby. Can easily kill rats!",
                [TowerType.Firecracker] = "Firecrackers shoots high in the air, dealing damage to enemies above",
                [TowerType.Mine] = "Mine explodes when enemy steps on it",
                [TowerType.EssenceGetter] = "Essence getter extracts essence from air"
            };
            _towerBonuses = new Dictionary<TowerType, String>
            {
                [TowerType.Ballista] = "Bonus: Bolts pierces through multiple enemies!",
                [TowerType.Catapult] = "Bonus: Slows down survived enemies!",
                [TowerType.Taser] = "Bonus: Repairs broken tower after shooting!",
                [TowerType.Firecracker] = "Bonus: Infinity ammo!",
                [TowerType.Mine] = "",
                [TowerType.EssenceGetter] = "Bonus: Unbreakable!"
            };
        }

        private void OnMouseUp()
        {
            if(_pauseButton.IsPaused()) return;
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
                    _builderTile.ReloadTower();
                    _playerInfo.AddScore(_costsUpgrade[towerType] * _builderTile.GetLevel());
                }
                else
                {
                    PlaySound(_sounds[0]);
                    //Debug.Log("Not enough essence");
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
                    //Debug.Log("Not enough essence");
                }
            }
            else
            {
                if (_playerInfo.GetEssence() >= _costs[_towers[_buttonType]])
                {
                    _playerInfo.AddEssence(-_costs[_towers[_buttonType]]);
                    _builderTile.SetTowerType(_towers[_buttonType]);
                    _playerInfo.AddScore(_costs[_towers[_buttonType]]);
                }
                else
                {
                    PlaySound(_sounds[0]);
                    //Debug.Log("Not enough essence");
                }
            }
        }
    
        private void OnMouseEnter()
        {
            if(_pauseButton.IsPaused()) return;
            _spriteRenderer.color = new Color(0.5f, 0.5f, 0.5f);
            Instantiate(_toolTip);
            _toolTipPanel = GameObject.FindGameObjectWithTag("ToolTip");
            _toolTipPanel.GetComponent<ToolTip>().SetParent(gameObject);
            if(_buttonType == 0)
            {
                _toolTipPanel.GetComponent<ToolTip>().ShowTooltip("Destroy tower", 0);
            }
            else if(_buttonType == 1)
            {
                if (_builderTile.GetLevel() < 3)
                {
                    string tip = "Upgrade tower to level " + (_builderTile.GetLevel() + 1) + "\n";
                    if (_builderTile.GetLevel() == 2)
                    {
                        tip += _towerBonuses[_builderTile.GetTowerType()];
                        /*if (_builderTile.GetTowerType() == TowerType.Ballista)
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
                        if (_builderTile.GetTowerType() == TowerType.EssenceGetter)
                        {
                            tip += "Bonus: Unbreakable!";
                        }*/
                    }
                    _toolTipPanel.GetComponent<ToolTip>().ShowTooltip(tip, _costsUpgrade[_builderTile.GetTowerType()] * _builderTile.GetLevel());
                }
                else
                {
                    _toolTipPanel.GetComponent<ToolTip>().ShowTooltip("Max level", 0);
                }
            }
            else if (_buttonType == 2)
            {
                _toolTipPanel.GetComponent<ToolTip>().ShowTooltip("Reload tower ammo or repair it", _costsReload[_builderTile.GetTowerType()]);
            }
            else
            {
                _toolTipPanel.GetComponent<ToolTip>().ShowTooltip(_towerTips[_towers[_buttonType]], _costs[_towers[_buttonType]]);
            }
            /*if (_buttonType == 3)
            {
                _toolTipPanel.GetComponent<ToolTip>().ShowTooltip("Ballista shoots fast, deals low damage and has high ammo capacity", _costs[TowerType.Ballista]);
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
            if (_buttonType == 7)
            {
                _toolTipPanel.GetComponent<ToolTip>().ShowTooltip("Mine explodes when enemy steps on it", _costs[TowerType.Mine]);
            }
            if (_buttonType == 8)
            {
                _toolTipPanel.GetComponent<ToolTip>().ShowTooltip("Essence getter extracts essence from air", _costs[TowerType.EssenceGetter]);
            }*/
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
            if(_pauseButton.IsPaused()) return;
            _spriteRenderer.material = _materialBlink;
        }

        private void OnDestroy()
        {
           if(_toolTipPanel != null) Destroy(_toolTipPanel.transform.parent.gameObject);
        }
    }
}
