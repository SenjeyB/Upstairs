using System;
using GameCont;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PlayerStaff
{
    public class PlayerDeath : MonoBehaviour
    {
        private float _deathTime = 2.16f;
        private PlayerInfo _playerInfo;
        private void Start()
        {
            _playerInfo = GameObject.FindGameObjectWithTag("GameController").GetComponent<PlayerInfo>();
            GameObject.FindGameObjectWithTag("ScoreKeeper").GetComponent<ScoreKeeper>().SaveScore(_playerInfo.GetScore());
            Invoke(nameof(Death), _deathTime);
        }
        
        private void Death()
        {
            SceneManager.LoadScene("GameOver");
            //Destroy(gameObject);
        }
    }
}
