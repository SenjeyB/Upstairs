using PlayerStaff;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameCont
{
    public class PlayerInfo : SoundsCont
    {

        [SerializeField] private int _upgradesLeft;
        [SerializeField] private int _essence;
        [SerializeField] private Font _font;
        private GameObject _player;
        private TakingDamage _playerInfo;
        private float _gameTime;
        private int _score;
        private float _coefficient = 1f;
        protected float _nextUpdate = 60f;
        protected float _nextUpdateTimer;
        public void AddEssence(int amount)
        {
            _essence += amount;
        }
        public int GetEssence()
        {
            return _essence;
        }
        public void AddScore(int amount)
        {
            _score += amount;
        }
        
        public float GetGameTime()
        {
            return Time.time - _gameTime;
        }
        
        private void Start()
        {
            _player = GameObject.FindGameObjectWithTag("Player");
            _gameTime = Time.time;
            _playerInfo = _player.GetComponent<TakingDamage>();
            PlaySound(_sounds[0]);
            //_essence = 13;
            _score = 0;
            _nextUpdateTimer = Time.time + _nextUpdate;
            Invoke(nameof(UpdateScore), 1f);
        }
        
        private void UpdateScore()
        {
            AddScore(1);
            Invoke(nameof(UpdateScore), 1f);
        }

        public int GetHealth()
        {
            return _playerInfo.GetHealth();
        }
        
        /*private void OnGUI()
        {
            GUIStyle style = new GUIStyle();
            style.fontSize = 23;
            style.font = _font;
            style.normal.textColor = Color.white;
            int ypos = Screen.height / 100;
            GUI.Label(new Rect(ypos, ypos, Screen.width, Screen.height), "Health: " + _playerInfo.GetHealth(), style);
            GUI.Label(new Rect(ypos, ypos * 5, Screen.width, Screen.height), "Essence: " + _essence, style);
            GUI.Label(new Rect(ypos, ypos * 9, Screen.width, Screen.height), "Score: " + _score, style);
            GUI.Label(new Rect(ypos, ypos * 14, Screen.width, Screen.height), "Upgrades left: " + _upgradesLeft, style);
            int time = (int)(Time.time - _gameTime);
            GUI.Label(new Rect(Screen.width / 2 - ypos * 4, ypos, Screen.width, 200), (time / 60) / 10 + (time / 60) % 10 + ":" + (time % 60) / 10 + (time % 60) % 10, style);
        }*/

        public int GetTime()
        {
            return (int)(Time.time - _gameTime);
        }
            
        private void Update()
        {
            if(!_audioSource.isPlaying) PlaySound(_sounds[0], 0.8f);
            if (_nextUpdateTimer > Time.time) return;
            _coefficient *= 1.15f;
            _nextUpdateTimer = Time.time + _nextUpdate;
 
            
        }

        public float GetCoefficient()
        {
            return _coefficient;
        }
        
        public int GetUpgradesLeft()
        {
            return _upgradesLeft;
        }
        
        public void Upgrade()
        {
            _upgradesLeft--;
        }
        
        public void ReturnUpgrade(int amount)
        {
            _upgradesLeft += amount;
        }
        
        public int GetScore()
        {
            return _score;
        }
    }
}
