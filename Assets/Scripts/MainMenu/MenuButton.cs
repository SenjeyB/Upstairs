using GameCont;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MainMenu
{
    public class StartButton : MonoBehaviour
    {
        public void StartGame(int difficulty)
        {
            GameObject.FindGameObjectWithTag("ScoreKeeper").GetComponent<ScoreKeeper>().SetDifficulty(difficulty);
            SceneManager.LoadScene("ChooseLevel");
        }
        
        public void PlayAgain()
        {
            string lastScene = GameObject.FindGameObjectWithTag("ScoreKeeper").GetComponent<ScoreKeeper>().GetLastSceneName();
            SceneManager.LoadScene(lastScene);
        }
        
        public void ExitGame()
        {
            Application.Quit();
        }

        public void ChooseScene(string levelName)
        {
            SceneManager.LoadScene(levelName);
        }

    }
}
