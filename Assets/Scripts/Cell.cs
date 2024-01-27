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
        [HideInInspector]public Color NodeColor;
        [HideInInspector]public Cell NextSameNode = null;

        List<Cell> linePathCells = new List<Cell>();
        bool isNodeActivated = false;
        Cell CurrentHoverCell = null;
        int lineCount = 1;


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
            GetComponent<SpriteRenderer>().color = GamePlayManager.instance.currentSelectedCell.NodeColor;
            if(GamePlayManager.instance.currentSelectedCell != this) // prevent self draw
            {
                GamePlayManager.instance.currentSelectedCell.CreateLine(this);
            }
            if (NodeColor == GamePlayManager.instance.currentSelectedCell.NodeColor) // reached the Next same node
            {
                GamePlayManager.instance.isDrawing = false;
            }
        }

        private void OnMouseDown()
        {
            if (!isNodeActivated) 
            { 
                return; 
            }
            GamePlayManager.instance.currentSelectedCell = this;
            GamePlayManager.instance.isDrawing = true;
            removeLine();
            if (NextSameNode != null)
            {
                NextSameNode.removeLine();
            }
        }

        private void OnMouseUp()
        {
            if (!isNodeActivated )
            { 
                return;
            }
            GamePlayManager.instance.isDrawing = false;

            if(CurrentHoverCell != null)
            {
                if (NodeColor != CurrentHoverCell.NodeColor)
                {
                    removeLine();
                }
                else
                {
                   CurrentHoverCell.NextSameNode = this;
                   GamePlayManager.instance.occupiedCells.AddRange(linePathCells);
                }
            }
        }

        private void OnMouseExit()
        {
            GetComponent<SpriteRenderer>().color = Color.white;
        }

        public void CreateLine(Cell cell)
        {
            if (GamePlayManager.instance.currentSelectedCell == null)
            {
                return;
            }
            if (linePathCells.Count == 0 && diagonalCheck(this, cell))
            {
                drawLine(cell);
            }
            else if (!linePathCells.Contains(cell) && diagonalCheck(CurrentHoverCell, cell))
            {
                drawLine(cell);
            }
            else
            {
                GamePlayManager.instance.isDrawing = false;
            }
        }



        void drawLine(Cell cell)
        {
            if (GamePlayManager.instance.occupiedCells.Contains(cell) && GamePlayManager.instance.currentSelectedCell.NodeColor != cell.NodeColor)
            {
                GamePlayManager.instance.isDrawing = false;
                return;
            }
            linePathCells.Add(cell);
            lineCount++;
            CurrentHoverCell = linePathCells.Last<Cell>();
            lineRenderer.positionCount = lineCount;
            lineRenderer.SetPosition(lineCount - 1, CurrentHoverCell.gameObject.transform.position);
        }

        protected void removeLine()
        {
            lineCount = 1;
            foreach (Cell cell in linePathCells)
            {
                GamePlayManager.instance.occupiedCells.Remove(cell);
            }
            linePathCells.Clear();
            lineRenderer.positionCount = lineCount;
            CurrentHoverCell = null;
        }


        bool diagonalCheck (Cell PreviousCell, Cell NextCell)
        {
            if (PreviousCell == null || NextCell == null)
            {
                return false;
            }
            else if ((PreviousCell.transform.position.x == NextCell.transform.position.x)|| (PreviousCell.transform.position.y == NextCell.transform.position.y))
            {
                return true;   
            }
            else
            {
                return false;
            }
        }





    }
}
