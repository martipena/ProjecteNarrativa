using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
namespace Narrativa
{
    public class StartExitgame : MonoBehaviour
    {
        public void StartGame()
        {
            SceneManager.LoadScene("Cutscenes");
        }

        public void ExitGame()
        {
            Application.Quit();
        }
    }
}

