using UnityEngine;

public class Grid : MonoBehaviour
{
    [SerializeField]
    private float size = 1f;

    public Vector3 GetNearestPointOnGrid(Vector3 position)
    {
        position -= transform.position;

        int xCount = Mathf.RoundToInt(position.x / size);
        int yCount = Mathf.RoundToInt(position.y / size);
        int zCount = Mathf.RoundToInt(position.z / size);
          
        Vector3 result = new Vector3(
            (float)xCount * size,
            (float)yCount * size,
            (float)zCount * size);

        result += transform.position;

        return result;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        for (float Rows = 0; Rows < 20; Rows += size)
        {
            for (float Columns = 0; Columns < 20; Columns += size)
            {
                if(Columns == Random.Range(0,20))
                {
                    var point = GetNearestPointOnGrid(new Vector3(Rows, 0f, Columns));
                    Gizmos.DrawCube(point, Vector3.one);
                }
                
            }

        }
    }

}
