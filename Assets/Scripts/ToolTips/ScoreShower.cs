using GameCont;
using TMPro;
using UnityEngine;

namespace ToolTips
{
    public class ScoreShower : MonoBehaviour
    {
        private TextMeshProUGUI _scoreText;
        private ScoreKeeper _scoreKeeper;
        private void Start()
        {
            _scoreText = GetComponent<TextMeshProUGUI>();
            _scoreKeeper = GameObject.FindGameObjectWithTag("ScoreKeeper").GetComponent<ScoreKeeper>();
            _scoreText.text = "Score: " + _scoreKeeper.GetLastScore() + "\n";
            _scoreText.text += "Highscore: " + _scoreKeeper.GetScore(_scoreKeeper.GetLastSceneName());
        }
    }
}
