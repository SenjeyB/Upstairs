using GameCont;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace MainMenu
{
    public class SettingsButton : MonoBehaviour
    {
        public void ToggleSetting(string setting)
        {
            GameObject.FindGameObjectWithTag("ScoreKeeper").GetComponent<ScoreKeeper>().ValueSetting(setting, GetComponent<Slider>().value);
        }
    }
}
