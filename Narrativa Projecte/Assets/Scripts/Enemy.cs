using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Narrativa
{
    public class Enemy : MonoBehaviour
    {
        public int hp=3;
        public bool hit;
        public GameObject textDamage;
        public int randNum=0;
        public int speed;
        public bool wall=false;
        public bool coroutine = false;
        public bool canCoroutine = true;
        public bool canWalk = true;
        public float attackTime;
        public GameObject areaAttack;
        public bool salero;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (canWalk)
            {
                Walk();
            }
            
            if (hp <= 0)
            {
                this.gameObject.SetActive(false);
            }
        }
        public void getHit(int value)
        {
            hp=hp-value;
            if (textDamage)
            {
                showDamage(value);
            }
            
        }
        public void wallHit()
        {
            if (randNum == 1)
            {
                randNum = 3;
            }
            else if (randNum == 3)
            {
                randNum = 1;
            }
            else if (randNum == 2)
            {
                randNum = 4;
            }else if (randNum == 4)
            {
                randNum = 2;
            }else if (randNum == 5)
            {
                randNum = 6;
            }else if (randNum == 6)
            {
                randNum = 5;
            }
            StartCoroutine(waitNext());
        }

        public void Attack()
        {
            if (salero)
            {
                gameObject.GetComponentInChildren<Animator>().Play("SaleroAttack");
            }
            else
            {
                gameObject.GetComponentInChildren<Animator>().Play("CuchilloAttack");
            }
            
            StartCoroutine(enemyAttack(attackTime));
        }

        public void Walk()
        {
            if (coroutine == false && canCoroutine == true)
            {
                StartCoroutine(nextMove());
            }

            if (randNum == 0 || randNum>6)
            {
                this.transform.position += new Vector3(0, 0, 0) * Time.deltaTime * speed;
            }
            else if (randNum == 1)
            {
                this.transform.position += new Vector3(1, 0, 0) * Time.deltaTime * speed;//Dreta
            }
            else if (randNum == 2)
            {
                this.transform.position += new Vector3(0, 1, 0) * Time.deltaTime * speed;//Amunt
            }
            else if (randNum == 3)
            {
                this.transform.position -= new Vector3(1, 0, 0) * Time.deltaTime * speed;//Esquerra
            }
            else if (randNum == 4)
            {
                this.transform.position -= new Vector3(0, 1, 0) * Time.deltaTime * speed;//Abaix
            }
            else if (randNum == 5)
            {
                this.transform.position -= new Vector3(1, 1, 0) * Time.deltaTime * speed;//Diagonal abaix
            }
            else if (randNum == 6)
            {
                this.transform.position += new Vector3(1, 1, 0) * Time.deltaTime * speed;//Diagonal amunt
            }

            //this.transform.position += new Vector3(0, 0, 0) * Time.deltaTime;
            //this.transform.position += new Vector3(1, 0, 0) * Time.deltaTime;
            //this.transform.position += new Vector3(0, 1, 0) * Time.deltaTime;
            //this.transform.position -= new Vector3(1, 0, 0) * Time.deltaTime;
            //this.transform.position -= new Vector3(0, 1, 0) * Time.deltaTime;
            //this.transform.position -= new Vector3(1, 1, 0) * Time.deltaTime;

        }

        public void showDamage(int value)
        {
            var text=Instantiate(textDamage, transform.position, Quaternion.identity, transform);
            text.GetComponent<TextMesh>().text = value.ToString();
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.collider.tag == "Player" && PlayerController.canHit)
            {
                collision.collider.GetComponent<PlayerController>().Hit(1);
            }
        }

        IEnumerator nextMove()
        {
            coroutine = true;
            yield return new WaitForSeconds(1f);
            //this.transform.position += new Vector3(0, 0, 0) * Time.deltaTime * speed;
           
            randNum = Random.Range(0, 7);
            
            coroutine = false;
        }

        IEnumerator waitNext()
        {
            canCoroutine = false;
            yield return new WaitForSeconds(1f);
            canCoroutine = true;
        }

        IEnumerator enemyAttack(float attackTime)
        {
            canWalk = false;
            areaAttack.SetActive(true);
            yield return new WaitForSeconds(attackTime);
            areaAttack.SetActive(false);
            canWalk = true;
        }

    }

}
