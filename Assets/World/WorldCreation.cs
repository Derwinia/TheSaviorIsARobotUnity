using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class WorldCreation : MonoBehaviour
{
    RessourceHUD ressourceHUD;

    [SerializeField] GameObject grassTile;
    [SerializeField] GameObject Tree;
    [SerializeField] GameObject Trash;

    [SerializeField] int worldSize = 11;

    int gridSize = 10;

    int center;
    int pool;
    int trashPool;
    int treePool;

    public IList<Tile> WorldTiles { get { return worldTiles; } }
    IList<Tile> worldTiles = new List<Tile>();

    PlayerControl playerControl;

    private void Start()
    {
        ressourceHUD = FindObjectOfType<RessourceHUD>();
        playerControl = FindObjectOfType<PlayerControl>();
        if (worldSize % 2 == 0) worldSize += 1;
        center = ((worldSize - 1) / 2) + 1;

        pool = worldSize * worldSize - 9;
        trashPool = pool / 100 * 30;
        treePool = pool - trashPool;
        ressourceHUD.Co2 = trashPool;
        ressourceHUD.UpdateUI();
        createWorld();
    }

    void createWorld()
    {
        playerControl.transform.position = new Vector3(center * gridSize, 1, center * gridSize);
        for (int x = 1; x <= worldSize; x++)
        {
            for (int z = 1; z <= worldSize; z++)
            {
                GameObject grass = Instantiate(grassTile, new Vector3(x * gridSize, 0, z * gridSize), Quaternion.identity * Quaternion.Euler(0, 180, 0));
                grass.transform.SetParent(transform, false);
                if (x < center - 1 || x > center + 1)
                {
                    InstantiateRandom(grass);
                }
                else if (z < center - 1 || z > center + 1)
                {
                    InstantiateRandom(grass);
                }
                Tile tile = grass.GetComponent<Tile>();
                tile.InitTile();
                worldTiles.Add(tile);
            }
        }
    }

    private void InstantiateRandom(GameObject grass)
    {
        int rand = 0;
        bool needToSpawn = false;
        while(!needToSpawn)
        {
            rand = Random.Range(1, 3);
            switch (rand)
            {
                case 1:
                    if (treePool > 0)
                    {
                        needToSpawn = true;
                        GameObject tree = Instantiate(Tree, new Vector3(), Quaternion.identity);
                        tree.transform.SetParent(grass.transform, false);
                        treePool--;
                    }
                    break;
                case 2:
                    if (trashPool > 0)
                    {
                        needToSpawn = true;
                        GameObject trash = Instantiate(Trash, new Vector3(), Quaternion.identity);
                        trash.transform.SetParent(grass.transform, false);
                        trashPool--;
                    }
                    break;
            }
        }
    }
}
