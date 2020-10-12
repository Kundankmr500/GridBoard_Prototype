using UnityEngine;

public class Tile : MonoBehaviour
{
    public TileType TileType;
    private Material defaultTileMaterial;


    void Start()
    {
        
    }


    public void ChangeTileMat(Material specialTileMaterial, TileType tileType = TileType.Normal)
    {
        GetComponent<Renderer>().material = specialTileMaterial;
        TileType = tileType;
    }

}


public enum TileType
{
    None,
    Special,
    Normal
}
