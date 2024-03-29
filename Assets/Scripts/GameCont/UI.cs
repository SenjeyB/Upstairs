using TMPro;
using UnityEngine;

namespace GameCont
{
    public class UI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _health;
        [SerializeField] private TextMeshProUGUI _essence;
        [SerializeField] private TextMeshProUGUI _score;
        [SerializeField] private TextMeshProUGUI _upgradesLeft;
        [SerializeField] private TextMeshProUGUI _time;
        private PlayerInfo _playerInfo;
        void Update()
        {
            _health.text = _playerInfo.GetHealth().ToString();
            _essence.text = _playerInfo.GetEssence().ToString();
            _score.text = "Score: " + _playerInfo.GetScore();
            _upgradesLeft.text = "Upgrade points: " + _playerInfo.GetUpgradesLeft();
            int time = _playerInfo.GetTime();
            _time.text = (time / 60 + ":" + (time % 60) / 10 + (time % 60) % 10).ToString();
        }

        private void Start()
        {
            _playerInfo = GameObject.FindGameObjectWithTag("GameController").GetComponent<PlayerInfo>();
        }
        
    }
}
