using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Narrativa
{
    public class ContObstacles : MonoBehaviour
    {
        public int cont = 0;
        public bool noForats = false;

        public void Update()
        {
            if (cont >= 5)
            {
                noForats = true;
            }
        }
    }

}
