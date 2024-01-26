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

        List<Cell> linePathCells = new List<Cell>();
        bool isNodeActivated = false;
        // Start is called before the first frame update
        void Start()
        {
            lineRenderer.positionCount = lineCount;
            lineRenderer.SetPosition(lineCount-1, transform.position);
        
        }

        public void ActivateNode(Color nodeColor)
        {
            node.enabled = true;
            node.color = nodeColor;
            NodeColor = nodeColor;
            lineRenderer.material.color = nodeColor;
            isNodeActivated = true;

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
            if (!isNodeActivated) { return; }
            GamePlayManager.instance.currentSelectedCell = this;
            GamePlayManager.instance.isDrawing = true;
        }
        private void OnMouseUp()
        {
            if (!isNodeActivated) { return; }
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
