using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;

public class Tile : MonoBehaviour
{
    private GameObject ghostInstance = null;
    public Building SelectedBuilding { get { return selectedBuilding; } }
    private Building selectedBuilding = null;

    GameObject buildingInstance = null;

    public bool IsConstructable { get { return isConstructable; } }
    private bool isConstructable;

    PlayerControl playerControl;

    IEnumerator CorConstruction;

    private void Start()
    {
        playerControl= FindObjectOfType<PlayerControl>();
    }

    public void InitTile()
    {
        if (transform.childCount == 0)
        {
            isConstructable = true;
        }
        else
        {
            isConstructable = false;
        }
    }

    public void ChooseBuilding(GameObject selected)
    {
        if (selected != null)
        {
            selectedBuilding = selected.GetComponent<Building>();
        }
        else
        {
            selectedBuilding = null;
        }
    }

    public void ConstructBuilding()
    {
        RessourceHUD ressources = FindObjectOfType<RessourceHUD>();
        if (ressources.RemoveResources(selectedBuilding.ConstructionWoodCost,0))
        {
            Destroy(ghostInstance);
            buildingInstance = Instantiate(selectedBuilding.BuildingPrefab, new Vector3(0, 1, 0), Quaternion.identity);
            buildingInstance.transform.SetParent(transform, false);
            CorConstruction = ConstructAnimation();
            StartCoroutine(CorConstruction);
        }
        else
        {
            Debug.Log("pas assez de ressources");
        }
    }

    public IEnumerator ConstructAnimation()
    {
        Building building = buildingInstance.GetComponent<Building>();
        foreach (Transform child in buildingInstance.transform)
        {
            child.transform.gameObject.SetActive(false);
        }
        foreach (Transform child in buildingInstance.transform)
        {
            yield return new WaitForSeconds(building.ConstructionTime);
            child.transform.gameObject.SetActive(true);
        }
        building.constructionReady= true;
    }

    private void OnMouseEnter()
    {
        if (isConstructable && !IsMouseOverUI() && selectedBuilding != null)
        {
            ghostInstance = Instantiate(selectedBuilding.BuildingPrefabGhost, new Vector3(), Quaternion.identity);
            ghostInstance.transform.SetParent(transform, false);
        }
    }

    private void OnMouseExit()
    {
        Destroy(ghostInstance);
    }

    private bool IsMouseOverUI()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }
}
