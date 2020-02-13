using UnityEngine;

public class GridBehaviour : MonoBehaviour
{
    public float X_Start, Z_Start;
    public int ColumnLength, RowLength;

    public float X_Space, Z_Space;

    public GameObject GridPrefab;
    
    private void Awake()
    {
        if (GridPrefab)
        {
            GenerateGrid();
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
        }
    }
    
}

