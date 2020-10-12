using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Game
{
    public class GridBehaviour : MonoBehaviour
    {
        public GameHandler GameHandler;
        public int ColumnLength, RowLength;
        public int X_Offset, Y_Offset;
        public Cell CellPrefab;
        public Transform CellParent;
        public Cell[,] Grid;

        [Header("Enable this to see the score up cells in green color")]
        public bool IsSpecialCellVisible;

        [SerializeField]
        private List<Cell> SpecialCellList;


        private void Start()
        {
            if (CellPrefab)
            {
                GenerateGrid();
            }
            else
            {
                Debug.Log("Missing GridPrefab, please assign gridPrefab");
            }
        }

        // Place all cell on Grid Board
        public void GenerateGrid()
        {
            Grid = new Cell[RowLength, ColumnLength];

            for (int i = 0; i < GameHandler.GameConfiguration.SpecialCellCount; i++)
            {
                PlaceSpecialCellRandomely();
            }

            PlaceNormalCellOnBoard();
        }


        // Placing all special cells on the board
        private void PlaceSpecialCellRandomely()
        {
            int XIndex = Random.Range(0, RowLength);
            int YIndex = Random.Range(0, ColumnLength);

            if (Grid[XIndex, YIndex] == null)
            {
                Cell specialCell = Instantiate(CellPrefab,
                                      new Vector3(XIndex, YIndex, 0), Quaternion.identity, CellParent) as Cell;
                specialCell.ChangeCellType(CellType.Special);
                Grid[XIndex, YIndex] = specialCell;
                SpecialCellList.Add(specialCell);

                RevealSpecialCellToggle(specialCell);
            }
            else
            {
                PlaceSpecialCellRandomely();
            }
        }


        // Placing all Normal cells on the board
        private void PlaceNormalCellOnBoard()
        {
            for (int xIndex = 0; xIndex < RowLength; xIndex++)
            {
                for (int yIndex = 0; yIndex < ColumnLength; yIndex++)
                {
                    if (Grid[xIndex, yIndex] == null)
                    {
                        Cell normalCell = Instantiate(CellPrefab, new Vector3(xIndex, yIndex, 0),
                                            Quaternion.identity, CellParent);
                        Grid[xIndex, yIndex] = normalCell;
                        normalCell.ChangeCellMat(GameHandler.GameConfiguration.DefaultMaterial);
                        normalCell.ChangeCellType(CellType.Normal);
                    }

                }
            }
        }


        private void RevealSpecialCellToggle(Cell specialCell)
        {
            if (IsSpecialCellVisible)
            {
                specialCell.ChangeCellMat(GameHandler.GameConfiguration.SpecialCellMaterial);
            }
            else
            {
                specialCell.ChangeCellMat(GameHandler.GameConfiguration.DefaultMaterial);
            }
        }
    }
}

