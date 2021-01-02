using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Terry
{
    public class TitleScreen : MonoBehaviour
    {
        // Start is called before the first frame update
        public void StartButton()
        {
            GameManager.Instance.LoadScene("Intro");
        }

        // Update is called once per frame
        public void ExitGame()
        {
            Application.Quit();
        }

        public void CreditsButton()
        {
            GameManager.Instance.LoadScene("Credits");
        }
    }
}
