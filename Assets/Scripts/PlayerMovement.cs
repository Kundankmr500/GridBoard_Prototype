using DG.Tweening;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public GridBehaviour GridInstance;
    public int PlayerColumnIndex { get; set; }
    public int PlayerRowIndex { get; set; }
    public KeyCode PlayerInput_Up;
    public KeyCode PlayerInput_Down;
    public KeyCode PlayerInput_Left;
    public KeyCode PlayerInput_Right;
    public KeyCode PlayerTileSelection;
    public Material SelectionMaterial;
    public Material FalseSelectionMaterial;
    public Material NearSelectionMaterial;
    public GameObject PlayerObj;

    private bool _canMovePlayer;
    private bool _isFoundTile;
    private GameObject _selectedTile;
    

    private void Start()
    {
        SetPlayerStartPosition();
        _isFoundTile = false;
    }


    private void Update()
    {
        if(_canMovePlayer )
            CheckPlayerInputMovement();
        TileCheck();
        
    }


    public void TileCheck()
    {
        if (Input.GetMouseButtonDown(0))
        {
            DoRaycastCheck();
        }
        else if (Input.GetKeyDown(PlayerTileSelection) && _isFoundTile)
        {
            ChangeTileColor();
        }
        else if (Input.GetKeyDown(PlayerTileSelection))
        {
            GameHandler.Instance.CheckForPlayerChance();
            ChangeTileColorToFalseSelection();
        }
    }


    public void DoRaycastCheck()
    {
        RaycastHit hitInfo;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hitInfo))
        {
            _selectedTile = hitInfo.collider.gameObject;
            if (hitInfo.collider.CompareTag(Constant.SelectedTileTag))
            {
                ChangeTileColor();
            }
            else
            {
                GameHandler.Instance.CheckForPlayerChance();
                ChangeTileColorToFalseSelection();
            }
        }
    }


    public void ChangeTileColor()
    {
        _selectedTile.GetComponent<Renderer>().material = SelectionMaterial;
        GameHandler.Instance.CalculateScore();
    }


    public void ChangeTileColorToFalseSelection()
    {
        _selectedTile.GetComponent<Renderer>().material = FalseSelectionMaterial;
    }


    public void OnTriggerEnter(Collider other)
    {
        _selectedTile = other.gameObject;
        if (other.CompareTag(Constant.SelectedTileTag))
        {
            _isFoundTile = true;
        }
    }


    public void CheckPlayerInputMovement()
    {
        if (Input.GetKeyDown(PlayerInput_Down))
        {
            if (PlayerColumnIndex > 0)
            {
                MovePlayer(PlayerObj.transform.position - new Vector3(0, 0, GridInstance.Z_Space));
                PlayerColumnIndex--;
            }
        }
        
        else if (Input.GetKeyDown(PlayerInput_Up))
        {
            if (PlayerColumnIndex < GridInstance.ColumnLength - 1)
            {
                MovePlayer(PlayerObj.transform.position + new Vector3(0, 0, GridInstance.Z_Space));
                PlayerColumnIndex++;
            }
        }
        
        if (Input.GetKeyDown(PlayerInput_Left))
        {
            if (PlayerRowIndex > 0)
            {
                MovePlayer(PlayerObj.transform.position - new Vector3(GridInstance.X_Space, 0, 0));
                PlayerRowIndex--;
            }
        }
        
        else if (Input.GetKeyDown(PlayerInput_Right))
        {
            if (PlayerRowIndex < GridInstance.RowLength - 1)
            {
                MovePlayer(PlayerObj.transform.position + new Vector3(GridInstance.X_Space, 0, 0));
                PlayerRowIndex++;
            }
        }
    }

    void MovePlayer(Vector3 position)
    {
        _canMovePlayer = false;
        _isFoundTile = false;
        PlayerObj.transform.DOMove(position, .4f).OnComplete(PlayerCanMove);
    }

    void PlayerCanMove()
    {
        _canMovePlayer = true;
    }


    public void SetPlayerStartPosition()
    {
        _canMovePlayer = true;
        PlayerObj.transform.position = new Vector3(GridInstance.X_Start + (GridInstance.X_Space * (PlayerRowIndex % GridInstance.RowLength)), 0 ,GridInstance.Z_Start + (GridInstance.Z_Space * (PlayerColumnIndex % GridInstance.ColumnLength)));
    }

}
