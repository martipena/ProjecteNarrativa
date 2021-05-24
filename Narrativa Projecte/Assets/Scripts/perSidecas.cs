using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Narrativa
{
    public class perSidecas : MonoBehaviour
    {

        public Transform[] startingPositions;
        public GameObject[] rooms;
        /*
         - Estandards
         0 = LR
         1 = BRL
         2 = LRT
         3 = BRLT
         - 1 entrada
         4 = B
         5 = L
         6 = R
         7 = T
         - Paret dreta
         8 = LB
         9 = LT
         10 = LTB
         */
        private int direction;
        public float moveAmount;

        private float timeBtwRoom;
        public float startTimeBtwRoom = 0.25f;

        public float minX;
        public float maxX;
        public float minY;
        public bool stopGeneration = false;

        public LayerMask room;
        private int availableRooms = 4;

        private int downCounter;
        private void Start()
        {
            int randStartingPos = Random.Range(0, startingPositions.Length);
            transform.position = startingPositions[randStartingPos].position;
            if (transform.position.x == -5)//Aquests if decideixen la posicio en la que inicia i quin tipus de sala pot fer
            {
                randStartingPos = Random.Range(4, 6);
                if (randStartingPos == 5)
                {
                    randStartingPos = 6;
                }
                Instantiate(rooms[randStartingPos], transform.position, Quaternion.identity);

            }
            else if (transform.position.x == 5)
            {
                randStartingPos = Random.Range(4, 6);
                Instantiate(rooms[randStartingPos], transform.position, Quaternion.identity);
            }
            else if (transform.position.x == 15)
            {
                randStartingPos = Random.Range(4, 6);
                Instantiate(rooms[randStartingPos], transform.position, Quaternion.identity);
            }
            else if (transform.position.x == 25)
            {
                randStartingPos = Random.Range(4, 5);

                Instantiate(rooms[randStartingPos], transform.position, Quaternion.identity);
            }

            direction = Random.Range(1, 6);
        }

        private void Update()
        {
            if (timeBtwRoom <= 0 && stopGeneration == false)
            {
                Move();
                timeBtwRoom = startTimeBtwRoom;
            }
            else
            {
                timeBtwRoom -= Time.deltaTime;
            }
        }

        private void Move()
        {
            if (direction == 1 || direction == 2)//Dreta
            {
                if (transform.position.x < maxX)
                {
                    downCounter = 0;
                    Vector2 newPos = new Vector2(transform.position.x + moveAmount, transform.position.y);
                    transform.position = newPos;
                    if (transform.position.x == maxX)
                    {
                        int rand = Random.Range(8, 10);
                        Instantiate(rooms[rand], transform.position, Quaternion.identity);
                    }
                    else
                    {
                        int rand = Random.Range(0, availableRooms);
                        Instantiate(rooms[rand], transform.position, Quaternion.identity);
                    }
                    direction = Random.Range(1, 6);
                    if (direction == 3)//Per evitar que torni endarrera
                    {
                        direction = 2;
                    }
                    else if (direction == 4)
                    {
                        direction = 5;
                    }
                }
                else
                {
                    direction = 5;
                }

            }
            else if (direction == 3 || direction == 4)//Esquerra
            {
                if (transform.position.x > minX)
                {
                    downCounter = 0;
                    Vector2 newPos = new Vector2(transform.position.x - moveAmount, transform.position.y);
                    transform.position = newPos;

                    int rand = Random.Range(0, availableRooms);
                    Instantiate(rooms[rand], transform.position, Quaternion.identity);

                    direction = Random.Range(3, 6);
                }
                else
                {
                    direction = 5;
                }

            }
            else if (direction == 5)//Avaix
            {
                downCounter++;

                if (transform.position.y > minY)
                {
                    Collider2D roomDetection = Physics2D.OverlapCircle(transform.position, 1, room);

                    if (roomDetection.GetComponent<RoomType>().type != 1 && roomDetection.GetComponent<RoomType>().type != 3 && roomDetection.tag != "iniciFi")
                    {
                        if (downCounter >= 2)
                        {
                            roomDetection.GetComponent<RoomType>().RoomDestruction();
                            Instantiate(rooms[3], transform.position, Quaternion.identity);
                        }
                        else
                        {
                            roomDetection.GetComponent<RoomType>().RoomDestruction();
                            int randBottomRoom = Random.Range(1, 4);

                            if (randBottomRoom == 2)
                            {
                                randBottomRoom = 1;
                            }

                            Instantiate(rooms[randBottomRoom], transform.position, Quaternion.identity);
                        }


                    }

                    Vector2 newPos = new Vector2(transform.position.x, transform.position.y - moveAmount);
                    transform.position = newPos;

                    int rand = Random.Range(2, 4);
                    Instantiate(rooms[rand], transform.position, Quaternion.identity);

                    direction = Random.Range(1, 6);
                }
                else
                {
                    stopGeneration = true;
                }

            }
            //Instantiate(rooms[0], transform.position, Quaternion.identity);
        }


    }

}
