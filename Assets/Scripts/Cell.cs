using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Travancore.ROBY
{
    public class Cell : MonoBehaviour
    {

        [SerializeField] LineRenderer lineRenderer;
        [SerializeField] SpriteRenderer node;
        public Color NodeColor;
        Cell CurrentHoverCell = null;
        int lineCount = 1;
        private Color[] colorOptions = { Color.red, Color.green, Color.blue, new Color(1.0f, 0.5f, 0.0f), Color.magenta };
        List<Cell> linePathCells = new List<Cell>();
        // Start is called before the first frame update
        void Start()
        {
            Color randomColor = colorOptions[Random.Range(0, colorOptions.Length)]; //Color.blue; //new Color(Random.value, Random.value, Random.value);
            node.color = randomColor;
            NodeColor = randomColor;
            lineRenderer.material.color = randomColor;
            lineRenderer.positionCount = lineCount;
            lineRenderer.SetPosition(lineCount-1, transform.position);
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }


        private void OnMouseEnter()
        {
            if (!GamePlayManager.instance.isDrawing)
            {
                return;
            }
            GetComponent<SpriteRenderer>().color = Color.green;
            GamePlayManager.instance.currentSelectedCell.CreateLine(this);
        }

        private void OnMouseExit()
        {
            GetComponent<SpriteRenderer>().color = Color.white;
        }

        private void OnMouseDown()
        {
            GamePlayManager.instance.currentSelectedCell = this;
            GamePlayManager.instance.isDrawing = true;
        }
        private void OnMouseUp()
        {
            GamePlayManager.instance.isDrawing = false;
            if (NodeColor != CurrentHoverCell.NodeColor)
            {
                lineCount = 1;  
                linePathCells.Clear();
                lineRenderer.positionCount = lineCount;
            }
        }



        public void CreateLine(Cell cell)
        {
            if(GamePlayManager.instance.currentSelectedCell == null)
            {
                return;
            }


            if (linePathCells.Count == 0 && (this.transform.position.x == cell.transform.position.x || this.transform.position.y == cell.transform.position.y))
            {
                linePathCells.Add(cell);

                lineCount++;
            }
            else if (!linePathCells.Contains(cell) && (linePathCells.Last<Cell>().transform.position.x == cell.transform.position.x || linePathCells.Last<Cell>().transform.position.y == cell.transform.position.y))
            {
                linePathCells.Add(cell);
                lineCount++;
            }

            lineRenderer.positionCount = lineCount;
            lineRenderer.SetPosition(lineCount-1, linePathCells.Last<Cell>().gameObject.transform.position);
            CurrentHoverCell = linePathCells.Last<Cell>();

        }





    }
}
