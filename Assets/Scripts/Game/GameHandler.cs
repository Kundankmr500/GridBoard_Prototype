using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using ScriptableObj;
using Player;

namespace Game
{
    public class GameHandler : MonoBehaviour
    {
        public GridBehaviour GridInstance;
        public GameConfiguration GameConfiguration; // Game Configuration file

        [Header("Veriable must check before start of the game")]
        public PlayerMovement playerMovement;
        public GameObject RestartGameButton;
        public TextMeshProUGUI ScoreText;
        public TextMeshProUGUI RemainingChanceText;
        public TextMeshProUGUI GameOverText;
        public TextMeshProUGUI PlayerWinText;

        private int _collectedCells;
        private int _scoreValue;
        private int _playerRemainingChance;


        private void Start()
        {
            InitializeGameSettings();
        }


        // Initailizing game start value
        public void InitializeGameSettings()
        {
            _playerRemainingChance = GameConfiguration.MaxPlayerChance;
            RemainingChanceText.text = Constant.RemainingChancePrefix + _playerRemainingChance;
            ScoreText.text = Constant.ScorePrefix + _scoreValue;
        }


        // Calculating score value
        public void CalculateScore()
        {
            _scoreValue += GameConfiguration.ScoreValuePerCell;
            ScoreText.text = Constant.ScorePrefix + _scoreValue;
            CheckForCollectedCells();
        }


        // Checking if the cell index in the range of board array
        public bool isInvalid(int row, int column)
        {
            return (row < 0 || row > GridInstance.RowLength - 1 || column < 0 || column > GridInstance.ColumnLength - 1);
        }


        // Player input cell selection process
        public void PlayerInputSelectionProcess(int xPos, int yPos)
        {
            if (!isInvalid(xPos, yPos)) //condition for clicking only in game board area 
            {
                Cell cell = GridInstance.Grid[xPos, yPos];
                CellSelectionRules(cell, xPos, yPos);

                Debug.Log("celltype - " + cell.CellType);
            }
        }


        // Actual rules for cell slection color
        private void CellSelectionRules(Cell cell, int xPos, int yPos)
        {
            if (cell.CellType == CellType.Special)
            {
                cell.ChangeCellMat(GameConfiguration.RedCellMaterial);
                CalculateScore();
            }
            else if (IsThereSpecialCellInRange(xPos, yPos))
            {
                cell.ChangeCellMat(GameConfiguration.YellowCellMaterial);
            }
            else
            {
                cell.ChangeCellMat(GameConfiguration.GreenCellMaterial);
            }

            CheckForPlayerChance();
        }


        // checking if we have found any special Cell in the range of 2 of selected Cell
        private bool IsThereSpecialCellInRange(int xIndex, int yIndex)
        {
            bool isSpecialCell = false;

            for (int i = xIndex - GameConfiguration.RangeForChekingCellWithinGrid;
                     i <= xIndex + GameConfiguration.RangeForChekingCellWithinGrid; i++)
            {
                for (int j = yIndex - GameConfiguration.RangeForChekingCellWithinGrid;
                         j <= yIndex + GameConfiguration.RangeForChekingCellWithinGrid; j++)
                {
                    if (!isInvalid(i, j) && GridInstance.Grid[i, j].CellType == CellType.Special)
                    {
                        isSpecialCell = true;
                    }
                }
            }
            return isSpecialCell;
        }


        // Counting for all special Cells
        public void CheckForCollectedCells()
        {
            _collectedCells++;
            if (_collectedCells == GameConfiguration.SpecialCellCount)
            {
                ShowPlayerWinScreen();
            }
        }


        // Counting for total player chances for finding the special cells
        public void CheckForPlayerChance()
        {
            _playerRemainingChance--;
            RemainingChanceText.text = Constant.RemainingChancePrefix + _playerRemainingChance;
            if (_playerRemainingChance == 0)
            {
                GameOverScreen();
            }
        }


        // showing gameover screen on the screen
        public void GameOverScreen()
        {
            GameOverText.gameObject.SetActive(true);
            StartCoroutine(ResatGame());
        }


        // showing game winiing screen on the screen
        public void ShowPlayerWinScreen()
        {
            PlayerWinText.gameObject.SetActive(true);
            StartCoroutine(ResatGame());
        }


        // resetting game for the next game
        public IEnumerator ResatGame()
        {
            playerMovement.enabled = false;
            yield return new WaitForSeconds(5f);
            RestartGameButton.SetActive(true);
        }


        //  restarting the game
        public void ResatartGame()
        {
            SceneManager.LoadScene(0);
        }
    }
}
