using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Narrativa
{
    public class slabTexture : MonoBehaviour
    {

        public Material[] slab = new Material[1];
        Renderer rend;


        void Start()
        {
            rend = GetComponent<Renderer>();
            int rand = Random.Range(0, slab.Length);
            rend.material = slab[rand];
        }

    }

}
