using GameCont;
using UnityEngine;
using UnityEngine.UI;

namespace MainMenu
{
    public class ToggleButton : MonoBehaviour
    {
        [SerializeField] private string _setting;
        
        private void Start()
        {
            GetComponent<Slider>().value = GameObject.FindGameObjectWithTag("ScoreKeeper").GetComponent<ScoreKeeper>().GetValue(_setting);
        }
    }
}
