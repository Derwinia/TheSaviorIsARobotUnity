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


    public void ConstructBuilding()
    {

    }
}
