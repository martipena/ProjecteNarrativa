﻿using System.Collections;
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
        // Update is called once per frame
        void Update()
        {
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
                }
                if (potIniciar)
                {
                    loadImage.enabled = false;
                    ProcessInputs();
                }

            }
                if (pos.x == 1)
                {
                    right = true;
                }
                else if (pos.x == -1)
                {
                    left = true;
                }
                else if (pos.y == 1)
                {
                    top = true;
                }
                else if (pos.y == -1)
                {
                    down = true;
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

        IEnumerator inicia()
        {
            yield return new WaitForSeconds(1.5f);
            potIniciar = true;
        }

        IEnumerator nextShot(float time)//Determina el temps que falta per poder tornar a atacar. Es pot modificar amb la variable time
        {
            yield return new WaitForSeconds(time);
            canShoot = true;
        }

        IEnumerator nextHit(float time)//Determina el temps que falta per poder tornar a atacar. Es pot modificar amb la variable time
        {
            yield return new WaitForSeconds(time);
            canHit = true;
        }

    }

}
