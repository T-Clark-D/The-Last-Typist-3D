using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] private int width, height;
    [SerializeField] public TileScript Tile;

    private Dictionary<Vector3, TileScript> tiles;

    // Start is called before the first frame update
    void Start()
    {
        GenerateGrid();
    }
    //generates a grid of given width and height
    private void GenerateGrid()
    {
        tiles = new Dictionary<Vector3, TileScript>();
        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < height; z++)
            {
                var spawnedTile = Instantiate(Tile, new Vector3(x,0,z), Quaternion.identity, gameObject.transform);
                spawnedTile.name = $"Tile {x}{z}";

                var isOffset = (x % 2 == 0 && z % 2 != 0) || (x % 2 != 0 && z % 2 == 0);
                spawnedTile.gameObject.GetComponent<TileScript>().Init(isOffset);

                tiles[new Vector3(x, 0, z)] = spawnedTile;
            }
        }
    }

    //use to access tile through script
    public TileScript getTileAtPosition(Vector3 pos)
    {
        if(tiles.TryGetValue(pos, out var tile))
        {
            return tile;
        }
        return null;
    }
    /*
    public void setGridToBuildMode(bool status)
    {
        foreach (var tile in tiles)
        {
            tile.Value.setBuildMode(status);
        }
    }*/
}
