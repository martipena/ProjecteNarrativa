using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Narrativa
{
    
    public class AttackArea : MonoBehaviour
    {
        public bool canDamage = false;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Player")
            {
                
                if (canDamage)
                {
                    canDamage = false;
                    collision.gameObject.GetComponent<PlayerController>().Hit(1);
                }
                else
                {
                    StartCoroutine(attackStart());
                }
                
            }
        }
        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Player")
            {
                Debug.Log("surt");
                StopCoroutine(attackStart());
                canDamage = false;
            }
        }
        IEnumerator attackStart()
        {
            yield return new WaitForSeconds(0.2f);
            canDamage = true;
            StopCoroutine(attackStart());
        }
    }

}
