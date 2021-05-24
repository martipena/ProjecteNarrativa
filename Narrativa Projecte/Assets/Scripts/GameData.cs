using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Narrativa
{
    public class GameData : MonoBehaviour
    {
        public static string nivell = "Nivell1";
        //public static string[] notAvailableScenes = { };
        public static List<string> notAvailableScenes;
        public void Start()
        {
            if (notAvailableScenes != null)
            {

            }
            else
            {
                notAvailableScenes = new List<string>();
            }
            
            //Debug.Log(notAvailableScenes[0]);
        }
    }

}
