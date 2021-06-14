using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Narrativa
{
    public class DetectWall : MonoBehaviour
    {
        public bool touchedWall=false;
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
            if (collision.gameObject.tag == "wall" && touchedWall==false)
            {
                touchedWall = true;
                gameObject.GetComponentInParent<Enemy>().wallHit();
                StartCoroutine(wait());
            }
            if (collision.gameObject.tag == "Player")
            {
                gameObject.GetComponentInParent<Enemy>().Attack();
            }
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "wall" && touchedWall==false)
            {
                touchedWall = true;
                gameObject.GetComponentInParent<Enemy>().wallHit();
                StartCoroutine(wait());
            }
        }

        IEnumerator wait()
        {
            yield return new WaitForSeconds(1f);
            touchedWall = false;
        }
    }

}
