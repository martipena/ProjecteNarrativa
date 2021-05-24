using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Narrativa
{
    public class SpawnRoom : MonoBehaviour
    {
        public LayerMask whatIsRoom;
        public LevelGeneration levelGen;
        Vector2 standardPos;
        public bool potPosar = true;
        public int cont = 5;
        public int[] valids = { 0, 1, 2, 3 };
        private void Start()
        {
            standardPos = gameObject.transform.position;
        }
        void Update()
        {
            Collider2D roomDetection = Physics2D.OverlapCircle(transform.position, 1, whatIsRoom);
            /*if (roomDetection == null && levelGen.stopGeneration == true)
            {
                Vector2 newPos = new Vector2(transform.position.x - levelGen.moveAmount, transform.position.y);
                transform.position = newPos;
                roomDetection = Physics2D.OverlapCircle(transform.position, 1, whatIsRoom);
                if (roomDetection.GetComponent<RoomType>().L == true)
                {
                    //Instantiate(levelGen.rooms[1], transform.position, Quaternion.identity);
                    //Destroy(gameObject);
                }
                else
                {

                }
            }

            transform.position = standardPos;*/
            /*if (potPosar)
            {
                if (roomDetection == null && levelGen.stopGeneration == true)
                {
                    potPosar = false;
                    int rand = Random.Range(0, 4);
                    Instantiate(levelGen.rooms[rand], transform.position, Quaternion.identity);
                    //Destroy(gameObject);
                    Debug.Log("Pot: " + potPosar);
                }
            }
            else
            {
                
                if (roomDetection == true && levelGen.stopGeneration == true)
                {
                    roomDetection.GetComponent<BoxCollider2D>().enabled = false;
                }
            }*/
            if (roomDetection == null && levelGen.stopGeneration == true && potPosar)
            {
                while (potPosar)
                {
                    int rand = Random.Range(valids[0], valids.Length);
                    if (rand == 0)
                    {
                        cont--;
                        Vector2 v2 = new Vector2(transform.position.x, transform.position.y - 10);
                        roomDetection = Physics2D.OverlapCircle(v2, 1, whatIsRoom);

                        if (roomDetection && roomDetection.gameObject.GetComponent<RoomType>().T)
                        {
                            Instantiate(levelGen.rooms[rand], transform.position, Quaternion.identity);
                            potPosar = false;
                        }
                        else
                        {
                            break;
                        }
                    }
                    else if (rand == 1)
                    {
                        cont--;
                        Vector2 v2 = new Vector2(transform.position.x - 10, transform.position.y);
                        roomDetection = Physics2D.OverlapCircle(v2, 1, whatIsRoom);
                        if (roomDetection && roomDetection.gameObject.GetComponent<RoomType>().R)
                        {
                            Instantiate(levelGen.rooms[rand], transform.position, Quaternion.identity);
                            potPosar = false;
                        }
                        else
                        {
                            break;
                        }
                    }
                    else if (rand == 2)
                    {
                        cont--;
                        Vector2 v2 = new Vector2(transform.position.x + 10, transform.position.y);
                        roomDetection = Physics2D.OverlapCircle(v2, 1, whatIsRoom);
                        if (roomDetection && roomDetection.gameObject.GetComponent<RoomType>().L)
                        {
                            Instantiate(levelGen.rooms[rand], transform.position, Quaternion.identity);
                            potPosar = false;
                        }
                        else
                        {
                            break;
                        }
                    }
                    else if (rand == 3)
                    {
                        cont--;
                        Vector2 v2 = new Vector2(transform.position.x, transform.position.y + 10);
                        roomDetection = Physics2D.OverlapCircle(v2, 1, whatIsRoom);
                        if (roomDetection && roomDetection.gameObject.GetComponent<RoomType>().B)
                        {
                            Instantiate(levelGen.rooms[rand], transform.position, Quaternion.identity);
                            potPosar = false;
                        }
                        else
                        {
                            break;
                        }
                    }else if (rand == 5)
                    {

                    }
                    for(int i = 0; i < valids.Length; i++)
                    {
                        if (valids[i] == rand)
                        {
                            valids[i] = 5;
                        }
                    }
                    
                }
                
            }
            else if (roomDetection == true && levelGen.stopGeneration == true)
            {

                potPosar = false;
                StartCoroutine(wait());
                
            }

            IEnumerator wait()
            {
                yield return new WaitForSeconds(1.5f);
                roomDetection.GetComponent<BoxCollider2D>().enabled = false;
                //SceneManager.LoadScene("generacioJoc");
            }
        }
    }

}
