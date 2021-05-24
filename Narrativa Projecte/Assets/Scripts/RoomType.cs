using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Narrativa
{
    public class RoomType : MonoBehaviour
    {
        public int type;
        public bool T;//tipus de sortida disponible
        public bool B;
        public bool R;
        public bool L;
        public void RoomDestruction()
        {
            Destroy(gameObject);
        }

    }

}
