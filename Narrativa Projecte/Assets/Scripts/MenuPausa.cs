using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuPausa : MonoBehaviour
{
    public GameObject menuPausa;
    public GameObject inGameUI;
    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale == 0)
        {
            menuPausa.SetActive(true);
            inGameUI.SetActive(false);
        }
        if (Time.timeScale == 1)
        {
            menuPausa.SetActive(false);
            inGameUI.SetActive(true);
        }
    }


    public void tornarJoc()
    {
        Time.timeScale = 1;
        menuPausa.SetActive(false);
        inGameUI.SetActive(true);
    }

    public void tornarMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void sortirJoc()
    {
        Application.Quit();
    }
}
