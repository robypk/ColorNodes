using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Travancore.ROBY
{
    public class GamePlayManager : MonoBehaviour
    {
        public static GamePlayManager instance;
        [HideInInspector] public Cell currentSelectedCell;
        [HideInInspector] public bool isDrawing = false;
        [HideInInspector] public List<Cell> allCells = new List<Cell>();
        [HideInInspector] public List<Cell> occupiedCells = new List<Cell>();

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
