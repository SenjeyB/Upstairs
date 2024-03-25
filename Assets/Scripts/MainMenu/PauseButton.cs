using System;
using GameCont;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MainMenu
{
    public class PauseButton : MonoBehaviour
    {
        [SerializeField] private bool _isPaused;
        [SerializeField] private GameObject _pauseMenu;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (_isPaused)
                {
                    Resume();
                }
                else
                {
                    Pause();
                }
            }
            
        }

        private void Start()
        {
            Resume();
        }

        public void Resume()
        {
            _pauseMenu.SetActive(false);
            Time.timeScale = 1f;
            _isPaused = false;
        }

        private void Pause()
        {
            _pauseMenu.SetActive(true);
            Time.timeScale = 0f;
            _isPaused = true;
        }

        public void LoadMenu()
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene("MainMenu");
        }

    }
}
