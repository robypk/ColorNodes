using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Travancore.ROBY
{
    public class GamePlayManager : MonoBehaviour
    {
        public static GamePlayManager instance;
        [HideInInspector] public Cell currentSelectedCell;
        [HideInInspector] public bool isDrawing = false;
        [HideInInspector] public List<Cell> allCells = new List<Cell>();
        [HideInInspector] public List<Cell> occupiedCells = new List<Cell>();
        [HideInInspector] public Level SelectedLevel;

        void Start()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }

        }


        public void Result()
        {
            foreach (Cell cell in allCells)
            {
                if (!occupiedCells.Contains(cell))
                {
                    return;
                }
            }
            ResetData();
            SceneChange(0);
        }



        public void SceneChange(int sceneIndex)
        {
            SceneManager.LoadScene(sceneIndex);
        }


        public void ResetData()
        {
            currentSelectedCell = null;
            isDrawing = false;
            allCells.Clear();
            occupiedCells.Clear();
            SelectedLevel = null;
        }



    }
}
