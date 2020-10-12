using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GridBehaviour : MonoBehaviour
{
    public GameHandler GameHandler;
    public float X_Start, Z_Start;
    public int ColumnLength, RowLength;
    public float X_Space, Z_Space;
    public Tile TilePrefab;
    public Transform TileParent;
    public List<Tile> SpecialTileList;
    public Material DemoMat;
    [Header("Enable this to see the score up tiles in green color")]
    public bool ScoreUpTileVisible;

    private Tile[,] Grid;
    //private int[,] Grid;

    private void Awake()
    {
        if (TilePrefab)
        {
            GenerateGrid();
            //GenerateRandomNoOfItems();
        }
        else
        {
            Debug.Log("Missing GridPrefab, please assign gridPrefab");
        }
    }

    public void GenerateGrid()
    {
        //for (int i = 0; i < ColumnLength; i++)
        //{
        //    for (int j = 0; j < RowLength; j++)
        //    {
        //        //SpawnObj(i, j);
        //    }
        //}
        Grid = new Tile[RowLength, ColumnLength];
        for (int i = 0; i < GameHandler.SpecialTileCount; i++)
        {
            PlaceSpecialTileRandomely();
        }
        PlaceNormalTileOnBoard();
    }


    //private void SpawnObj(int x, int y)
    //{
    //    GameObject obj = Instantiate(GridPrefab, new Vector3(X_Start + (X_Space * x), 0.01f, Z_Start + (Z_Space * y)),
    //                     Quaternion.Euler(90, 0, 0));
    //    obj.name = "X: " + x + " Y: " + y;
    //    obj.transform.SetParent(transform);
    //    GridItemList.Add(obj);
    //}

    // Generating items in the grid
    //private void GenerateRandomNoOfItems()
    //{
    //    for (int i = 0; i < GameHandler.Instance.RandomTileCount; i++)
    //    {
    //        int randomIndex = Random.Range(0, GridItemList.Count);
    //        GridItemList[randomIndex].gameObject.tag = Constant.SelectedTileTag;
    //        if(ScoreUpTileVisible)
    //            GridItemList[randomIndex].gameObject.GetComponent<Renderer>().material = DemoMat;
    //        GridItemList.RemoveAt(randomIndex);
    //    }
    //}


    // Placing all special Tiles on the board
    private void PlaceSpecialTileRandomely()
    {
        int XIndex = Random.Range(0, RowLength);
        int YIndex = Random.Range(0, ColumnLength);

        if (Grid[XIndex, YIndex] == null)
        {
            Tile specialTile = Instantiate(TilePrefab,
                                  new Vector3(XIndex, YIndex, 0), Quaternion.identity, TileParent) as Tile;

            Grid[XIndex, YIndex] = specialTile;
            specialTile.ChangeTileMat(GameHandler.SpecialTileMaterial, TileType.Special);
            SpecialTileList.Add(specialTile);
        }
        else
        {
            PlaceSpecialTileRandomely();
        }
    }


    // Placing all Normal Tiles on the board
    private void PlaceNormalTileOnBoard()
    {
        for (int xIndex = 0; xIndex < RowLength; xIndex++)
        {
            for (int yIndex = 0; yIndex < ColumnLength; yIndex++)
            {
                if (Grid[xIndex, yIndex] == null)
                {
                    Tile normalTile = Instantiate(TilePrefab, new Vector3(xIndex, yIndex, 0),
                                        Quaternion.identity, TileParent);
                    Grid[xIndex, yIndex] = normalTile;
                }

            }
        }
    }
}

