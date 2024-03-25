
using TMPro;
using UnityEngine;


namespace ToolTips
{
    public class ToolTip : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _tooltipText;
        [SerializeField] private TextMeshProUGUI _costText;
        private GameObject _parentButton;

        public void ShowTooltip(string tooltip, int cost, string turret = "")
        {
            _tooltipText.text = tooltip;
            _costText.text = turret;
            if (cost != 0)
            {
                _costText.text = "Cost: " + cost;
            }
        }

        public void SetParent(GameObject parentButton)
        {
            _parentButton = parentButton;
        }

        private void Update()
        {
            if(_tooltipText.text == "New Text")
            {
                Destroy(transform.parent.gameObject);
            }
        }
    }
}
