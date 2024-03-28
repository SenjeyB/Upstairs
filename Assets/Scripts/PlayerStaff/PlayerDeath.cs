using System;
using GameCont;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PlayerStaff
{
    public class PlayerDeath : SoundsCont
    {
        private float _deathTime = 2.16f;
        private PlayerInfo _playerInfo;
        private void Start()
        {
            _playerInfo = GameObject.FindGameObjectWithTag("GameController").GetComponent<PlayerInfo>();
            GameObject.FindGameObjectWithTag("ScoreKeeper").GetComponent<ScoreKeeper>().SaveScore(_playerInfo.GetScore());
            PlaySound(_sounds[0], 1f, true);
            Invoke(nameof(PreDeath), _deathTime - 1.05f);
            Invoke(nameof(Death), _deathTime);
        }
        
        private void PreDeath()
        {
            PlaySound(_sounds[1], 1f, true);
        }
        
        private void Death()
        {
            SceneManager.LoadScene("GameOver");
            //Destroy(gameObject);
        }
    }
}
