using UnityEngine;

namespace Game
{
    public class Cell : MonoBehaviour
    {
        public CellType CellType;

        public void ChangeCellMat(Material specialTileMaterial)
        {
            GetComponent<Renderer>().material = specialTileMaterial;
        }

        public void ChangeCellType(CellType cellType)
        {
            CellType = cellType;
        }
    }


    public enum CellType
    {
        None,
        Special,
        Normal
    }
}