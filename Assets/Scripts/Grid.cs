using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor.Experimental.GraphView;
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
                    GamePlayManager.instance.allCells.Add(cell.GetComponent<Cell>());
                    cell.name = "Cell " + cellNum;
                    cell.transform.parent = transform;
                    cellNum++;
                }
            }

            enableNode();


        }

        void enableNode()
        {
            string filepath = Path.Combine(Application.dataPath, "LevelData.json");
            if (File.Exists(filepath))
            {
                string json = File.ReadAllText(filepath);
                TotalLevels levels = JsonUtility.FromJson<TotalLevels>(json);
                foreach ( NodeData node in levels.Levels[1].Nodes)
                {
                    switch (node.Nodecolor)
                    {
                        case NodeColors.Orange:
                            GamePlayManager.instance.allCells[node.Node1Cell-1].ActivateNode(Color.cyan);
                            GamePlayManager.instance.allCells[node.Node2Cell-1].ActivateNode(Color.cyan);
                            break;
                        case NodeColors.Red:
                            GamePlayManager.instance.allCells[node.Node1Cell - 1].ActivateNode(Color.red);
                            GamePlayManager.instance.allCells[node.Node2Cell -1].ActivateNode(Color.red);
                            break;
                        case NodeColors.Green:
                            GamePlayManager.instance.allCells[node.Node1Cell - 1].ActivateNode(Color.green);
                            GamePlayManager.instance.allCells[node.Node2Cell - 1].ActivateNode(Color.green);
                            break;
                        case NodeColors.Blue:
                            GamePlayManager.instance.allCells[node.Node1Cell - 1].ActivateNode(Color.blue);
                            GamePlayManager.instance.allCells[node.Node2Cell - 1].ActivateNode(Color.blue);
                            break;
                        case NodeColors.Purple:
                            GamePlayManager.instance.allCells[node.Node1Cell - 1].ActivateNode(Color.yellow);
                            GamePlayManager.instance.allCells[node.Node2Cell - 1].ActivateNode(Color.yellow);
                            break;

                    }
                    GamePlayManager.instance.occupiedCells.Add(GamePlayManager.instance.allCells[node.Node1Cell-1]);
                    GamePlayManager.instance.occupiedCells.Add(GamePlayManager.instance.allCells[node.Node2Cell - 1]);

                }
            }

        }
    }
}
