using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Narrativa
{
    public class Enemy : MonoBehaviour
    {
        public int hp=3;
        public bool hit;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (hp == 0)
            {
                this.gameObject.SetActive(false);
            }
        }
        public void getHit(int value)
        {
            hp=hp-value;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.collider.tag == "Player" && PlayerController.canHit)
            {
                Debug.Log("aaa");
                collision.collider.GetComponent<PlayerController>().Hit(1);
            }
        }
    }

}
