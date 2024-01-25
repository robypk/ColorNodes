using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Travancore.ROBY
{
    public class GamePlayManager : MonoBehaviour
    {
        public static GamePlayManager instance;


        public Cell currentSelectedCell;
        public bool isDrawing = false;

        void Start()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }



    }
}
