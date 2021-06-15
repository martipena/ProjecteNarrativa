using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Narrativa
{
    public class LevelGeneration : MonoBehaviour
    {
        public bool nivell1;
        public bool nivell2;
        public bool nivell3;

        public Transform[] startingPositions;
        public GameObject[] rooms;
        public GameObject[] extras;//Habitacions extraen cas de fer el escenari mes gran. S'utilitza pel nivell 3
        public GameObject nextLvl;

        private int direction;
        public float moveAmount;

        private float timeBtwRoom;
        public float startTimeBtwRoom = 0.25f;

        public float minX;
        public float maxX;
        public float minY;
        public float maxY;

        public bool stopGeneration = false;
        private bool sortir = false;
        public bool obligatoriB = false;
        public bool obligatoriT = false;
        public bool obligatoriR = false;
        public bool obligatoriL = false;

        public LayerMask room;
        private int standardRooms = 14;
        private int maxDreta = 18;
        private int maxEsquerra = 22;
        private int maxDalt = 26;
        private int maxBaix = 30;
        private int maxDretaBaix = 31;
        private int maxEsquerraBaix = 32;
        private int maxDretaAlt = 33;
        private int maxEsquerraAlt = 34;
        private int posibleBaixMaxDreta = 37;
        private int posibleBaixMaxEsquerra = 40;
        private int posiblebaixPosibleDalt = 44;
        private int posiblebaixPosibleDaltMaxDreta = 46;
        private int posiblebaixPosibleDaltMaxEsquerra = 48;
        private int posibleDretaEsquerra = 51;
        private int posibleEsquerraBaix = 54;
        private int posibleEsquerraAmunt = 57;
        private int posibleDretaAvaix = 60;
        private int posibleDretaAmunt = 63;
        private int entradaDreta = 69;
        private int entradaDretaMaxAlt = 72;
        private int entradaDretaMaxBaix = 75;
        private int entradaEsquerra = 81;
        private int entradaEsquerraMaxAlt = 84;
        private int entradaesquerraMaxBaix = 87;
        private int entradaAmunt = 93;
        private int maxEsqPot = 96;
        public Vector3 posicioInici;
        public Vector3 posicioFinal;

        public bool inici = true;

        private int downCounter;
        private int contSales;
        public GameObject player;
        private void Start()
        {
            if (GameData.nivell == "Nivell1")
            {
                nivell1 = true;
                nivell2 = false;
                nivell3 = false;
            }else if (GameData.nivell == "Nivell2")
            {
                nivell1 = false;
                nivell2 = true;
                nivell3 = false;
            }
            else if (GameData.nivell == "Nivell3")
            {
                nivell1 = false;
                nivell2 = false;
                nivell3 = true;
            }
            if (nivell1 == true || nivell2 == true)
            {
                maxX = 25;
                minY = -25;
            }
            else if (nivell3 == true)
            {
                maxX = 35;
                minY = -35;
                for (int i = 0; i < extras.Length; i++)
                {
                    extras[i].SetActive(true);
                }
            }
            inici = true;
            contSales = 0;
            int randStartingPos = Random.Range(0, startingPositions.Length);
            transform.position = startingPositions[randStartingPos].position;
            if (transform.position.x == -5)//Aquests if decideixen la posicio en la que inicia i quin tipus de sala pot fer
            {
                randStartingPos = Random.Range(0, 2);
                if (randStartingPos == 1)
                {
                    randStartingPos = 2;
                }
                Instantiate(rooms[randStartingPos], transform.position, Quaternion.identity);

            }
            else if (transform.position.x == 5)
            {
                randStartingPos = Random.Range(0, 2);
                Instantiate(rooms[randStartingPos], transform.position, Quaternion.identity);
            }
            else if (transform.position.x == 15)
            {
                randStartingPos = Random.Range(0, 2);
                Instantiate(rooms[randStartingPos], transform.position, Quaternion.identity);
            }
            else if (transform.position.x == 25)
            {
                randStartingPos = Random.Range(0, 1);

                Instantiate(rooms[randStartingPos], transform.position, Quaternion.identity);
            }


            if (randStartingPos == 0)//Te sortida per sota aixi que obliga avaix amb sortida amunt
            {
                obligatoriT = true;
                direction = 7;
            }
            else if (randStartingPos == 1)//Te sortida per esquerra aixi que obliga esquerra amb sortida dreta
            {
                obligatoriR = true;
                direction = 4;
            }
            else if (randStartingPos == 2)//Te sortida per dreta aixi que obliga dreta amb sortida esquerra
            {
                obligatoriL = true;
                direction = 1;
            }
            else if (randStartingPos == 3)//Te sortida per dalt aixi que obliga avaix
            {
                obligatoriB = true;
            }
            //direction = Random.Range(1, 6);//Tria el proxim moviment que fara. 1,2 = dreta | 3,4 = esquerra | 5 = avaix
            posicioInici = transform.position;
            Debug.Log("Pos inicial: " + transform.position);
            contSales++;
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
            sortir = false;

            /*************************************************************************************************************************************************************************************/

            if (direction == 1 || direction == 2 || direction == 3)//Dreta
            {

                if (transform.position.x < maxX)
                {
                    //Debug.Log("Dreta: ");
                    Collider2D roomDetection = Physics2D.OverlapCircle(transform.position, 1, room);

                    if (roomDetection.GetComponent<RoomType>().R != true)//Detecta si la ultima habitacio creada te una sortida a la dreta o no
                    {

                        while (sortir == false)
                        {

                            if (roomDetection.GetComponent<RoomType>().L == true)
                            {
                                roomDetection.GetComponent<RoomType>().RoomDestruction();
                                int randBottomRoom = Random.Range(posiblebaixPosibleDaltMaxEsquerra + 1, posibleDretaEsquerra);
                                //Instantiate(rooms[randBottomRoom], transform.position, Quaternion.identity);

                                if (roomDetection.GetComponent<RoomType>().B == true)
                                {
                                    roomDetection.GetComponent<RoomType>().RoomDestruction();
                                    randBottomRoom = 7;
                                    Instantiate(rooms[randBottomRoom], transform.position, Quaternion.identity);
                                    roomDetection = Physics2D.OverlapCircle(transform.position, 1, room);

                                    if (roomDetection.GetComponent<RoomType>().T == true)
                                    {
                                        roomDetection.GetComponent<RoomType>().RoomDestruction();
                                        randBottomRoom = 10;
                                        Instantiate(rooms[randBottomRoom], transform.position, Quaternion.identity);
                                        sortir = true;
                                        break;
                                    }
                                }
                                else if (roomDetection.GetComponent<RoomType>().T == true)
                                {
                                    roomDetection.GetComponent<RoomType>().RoomDestruction();
                                    randBottomRoom = 14;
                                    Instantiate(rooms[randBottomRoom], transform.position, Quaternion.identity);
                                    roomDetection = Physics2D.OverlapCircle(transform.position, 1, room);

                                    if (roomDetection.GetComponent<RoomType>().B == true)
                                    {
                                        roomDetection.GetComponent<RoomType>().RoomDestruction();
                                        randBottomRoom = 10;
                                        Instantiate(rooms[randBottomRoom], transform.position, Quaternion.identity);
                                        sortir = true;
                                        break;
                                    }
                                }
                                else
                                {
                                    roomDetection.GetComponent<RoomType>().RoomDestruction();
                                    randBottomRoom = Random.Range(posiblebaixPosibleDaltMaxEsquerra + 1, posibleDretaEsquerra);
                                    Instantiate(rooms[randBottomRoom], transform.position, Quaternion.identity);
                                    sortir = true;
                                    break;
                                }
                                break;
                            }
                            else if (roomDetection.GetComponent<RoomType>().B == true)
                            {
                                roomDetection.GetComponent<RoomType>().RoomDestruction();
                                int randBottomRoom = Random.Range(posibleEsquerraAmunt + 1, posibleDretaAvaix);
                                //Instantiate(rooms[randBottomRoom], transform.position, Quaternion.identity);

                                if (roomDetection.GetComponent<RoomType>().T == true)
                                {
                                    roomDetection.GetComponent<RoomType>().RoomDestruction();
                                    randBottomRoom = 8;
                                    Instantiate(rooms[randBottomRoom], transform.position, Quaternion.identity);
                                    roomDetection = Physics2D.OverlapCircle(transform.position, 1, room);

                                    if (roomDetection.GetComponent<RoomType>().L == true)
                                    {
                                        roomDetection.GetComponent<RoomType>().RoomDestruction();
                                        randBottomRoom = 10;
                                        Instantiate(rooms[randBottomRoom], transform.position, Quaternion.identity);
                                        sortir = true;
                                        break;
                                    }
                                }
                                else if (roomDetection.GetComponent<RoomType>().L == true)
                                {
                                    roomDetection.GetComponent<RoomType>().RoomDestruction();
                                    randBottomRoom = 7;
                                    Instantiate(rooms[randBottomRoom], transform.position, Quaternion.identity);
                                    roomDetection = Physics2D.OverlapCircle(transform.position, 1, room);

                                    if (roomDetection.GetComponent<RoomType>().T == true)
                                    {
                                        roomDetection.GetComponent<RoomType>().RoomDestruction();
                                        randBottomRoom = 10;
                                        Instantiate(rooms[randBottomRoom], transform.position, Quaternion.identity);
                                        sortir = true;
                                        break;
                                    }
                                }
                                else
                                {
                                    roomDetection.GetComponent<RoomType>().RoomDestruction();
                                    randBottomRoom = Random.Range(posibleEsquerraAmunt + 1, posibleDretaAvaix);
                                    Instantiate(rooms[randBottomRoom], transform.position, Quaternion.identity);
                                    sortir = true;
                                    break;
                                }
                                break;
                            }
                            else if (roomDetection.GetComponent<RoomType>().T == true)
                            {
                                roomDetection.GetComponent<RoomType>().RoomDestruction();
                                int randBottomRoom = Random.Range(posibleDretaAvaix + 1, posibleDretaAmunt);
                                if (roomDetection.GetComponent<RoomType>().L == true)
                                {
                                    roomDetection.GetComponent<RoomType>().RoomDestruction();
                                    randBottomRoom = 14;
                                    Instantiate(rooms[randBottomRoom], transform.position, Quaternion.identity);
                                    roomDetection = Physics2D.OverlapCircle(transform.position, 1, room);

                                    if (roomDetection.GetComponent<RoomType>().B == true)
                                    {
                                        roomDetection.GetComponent<RoomType>().RoomDestruction();
                                        randBottomRoom = 10;
                                        Instantiate(rooms[randBottomRoom], transform.position, Quaternion.identity);
                                        sortir = true;
                                        break;
                                    }
                                }
                                else if (roomDetection.GetComponent<RoomType>().B == true)
                                {
                                    roomDetection.GetComponent<RoomType>().RoomDestruction();
                                    randBottomRoom = 8;
                                    Instantiate(rooms[randBottomRoom], transform.position, Quaternion.identity);
                                    roomDetection = Physics2D.OverlapCircle(transform.position, 1, room);

                                    if (roomDetection.GetComponent<RoomType>().L == true)
                                    {
                                        roomDetection.GetComponent<RoomType>().RoomDestruction();
                                        randBottomRoom = 10;
                                        Instantiate(rooms[randBottomRoom], transform.position, Quaternion.identity);
                                        sortir = true;
                                        break;
                                    }
                                }
                                else
                                {
                                    roomDetection.GetComponent<RoomType>().RoomDestruction();
                                    randBottomRoom = Random.Range(posibleDretaAvaix + 1, posibleDretaAmunt);
                                    Instantiate(rooms[randBottomRoom], transform.position, Quaternion.identity);
                                    sortir = true;
                                    break;
                                }
                                break;
                            }
                            sortir = true;
                        }
                    }
                    downCounter = 0;
                    Vector2 newPos = new Vector2(transform.position.x + moveAmount, transform.position.y);
                    transform.position = newPos;
                    if (transform.position.x == maxX && transform.position.y == minY)//Cantonada baix dreta
                    {
                        Instantiate(rooms[maxDretaBaix], transform.position, Quaternion.identity);
                        direction = 7;
                    }
                    else if (transform.position.x == maxX && transform.position.y == maxY)//Cantonada dalt dreta
                    {
                        Instantiate(rooms[maxDretaAlt], transform.position, Quaternion.identity);
                        direction = 7;
                    }
                    else if (transform.position.y == minY)//Va cap a la dreta i esta tocant el terra
                    {
                        int rand = Random.Range(entradaEsquerraMaxAlt + 1, entradaesquerraMaxBaix);
                        Instantiate(rooms[rand], transform.position, Quaternion.identity);
                        direction = 7;
                    }
                    else if (transform.position.y == maxY)//Va cap a la dreta i esta tocant el sostre
                    {
                        int rand = Random.Range(entradaEsquerra + 1, entradaEsquerraMaxAlt);
                        Instantiate(rooms[rand], transform.position, Quaternion.identity);
                        direction = 7;
                    }
                    else if (transform.position.x == maxX && roomDetection.GetComponent<RoomType>().R == true)//Va cap a la dreta toca la paret pero necesita entrada esquerra
                    {
                        int rand = Random.Range(17, 18);//Aqui posar els maxDreta
                        Instantiate(rooms[rand], transform.position, Quaternion.identity);
                        direction = 7;
                    }
                    else if (transform.position.x == maxX)//Va cap a la dreta pero toca la paret
                    {
                        int rand = Random.Range(17, 18);//Aqui posar els maxDreta
                        Instantiate(rooms[rand], transform.position, Quaternion.identity);
                        direction = 7;
                    }
                    else//Pot estar en qualsevol lloc que no sigui una paret posar entrades esquerra nomes
                    {
                        int rand = Random.Range(entradaDretaMaxBaix + 1, entradaEsquerra);
                        Instantiate(rooms[rand], transform.position, Quaternion.identity);
                    }

                    contSales++;
                    direction = Random.Range(1, 8);
                    if (direction == 4 || direction == 5)//Per evitar que torni endarrera
                    {
                        direction = 2;
                    }
                    else if (direction == 6)
                    {
                        direction = 7;
                    }
                    obligatoriL = true;
                    obligatoriR = false;
                    obligatoriB = false;
                    obligatoriT = false;
                }
                else
                {
                    direction = 7;
                }
                //inici = false;

            }

            /*************************************************************************************************************************************************************************************/

            else if (direction == 4 || direction == 5 || direction == 6)//Esquerra
            {

                if (transform.position.x > minX)
                {
                    //Debug.Log("Esq: ");
                    Collider2D roomDetection = Physics2D.OverlapCircle(transform.position, 1, room);

                    if (roomDetection.GetComponent<RoomType>().L != true)//Detecta si la ultima habitacio creada te una sortida a la esquerra o no
                    {

                        while (sortir == false)
                        {
                            if (roomDetection.GetComponent<RoomType>().R == true)
                            {
                                roomDetection.GetComponent<RoomType>().RoomDestruction();
                                int randBottomRoom = Random.Range(posiblebaixPosibleDaltMaxEsquerra + 1, posibleDretaEsquerra);
                                //Instantiate(rooms[randBottomRoom], transform.position, Quaternion.identity);

                                if (roomDetection.GetComponent<RoomType>().B == true)
                                {
                                    roomDetection.GetComponent<RoomType>().RoomDestruction();
                                    randBottomRoom = 49;
                                    Instantiate(rooms[randBottomRoom], transform.position, Quaternion.identity);
                                    roomDetection = Physics2D.OverlapCircle(transform.position, 1, room);

                                    if (roomDetection.GetComponent<RoomType>().T == true)
                                    {
                                        roomDetection.GetComponent<RoomType>().RoomDestruction();
                                        randBottomRoom = 10;
                                        Instantiate(rooms[randBottomRoom], transform.position, Quaternion.identity);
                                        sortir = true;
                                        break;
                                    }
                                }
                                else if (roomDetection.GetComponent<RoomType>().T == true)
                                {
                                    roomDetection.GetComponent<RoomType>().RoomDestruction();
                                    randBottomRoom = 14;
                                    Instantiate(rooms[randBottomRoom], transform.position, Quaternion.identity);
                                    roomDetection = Physics2D.OverlapCircle(transform.position, 1, room);

                                    if (roomDetection.GetComponent<RoomType>().B == true)
                                    {
                                        roomDetection.GetComponent<RoomType>().RoomDestruction();
                                        randBottomRoom = 10;
                                        Instantiate(rooms[randBottomRoom], transform.position, Quaternion.identity);
                                        sortir = true;
                                        break;
                                    }
                                }
                                else
                                {
                                    roomDetection.GetComponent<RoomType>().RoomDestruction();
                                    randBottomRoom = Random.Range(posiblebaixPosibleDaltMaxEsquerra + 1, posibleDretaEsquerra);
                                    Instantiate(rooms[randBottomRoom], transform.position, Quaternion.identity);
                                    sortir = true;
                                }
                                break;
                            }
                            else if (roomDetection.GetComponent<RoomType>().B == true)
                            {
                                roomDetection.GetComponent<RoomType>().RoomDestruction();
                                int randBottomRoom = Random.Range(posibleDretaEsquerra + 1, posibleEsquerraBaix);
                                //Instantiate(rooms[randBottomRoom], transform.position, Quaternion.identity);

                                if (roomDetection.GetComponent<RoomType>().T == true)
                                {
                                    roomDetection.GetComponent<RoomType>().RoomDestruction();
                                    randBottomRoom = 9;
                                    Instantiate(rooms[randBottomRoom], transform.position, Quaternion.identity);
                                    roomDetection = Physics2D.OverlapCircle(transform.position, 1, room);

                                    if (roomDetection.GetComponent<RoomType>().R == true)
                                    {
                                        roomDetection.GetComponent<RoomType>().RoomDestruction();
                                        randBottomRoom = 10;
                                        Instantiate(rooms[randBottomRoom], transform.position, Quaternion.identity);
                                        sortir = true;
                                        break;
                                    }
                                }
                                else if (roomDetection.GetComponent<RoomType>().R == true)
                                {
                                    roomDetection.GetComponent<RoomType>().RoomDestruction();
                                    randBottomRoom = 7;
                                    Instantiate(rooms[randBottomRoom], transform.position, Quaternion.identity);
                                    roomDetection = Physics2D.OverlapCircle(transform.position, 1, room);

                                    if (roomDetection.GetComponent<RoomType>().T == true)
                                    {
                                        roomDetection.GetComponent<RoomType>().RoomDestruction();
                                        randBottomRoom = 10;
                                        Instantiate(rooms[randBottomRoom], transform.position, Quaternion.identity);
                                        sortir = true;
                                        break;
                                    }
                                }
                                else
                                {
                                    roomDetection.GetComponent<RoomType>().RoomDestruction();
                                    randBottomRoom = Random.Range(posibleDretaEsquerra + 1, posibleEsquerraBaix);
                                    Instantiate(rooms[randBottomRoom], transform.position, Quaternion.identity);
                                    sortir = true;
                                }
                                break;
                            }
                            else if (roomDetection.GetComponent<RoomType>().T == true)
                            {
                                roomDetection.GetComponent<RoomType>().RoomDestruction();
                                int randBottomRoom = Random.Range(posibleEsquerraBaix + 1, posibleEsquerraAmunt);
                                //Instantiate(rooms[randBottomRoom], transform.position, Quaternion.identity);

                                if (roomDetection.GetComponent<RoomType>().R == true)
                                {
                                    roomDetection.GetComponent<RoomType>().RoomDestruction();
                                    randBottomRoom = 14;
                                    Instantiate(rooms[randBottomRoom], transform.position, Quaternion.identity);
                                    roomDetection = Physics2D.OverlapCircle(transform.position, 1, room);

                                    if (roomDetection.GetComponent<RoomType>().B == true)
                                    {
                                        roomDetection.GetComponent<RoomType>().RoomDestruction();
                                        randBottomRoom = 10;
                                        Instantiate(rooms[randBottomRoom], transform.position, Quaternion.identity);
                                        sortir = true;
                                        break;
                                    }
                                }
                                else if (roomDetection.GetComponent<RoomType>().B == true)
                                {
                                    roomDetection.GetComponent<RoomType>().RoomDestruction();
                                    randBottomRoom = 9;
                                    Instantiate(rooms[randBottomRoom], transform.position, Quaternion.identity);
                                    roomDetection = Physics2D.OverlapCircle(transform.position, 1, room);

                                    if (roomDetection.GetComponent<RoomType>().R == true)
                                    {
                                        roomDetection.GetComponent<RoomType>().RoomDestruction();
                                        randBottomRoom = 10;
                                        Instantiate(rooms[randBottomRoom], transform.position, Quaternion.identity);
                                        sortir = true;
                                        break;
                                    }
                                }
                                else
                                {
                                    roomDetection.GetComponent<RoomType>().RoomDestruction();
                                    randBottomRoom = Random.Range(posibleEsquerraBaix + 1, posibleEsquerraAmunt);
                                    Instantiate(rooms[randBottomRoom], transform.position, Quaternion.identity);
                                    sortir = true;
                                }
                                break;
                            }

                            sortir = true;
                        }
                    }
                    downCounter = 0;

                    Vector2 newPos = new Vector2(transform.position.x - moveAmount, transform.position.y);
                    transform.position = newPos;
                    if (transform.position.x == minX && transform.position.y == minY)//Cantonada baix esquerra
                    {
                        Instantiate(rooms[maxEsquerraBaix], transform.position, Quaternion.identity);
                        direction = 7;
                    }
                    else if (transform.position.x == minX && transform.position.y == maxY)//Cantonada dalt esquerra
                    {
                        Instantiate(rooms[maxEsquerraAlt], transform.position, Quaternion.identity);
                        direction = 7;
                    }
                    else if (transform.position.y == minY)//Va cap a la esquerra i esta tocant el terra
                    {
                        int rand = Random.Range(entradaDretaMaxAlt + 1, entradaDretaMaxBaix);
                        Instantiate(rooms[rand], transform.position, Quaternion.identity);
                        direction = 7;
                    }
                    else if (transform.position.y == maxY)//Va cap a la esquerra i esta tocant el sostre
                    {
                        int rand = Random.Range(entradaDreta + 1, entradaDretaMaxAlt);
                        Instantiate(rooms[rand], transform.position, Quaternion.identity);
                        direction = 7;
                    }
                    else if (transform.position.x == minX)
                    {
                        int rand = Random.Range(entradaAmunt + 1, maxEsqPot);
                        Instantiate(rooms[rand], transform.position, Quaternion.identity);
                        direction = 7;
                    }
                    else
                    {
                        int rand = Random.Range(posibleDretaAmunt + 1, entradaDreta);
                        Instantiate(rooms[rand], transform.position, Quaternion.identity);
                    }
                    contSales++;
                    direction = Random.Range(4, 8);
                    obligatoriL = false;
                    obligatoriR = true;
                    obligatoriB = false;
                    obligatoriT = false;
                }
                else
                {
                    direction = 7;
                }
                //inici = false;

            }

            /*************************************************************************************************************************************************************************************/

            else if (direction == 7)//Avaix
            {

                downCounter++;

                if (transform.position.y > minY)
                {
                    //Debug.Log("Baix: ");
                    Collider2D roomDetection = Physics2D.OverlapCircle(transform.position, 1, room);

                    if (roomDetection.GetComponent<RoomType>().B != true)//Detecta si la ultima habitacio creada te una sortida avaix o no
                    {

                        while (sortir == false)
                        {
                            if (roomDetection.GetComponent<RoomType>().L == true)
                            {
                                roomDetection.GetComponent<RoomType>().RoomDestruction();
                                int randBottomRoom = Random.Range(posibleBaixMaxEsquerra + 1, posiblebaixPosibleDalt - 1);
                                //Instantiate(rooms[randBottomRoom], transform.position, Quaternion.identity);

                                if (roomDetection.GetComponent<RoomType>().R == true)
                                {
                                    roomDetection.GetComponent<RoomType>().RoomDestruction();
                                    randBottomRoom = 7;
                                    Instantiate(rooms[randBottomRoom], transform.position, Quaternion.identity);
                                    roomDetection = Physics2D.OverlapCircle(transform.position, 1, room);

                                    if (roomDetection.GetComponent<RoomType>().T == true)
                                    {
                                        roomDetection.GetComponent<RoomType>().RoomDestruction();
                                        randBottomRoom = 10;
                                        Instantiate(rooms[randBottomRoom], transform.position, Quaternion.identity);
                                        sortir = true;
                                    }
                                }
                                else if (roomDetection.GetComponent<RoomType>().T == true)
                                {
                                    roomDetection.GetComponent<RoomType>().RoomDestruction();
                                    randBottomRoom = 9;
                                    Instantiate(rooms[randBottomRoom], transform.position, Quaternion.identity);
                                    roomDetection = Physics2D.OverlapCircle(transform.position, 1, room);

                                    if (roomDetection.GetComponent<RoomType>().R == true)
                                    {
                                        roomDetection.GetComponent<RoomType>().RoomDestruction();
                                        randBottomRoom = 10;
                                        Instantiate(rooms[randBottomRoom], transform.position, Quaternion.identity);
                                        sortir = true;
                                    }
                                }
                                break;
                            }
                            else if (roomDetection.GetComponent<RoomType>().T == true)
                            {
                                roomDetection.GetComponent<RoomType>().RoomDestruction();
                                int randBottomRoom = Random.Range(posibleBaixMaxEsquerra + 1, posiblebaixPosibleDalt - 1);
                                //Instantiate(rooms[randBottomRoom], transform.position, Quaternion.identity);

                                if (roomDetection.GetComponent<RoomType>().L == true)
                                {
                                    roomDetection.GetComponent<RoomType>().RoomDestruction();
                                    randBottomRoom = 9;
                                    Instantiate(rooms[randBottomRoom], transform.position, Quaternion.identity);
                                    roomDetection = Physics2D.OverlapCircle(transform.position, 1, room);

                                    if (roomDetection.GetComponent<RoomType>().R == true)
                                    {
                                        roomDetection.GetComponent<RoomType>().RoomDestruction();
                                        randBottomRoom = 10;
                                        Instantiate(rooms[randBottomRoom], transform.position, Quaternion.identity);
                                        sortir = true;
                                    }
                                }
                                else if (roomDetection.GetComponent<RoomType>().R == true)
                                {
                                    roomDetection.GetComponent<RoomType>().RoomDestruction();
                                    randBottomRoom = 8;
                                    Instantiate(rooms[randBottomRoom], transform.position, Quaternion.identity);
                                    roomDetection = Physics2D.OverlapCircle(transform.position, 1, room);

                                    if (roomDetection.GetComponent<RoomType>().L == true)
                                    {
                                        roomDetection.GetComponent<RoomType>().RoomDestruction();
                                        randBottomRoom = 10;
                                        Instantiate(rooms[randBottomRoom], transform.position, Quaternion.identity);
                                        sortir = true;
                                    }
                                }
                                break;
                            }
                            else
                            {
                                roomDetection.GetComponent<RoomType>().RoomDestruction();
                                int randBottomRoom = Random.Range(posibleBaixMaxEsquerra + 1, posiblebaixPosibleDalt - 1);
                                Instantiate(rooms[randBottomRoom], transform.position, Quaternion.identity);
                                sortir = true;
                            }
                            sortir = true;
                        }
                    }
                    Vector2 newPos = new Vector2(transform.position.x, transform.position.y - moveAmount);
                    transform.position = newPos;
                    if (transform.position.x == maxX && transform.position.y > minY)//Pot baixar pero no anar cap a la dreta
                    {
                        int rand = Random.Range(posiblebaixPosibleDalt + 1, posiblebaixPosibleDaltMaxDreta);
                        Instantiate(rooms[rand], transform.position, Quaternion.identity);
                    }
                    else if (transform.position.x == minX && transform.position.y > minY)//Pot baixar pero no anar cap a la esquerra
                    {
                        int rand = Random.Range(posiblebaixPosibleDaltMaxDreta + 1, posiblebaixPosibleDaltMaxEsquerra);
                        Instantiate(rooms[rand], transform.position, Quaternion.identity);
                    }
                    else if (transform.position.x > minX && transform.position.y > minY)//Esta en algun punt del centre
                    {
                        int rand = Random.Range(maxDalt + 1, maxBaix);
                        Instantiate(rooms[10], transform.position, Quaternion.identity);
                    }
                    else if (transform.position.x == maxX && transform.position.y == minY)//Esta a la cantonada dreta
                    {
                        Instantiate(rooms[maxDretaBaix], transform.position, Quaternion.identity);
                    }
                    else if (transform.position.x == minX && transform.position.y == minY)//Esta a la cantonada esquerra
                    {
                        Instantiate(rooms[maxEsquerraBaix], transform.position, Quaternion.identity);
                    }
                    else if (transform.position.y == minY)//Esta a baix pero no toca les parets
                    {
                        int rand = Random.Range(maxDalt + 1, maxBaix);
                        Instantiate(rooms[14], transform.position, Quaternion.identity);
                    }
                    else
                    {
                        int rand = Random.Range(posibleBaixMaxEsquerra + 1, posiblebaixPosibleDalt);
                        Instantiate(rooms[rand], transform.position, Quaternion.identity);
                    }
                    contSales++;
                    obligatoriL = false;
                    obligatoriR = false;
                    obligatoriB = false;
                    obligatoriT = true;


                    direction = Random.Range(1, 8);
                }
                else
                {
                    //Debug.Log("ACABA: "+contSales);
                    if (nivell1 == true && contSales < 6 || nivell1 == true && contSales > 7)
                    {
                        SceneManager.LoadScene("generacioJoc");
                    }
                    else if (nivell2 == true && contSales < 8 || nivell2 == true && contSales > 11)
                    {
                        SceneManager.LoadScene("generacioJoc");
                    }
                    else if (nivell3 == true && contSales <= 12 || nivell3 == true && contSales > 15)
                    {
                        SceneManager.LoadScene("generacioJoc");
                    }
                    else
                    {
                        Collider2D roomDetection = Physics2D.OverlapCircle(transform.position, 1, room);
                        roomDetection.GetComponent<RoomType>().RoomDestruction();
                        if (obligatoriL == true)
                        {
                            Instantiate(rooms[1], transform.position, Quaternion.identity);
                        }
                        else if (obligatoriR == true)
                        {
                            Instantiate(rooms[2], transform.position, Quaternion.identity);
                        }
                        else
                        {
                            Instantiate(rooms[3], transform.position, Quaternion.identity);
                        }
                        roomDetection.GetComponent<RoomType>().RoomDestruction();
                        posicioFinal = transform.position;
                        Instantiate(nextLvl, transform.position, Quaternion.identity);
                        stopGeneration = true;
                        Debug.Log("Posicio final: " + posicioFinal);
                        player.transform.position = posicioInici;
                    }

                }
                //inici = false;
            }
        }


    }
}

