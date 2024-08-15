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
        [SerializeField] private TextMeshProUGUI _kills;
        private PlayerInfo _playerInfo;
        void Update()
        {
            _health.text = _playerInfo.GetHealth().ToString();
            _essence.text = _playerInfo.GetEssence().ToString();
            _kills.text = _playerInfo.GetKills().ToString();
            _upgradesLeft.text = _playerInfo.GetUpgradesLeft().ToString();
            _score.text = "Score: " + _playerInfo.GetScore();
            int time = _playerInfo.GetTime();
            _time.text = time / 600 + time / 60 % 9 + ":" + time % 60 / 10 + time % 60 % 10;
        }

        private void Start()
        {
            _playerInfo = GameObject.FindGameObjectWithTag("GameController").GetComponent<PlayerInfo>();
        }
        
    }
}
