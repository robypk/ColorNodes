using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Travancore.ROBY
{
    public class Grid : MonoBehaviour
    {
        public GameObject cellPrefab;
        public int gridSize = 5;
        public float cellSize = 1.0f;
        float cellgap = .3f;
        float border = .5f;
        Sprite gridSprite;
        Sprite cellsprite;
        int cellNum = 1;

        void Start()
        {
            gridSprite = GetComponent<SpriteRenderer>().sprite;
            cellsprite = cellPrefab.GetComponent<SpriteRenderer>().sprite;
            GenerateGrid();
        }

        void GenerateGrid()
        {
            if (gridSprite == null)
            {
                Debug.LogError("Grid sprite is not assigned!");
                return;
            }

            float spriteWidth = gridSprite.bounds.size.x;
            float spriteHeight = gridSprite.bounds.size.y;


            float cellWidth = cellsprite.bounds.size.x;
            float cellHeight = cellsprite.bounds.size.y;

            for (int x = 0; x < gridSize; x++)
            {
                for (int y = 0; y < gridSize; y++)
                {
                    float xPos = x - (spriteWidth / 2.0f) + (cellWidth/2 + border ) ;
                    float yPos = y  - (spriteHeight / 2.0f) + (cellHeight / 2 + border);

                    Vector3 cellPosition = new Vector3(xPos * (cellSize + cellgap), yPos * (cellSize + cellgap), 0);
                    GameObject cell = Instantiate(cellPrefab, cellPosition, Quaternion.identity);
                    cell.name = "Cell " + cellNum;
                    cell.transform.parent = transform; // Set the grid as the parent of each cell
                    cellNum++;
                }
            }
        }
    }
}
