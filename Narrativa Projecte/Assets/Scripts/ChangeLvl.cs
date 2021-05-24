using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Narrativa
{
    public class ChangeLvl : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == "Player")//Modifica la info pel seguent nivell
            {
                if (GameData.nivell == "Nivell1")
                {
                    GameData.nivell = "Nivell2";
                }else if (GameData.nivell == "Nivell2")
                {
                    GameData.nivell = "Nivell3";
                }else if (GameData.nivell == "Nivell3")
                {
                    GameData.nivell = "Nivell4";
                }
                else if (GameData.nivell == "Nivell4")
                {
                    GameData.nivell = "Nivell5";
                }
                SceneManager.LoadScene("Cutscenes");
            }
        }
    }
}

