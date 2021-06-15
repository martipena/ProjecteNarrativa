using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Narrativa
{
    public class PlayerController : MonoBehaviour
    {
        public float moveSpeed = 5f;
        public Rigidbody2D rb;
        private Vector2 moveDirection;
        public LevelGeneration levelGen;
        public Image loadImage;
        public Text loadText;
        public GameObject UI;
        public bool potIniciar=false;
        public bool test = false;
        public bool canShoot = true;
        public float time;
        public GameObject bullet;
        public static Vector2 pos;
        public static bool left;
        public static bool right;
        public static bool down=true;
        public static bool top;
        public int hp;
        public static bool canHit = true;
        public Animator anim;
        public SpriteRenderer [] mySpriteRender = new SpriteRenderer[0];
        public bool rotadoA = false;
        public bool rotadoD = false;
        public bool primerclick = false;
        public static int attackValue=1;
        public Text hpUi;
        void Start()
        {
            for (int i = 0; i < mySpriteRender.Length; i++)
            {
                mySpriteRender[i].GetComponent<SpriteRenderer>().gameObject.SetActive(false);
            }
        }
        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape) && Time.timeScale == 1)//pausa
            {
                Time.timeScale = 0;
            }
            else if (Input.GetKeyDown(KeyCode.Escape) && Time.timeScale == 0)
            {
                Time.timeScale = 1;
            }
            hpUi.text = hp.ToString();
            if (test == true){//Inici joc en zona de test
                ProcessInputs();
            }
            else//Inici joc normal
            {
                if (levelGen.stopGeneration)
                {
                    StartCoroutine(inicia());
                }
                else
                {
                    loadImage.enabled = true;
                    loadText.enabled = true;
                    UI.SetActive(false);
                }
                if (potIniciar)
                {
                    loadImage.enabled = false;
                    loadText.enabled = false;
                    UI.SetActive(true);
                    ProcessInputs();
                }

            }

            
                if (Input.GetKey(KeyCode.D))
                {
                    anim.SetBool("Change", true);
                anim.SetBool("ChangeInvert", false);
                right = true;
                rotadoA = false;

                if(rotadoD == false && primerclick == false)
                {
                    rotadoD = true;
                    primerclick = true;
                    }
                else if (rotadoD == false && primerclick == true)
                {
                    transform.Rotate(new Vector3(0, -180, 0));
                    rotadoD = true;
                    primerclick = true;
                }
            }

            else if (Input.GetKey(KeyCode.A))
                {
                anim.SetBool("Change", false);
                anim.SetBool("ChangeInvert", true);
                   left = true;
                rotadoD = false;

                if(rotadoA == false && primerclick == false)
                {
                    transform.Rotate(new Vector3(0, 180, 0));
                    rotadoA = true;
                    primerclick = true;

                } else if (rotadoA == false && primerclick == true)
                {
                    transform.Rotate(new Vector3(0, -180, 0));
                    rotadoA = true;
                    primerclick = true;
                }
                
            }
                else if (Input.GetKey(KeyCode.W))
                {
                    anim.SetBool("Change", true);
                anim.SetBool("ChangeInvert", false);
                top = true;
                primerclick = true;
            }
                else if (Input.GetKey(KeyCode.S))
                {
                anim.SetBool("Change", false);
                anim.SetBool("ChangeInvert", true);
                down = true;
                primerclick = true;

            }
                else if (pos.x == 0 && pos.y == 0)
                {
                anim.SetBool("Change", false);
                anim.SetBool("ChangeInvert", false);
                transform.Rotate(new Vector3(0, 0, 0));
            }
            if (Input.GetKeyUp(KeyCode.Space))
            {
                moveSpeed = 5f;
            }
            //Atacs
            if (Input.GetKey(KeyCode.Space) && canShoot==true)
            {
                moveSpeed = 4f;
                pos.x = 0;
                pos.y = 0;
                if(moveDirection.x==0 && moveDirection.y == 0)
                {

                }
                else
                {
                    left = false;
                    right = false;
                    down = false;
                    top = false;
                }
                
                if (moveDirection.x != 0)
                {
                    pos.x = moveDirection.x;
                }
                else if (moveDirection.y != 0)
                {
                    pos.y = moveDirection.y;
                }
                canShoot = false;
                StartCoroutine(nextShot(time));
                Instantiate(bullet, new Vector3(this.transform.position.x, this.transform.position.y, 0), Quaternion.identity);
                
            }
            if (hp <= 0)
            {
                this.gameObject.SetActive(false);

            }
            
        }

        void FixedUpdate()
        {
            Move();
        }

        void ProcessInputs()
        {
            float moveX = Input.GetAxisRaw("Horizontal");
            float moveY = Input.GetAxisRaw("Vertical");
            moveDirection = new Vector2(moveX, moveY);
        }

        void Move()
        {
            rb.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
        }

        public void Hit(int value)
        {
            hp = hp - value;
            canHit = false;
            StartCoroutine(nextHit(1f));
        }

        public void Heal()
        {
            if (hp < 10)
            {
                hp += 1;
            }
            else
            {

            }
            
        }
        public void increaseAttack()
        {
            if (attackValue < 4)
            {
                attackValue += 1;
            }
            else
            {

            }
        }

        public void increaseAttackSpeed()
        {
            if (time > 0.2)
            {
                time -= 0.1f;
            }
            else
            {

            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "heal")
            {
                collision.gameObject.SetActive(false);
                Heal();
            }
            if (collision.gameObject.tag == "attackUp")
            {
                collision.gameObject.SetActive(false);
                increaseAttack();
            }
            if (collision.gameObject.tag == "speedUpAttack")
            {
                collision.gameObject.SetActive(false);
                Debug.Log("tocat");
                increaseAttackSpeed();
            }
        }

        IEnumerator inicia()
        {
            
            yield return new WaitForSeconds(1.5f);
            
            for (int i = 0; i < mySpriteRender.Length; i++)
            {
                mySpriteRender[i].GetComponent<SpriteRenderer>().gameObject.SetActive(true);
            }
            potIniciar = true;
        }

        IEnumerator nextShot(float time)//Determina el temps que falta per poder tornar a atacar. Es pot modificar amb la variable time
        {
            yield return new WaitForSeconds(time);
            canShoot = true;
        }

        IEnumerator nextHit(float time)//Determina el temps que falta per poder tornar a atacar. Es pot modificar amb la variable time
        {
            StartCoroutine(hitTime());
            
            yield return new WaitForSeconds(time);
            canHit = true;
        }

        IEnumerator hitTime()//Determina el temps que falta per poder tornar a atacar. Es pot modificar amb la variable time
        {
            for (int i = 0; i < mySpriteRender.Length; i++)
            {
                mySpriteRender[i].GetComponent<SpriteRenderer>().gameObject.SetActive(false);
            }
            yield return new WaitForSeconds(0.2f);
            for (int i = 0; i < mySpriteRender.Length; i++)
            {
                mySpriteRender[i].GetComponent<SpriteRenderer>().gameObject.SetActive(true);
            }
        }

    }

}
