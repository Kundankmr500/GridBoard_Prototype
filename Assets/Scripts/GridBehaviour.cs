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
        for (int i = 0; i < ColumnLength * RowLength; i++)
        {
            GameObject obj = Instantiate(GridPrefab, new Vector3(X_Start + (X_Space * (i % ColumnLength)), 0.01f ,Z_Start + (Z_Space * (i / ColumnLength))),
                Quaternion.Euler(90,0,0));
            obj.transform.SetParent(transform);
            GridItemList.Add(obj);
        }
    }


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

