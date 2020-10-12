using DG.Tweening;
using UnityEngine;
using ScriptableObj;
using Game;

namespace Player
{
    public class PlayerMovement : MonoBehaviour
    {
        public GridBehaviour GridInstance;
        public GameHandler GameHandler;
        public PlayerConfiguration PlayerConfiguration;

        private bool _canMovePlayer;


        private void Start()
        {
            SetPlayerStartPosition();
        }


        private void Update()
        {
            PlayerSelectionKeyInput();

            if (_canMovePlayer)
                CheckPlayerKeyInputMovement();
        }


        // checking for User Input for slection
        private void PlayerSelectionKeyInput()
        {
            // Mouse left click check
            if (Input.GetMouseButtonDown(0))
            {
                Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                int xPos = Mathf.RoundToInt(mousePos.x);
                int yPos = Mathf.RoundToInt(mousePos.y);

                GameHandler.PlayerInputSelectionProcess(xPos, yPos);
            }
            else if (Input.GetKeyDown(PlayerConfiguration.PlayerCellSelection))
            {
                int xPos = Mathf.RoundToInt(transform.position.x);
                int yPos = Mathf.RoundToInt(transform.position.y);

                GameHandler.PlayerInputSelectionProcess(xPos, yPos);
            }
        }

        // Checking player keyboard input for player move on board
        public void CheckPlayerKeyInputMovement()
        {
            int xPos = Mathf.RoundToInt(transform.position.x);
            int yPos = Mathf.RoundToInt(transform.position.y);

            if (Input.GetKeyDown(PlayerConfiguration.PlayerInput_Down))
            {
                if (!GameHandler.isInvalid(xPos, yPos - GridInstance.Y_Offset))
                {
                    MovePlayer(transform.position - new Vector3(0, GridInstance.Y_Offset, 0));
                }
            }

            else if (Input.GetKeyDown(PlayerConfiguration.PlayerInput_Up))
            {
                if (!GameHandler.isInvalid(xPos, yPos + GridInstance.Y_Offset))
                {
                    MovePlayer(transform.position + new Vector3(0, GridInstance.Y_Offset, 0));
                }
            }

            if (Input.GetKeyDown(PlayerConfiguration.PlayerInput_Left))
            {
                if (!GameHandler.isInvalid(xPos - GridInstance.X_Offset, yPos))
                {
                    MovePlayer(transform.position - new Vector3(GridInstance.X_Offset, 0, 0));
                }
            }

            else if (Input.GetKeyDown(PlayerConfiguration.PlayerInput_Right))
            {
                if (!GameHandler.isInvalid(xPos + GridInstance.X_Offset, yPos))
                {
                    MovePlayer(transform.position + new Vector3(GridInstance.X_Offset, 0, 0));
                }
            }
        }

        // player movement
        void MovePlayer(Vector3 position)
        {
            _canMovePlayer = false;
            transform.DOMove(position, .4f).OnComplete(PlayerCanMoveAgain);
        }


        void PlayerCanMoveAgain()
        {
            _canMovePlayer = true;
        }

        // Set player initial position
        public void SetPlayerStartPosition()
        {
            _canMovePlayer = true;
            transform.position = Vector3.zero;
        }

    }
}
