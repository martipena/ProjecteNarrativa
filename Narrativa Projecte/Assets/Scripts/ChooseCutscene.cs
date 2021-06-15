using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
namespace Narrativa
{
    public class ChooseCutscene : MonoBehaviour
    {
        public Canvas Intro;
        public Canvas Ending;
        public GameObject[] scenes;
        public bool surt=false;
        public int cont = 0;
        // Start is called before the first frame update
        void Start()
        {
            for (int i=0;i<scenes.Length;i++)
            {
                for (int j=0;j< GameData.notAvailableScenes.Count;j++)
                {
                    if (GameData.notAvailableScenes[j] == scenes[i].name)
                    {
                        GameObject g1 = new GameObject();
                        g1.name = "no";
                        scenes[i] = g1;
                    }
                }
                
            }

            if (GameData.nivell=="Nivell1")//Mostra la introduccio
            {
                Intro.gameObject.SetActive(true);
                StartCoroutine(cutsceneDuration(5));
            }else if (GameData.nivell == "Nivell2" || GameData.nivell == "Nivell3")//Mostra les escenes aleatories
            {
                while (surt!=true)
                {
                    int rand = Random.Range(0, scenes.Length);
                    if (scenes[rand].name != "no")
                    {
                        scenes[rand].gameObject.SetActive(true);
                        GameData.notAvailableScenes.Add(scenes[rand].name);
                        scenes[rand] = null;
                        surt = true;
                        StartCoroutine(cutsceneDuration(5));
                    }
                    else
                    {
                        cont++;
                    }
                    if (cont > 8)
                    {
                        surt = true;
                    }
                }
            }else if(GameData.nivell == "Nivell4")
            {
                Ending.gameObject.SetActive(true);
                StartCoroutine(endGame(10));
            }
        }

        IEnumerator cutsceneDuration(float duration)
        {
            yield return new WaitForSeconds(duration);
            SceneManager.LoadScene("generacioJoc");
            //SceneManager.LoadScene("Cutscenes");
        }

        IEnumerator endGame(float duration)
        {
            yield return new WaitForSeconds(duration);
            GameData.nivell = "Nivell1";
            GameData.notAvailableScenes = new List<string>();
            SceneManager.LoadScene("MainMenu");
            //SceneManager.LoadScene("Cutscenes");
        }

    }

}
