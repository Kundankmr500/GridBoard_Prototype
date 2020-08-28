using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GridBehaviour : MonoBehaviour
{
    public float X_Start, Z_Start;
    public int ColumnLength, RowLength;
    public float X_Space, Z_Space;
    public GameObject GridPrefab;
    public List<GameObject> GridItemList;
    public Material DemoMat;
    [Header("Enable this to see the score up tiles in green color")]
    public bool ScoreUpTileVisible;


    private int[,] Grid;

    private void Awake()
    {
        if (GridPrefab)
        {
            GenerateGrid();
            GenerateRandomNoOfItems();
        }
        else
        {
            Debug.Log("Missing GridPrefab, please assign gridPrefab");
        }
    }

    public void GenerateGrid()
    {
        for (int i = 0; i < ColumnLength; i++)
        {
            for (int j = 0; j < RowLength; j++)
            {
                SpawnObj(i, j);
            }
        }
    }

    
    private void SpawnObj(int x, int y)
    {
        GameObject obj = Instantiate(GridPrefab, new Vector3(X_Start + (X_Space * x), 0.01f, Z_Start + (Z_Space * y)),
                         Quaternion.Euler(90, 0, 0));
        obj.name = "X: " + x + " Y: " + y;
        obj.transform.SetParent(transform);
        GridItemList.Add(obj);
    }

    // Generating items in the grid
    private void GenerateRandomNoOfItems()
    {
        for (int i = 0; i < GameHandler.Instance.RandomTileCount; i++)
        {
            int randomIndex = Random.Range(0, GridItemList.Count);
            GridItemList[randomIndex].gameObject.tag = Constant.SelectedTileTag;
            if(ScoreUpTileVisible)
                GridItemList[randomIndex].gameObject.GetComponent<Renderer>().material = DemoMat;
            GridItemList.RemoveAt(randomIndex);
        }
    }
}

