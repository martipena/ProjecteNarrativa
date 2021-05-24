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
        public bool potIniciar=false;

        // Update is called once per frame
        void Update()
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

        IEnumerator inicia()
        {
            yield return new WaitForSeconds(1.5f);
            potIniciar = true;
        }
    }

}
