using GameCont;
using TMPro;
using UnityEngine;

namespace MainMenu
{
    public class HighScore : MonoBehaviour
    {
        [SerializeField] private string _levelName;
        private TextMeshProUGUI _highScoreText;

        private void Start()
        {
            _highScoreText = GetComponent<TextMeshProUGUI>();
            _highScoreText.text = "Highscore: " + GameObject.FindWithTag("ScoreKeeper").GetComponent<ScoreKeeper>().GetScore(_levelName);
        }
    }
}

