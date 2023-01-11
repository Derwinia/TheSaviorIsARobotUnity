using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class WorldCreation : MonoBehaviour
{
    [SerializeField] GameObject grassTile;
    [SerializeField] GameObject Tree;

    [SerializeField] int worldSize = 5;
    int gridSize = 10;
    int center = 0;

    IList<Tile> worldTiles = new List<Tile>();
    public IList<Tile> WorldTiles { get { return worldTiles; } }

    private void Start()
    {
        center = ((worldSize-1)/2)+1;
        createWorld();
    }

    void createWorld()
    {
        for(int x=1; x <= worldSize; x++)
        {
            for(int z=1; z <= worldSize; z++)
            {
                GameObject grass = Instantiate(grassTile, new Vector3(x*gridSize, 0, z * gridSize), Quaternion.identity * Quaternion.Euler(0,180,0));
                grass.transform.SetParent(transform, false);
                if (x < center - 1 || x > center + 1)
                {
                    GameObject tree = Instantiate(Tree, new Vector3(), Quaternion.identity);
                    tree.transform.SetParent(grass.transform, false);
                }
                else if (z < center - 1 || z > center + 1)
                {
                    GameObject tree = Instantiate(Tree, new Vector3(), Quaternion.identity);
                    tree.transform.SetParent(grass.transform, false);
                }
                Tile tile = grass.GetComponent<Tile>();
                tile.InitTile();
                worldTiles.Add(tile);
            }
        }
    }
}
