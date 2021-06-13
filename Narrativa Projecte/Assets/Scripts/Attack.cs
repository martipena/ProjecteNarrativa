using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Narrativa
{
    public class Attack : MonoBehaviour
    {
        public float speed=30;
        public int attackValue=1;
        // Start is called before the first frame update
        void Start()
        {
            StartCoroutine(time());
        }

        // Update is called once per frame
        void Update()
        {
            if (PlayerController.left)
            {
                this.transform.position += new Vector3(-1,0,0) * Time.deltaTime * speed;
            }else if (PlayerController.right)
            {
                this.transform.position += new Vector3(1, 0, 0) * Time.deltaTime * speed;
            }else if (PlayerController.top)
            {
                this.transform.position += new Vector3(0, 1, 0) * Time.deltaTime * speed;
            }else if (PlayerController.down)
            {
                this.transform.position += new Vector3(0, -1, 0) * Time.deltaTime * speed;
            }

        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("enemy"))
            {
               Debug.Log(collision.gameObject.name+" | "+collision.GetComponent<Enemy>().hp);
               collision.GetComponent<Enemy>().getHit(attackValue);
               StopCoroutine(time());
               Destroy(this.gameObject);
            }
        }

        IEnumerator time()
        {
            yield return new WaitForSeconds(0.3f);
            Destroy(this.gameObject);
        }
    }

}
