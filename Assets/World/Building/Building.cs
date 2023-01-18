using UnityEngine;

public abstract class Building : MonoBehaviour
{
    public GameObject BuildingPrefab { get { return buildingPrefab; } }
    [SerializeField] GameObject buildingPrefab;

    public GameObject BuildingPrefabGhost { get { return buildingPrefabGhost; } }
    [SerializeField] GameObject buildingPrefabGhost;

    public float ConstructionTime { get { return constructionTime; } }
    [SerializeField] float constructionTime;

    public int ConstructionWoodCost { get { return constructionWoodCost; } }
    [SerializeField] int constructionWoodCost;

    public bool constructionReady = false;

    public abstract void SelectedBuilding();

}
