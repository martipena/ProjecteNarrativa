using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInRoom : MonoBehaviour
{
    public GameObject[] roomElements = new GameObject[0];
    public GameObject sueloMapa;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            for(int i = 0; i < roomElements.Length; i++)
            {
                roomElements[i].SetActive(true);
            }
            sueloMapa.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            for (int i = 0; i < roomElements.Length; i++)
            {
                roomElements[i].SetActive(false);
            }
        }
    }
}
