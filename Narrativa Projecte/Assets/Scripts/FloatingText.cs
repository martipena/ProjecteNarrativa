using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Narrativa
{
    public class FloatingText : MonoBehaviour
    {
        public float DestroyTime = 3f;
        public Vector3 textAbove = new Vector3(0, 2, 0);
        public Vector3 randomIntensity = new Vector3(0.5f, 0, 0);
        // Start is called before the first frame update
        void Start()
        {
            Destroy(gameObject, DestroyTime);
            transform.localPosition += textAbove;
            transform.localPosition += new Vector3(Random.Range(-randomIntensity.x, randomIntensity.x),
                Random.Range(-randomIntensity.y, randomIntensity.y),
                Random.Range(-randomIntensity.z, randomIntensity.z));
        }

    }

}
