using UnityEngine;
using UnityEngine.SceneManagement;

namespace MainMenu
{
    public class MenuMusic : SoundsCont
    {
        private void Start()
        {
            DontDestroyOnLoad(gameObject);
            PlaySound(_sounds[0]);
            if(GameObject.FindWithTag("MenuMusic") != gameObject)
            {
                Destroy(gameObject);
            }
        }

        private void Update()
        {
            if (!_audioSource.isPlaying) PlaySound(_sounds[0]);
            
            if(SceneManager.GetActiveScene().name == "Plains" || SceneManager.GetActiveScene().name == "TwoHills" || SceneManager.GetActiveScene().name == "TwoRivers")
            {
                Destroy(gameObject);
            }
        }
    }
}
