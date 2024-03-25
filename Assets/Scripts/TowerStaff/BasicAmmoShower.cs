using System;
using ToolTips;
using UnityEngine;
using UnityEngine.AI;

namespace TowerStaff
{
    public class BasicAmmoShower : MonoBehaviour
    {
        [SerializeField] private GameObject _toolTip;
        private GameObject _toolTipPanel;
        private bool _showingAmmo;
        private BasicTowerShooting _towerShooting => GetComponent<BasicTowerShooting>();
        private BasicTowerAmmo _towerAmmo => GetComponent<BasicTowerAmmo>();
        private BasicTowerLevel _towerLevel => GetComponent<BasicTowerLevel>();
        private void Show()
        {
            string tip = "Ammo left: " + _towerAmmo.GetAmmo() + "\n" +
                         "Reload time: " + _towerShooting.GetReloadTime() + "s\n" +
                         "Damage: " + _towerShooting.GetDamage() + "\n" +
                         "Level: " + _towerLevel.GetLevel() + "\n";
            
            _toolTipPanel = GameObject.FindGameObjectWithTag("ToolTip");
            _toolTipPanel.GetComponent<ToolTip>().ShowTooltip(tip, 0, _towerShooting.GetName());
        }

        private void OnMouseEnter()
        {
            Instantiate(_toolTip);
            _showingAmmo = true;
            Show();
        }

        private void Update()
        {
            if (!_showingAmmo) return;
            Show();
            
        }

        private void OnMouseExit()
        {
            Destroy(_toolTipPanel);
            _showingAmmo = false;
        }
    }
}
