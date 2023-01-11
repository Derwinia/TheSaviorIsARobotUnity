using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour 
{
    [SerializeField] GameObject buildingPrefab;
    public GameObject BuildingPrefab { get { return buildingPrefab; } }
    
    [SerializeField] GameObject buildingPrefabGhost;
    public GameObject BuildingPrefabGhost { get { return buildingPrefabGhost; } }
    
    [SerializeField] int constructionTime;
    public int ConstructionTime { get { return constructionTime; } }

    [SerializeField] int constructionWoodCost;
    public int ConstructionWoodCost { get { return constructionWoodCost; } }

    private Ressources ressources;

    public bool ConstructBuilding(Transform transform)
    {
        ressources = FindObjectOfType<Ressources>();
        if (ressources.CheckIfEnough(constructionWoodCost))
        {
            ressources.removeResources(constructionWoodCost);
            GameObject buildingInstance = Instantiate(buildingPrefab, new Vector3(0,1,0), Quaternion.identity);
            buildingInstance.transform.SetParent(transform, false);
            return true;
        }
        return false;
    }
}
