using UnityEngine;
using UnityEngine.EventSystems;

public class Tile : MonoBehaviour
{
    private GameObject building = null;
    private GameObject buildingGhost = null;
    private GameObject ghostInstance = null;

    private bool isConstructable;
    public bool IsConstructable { get { return isConstructable; } }
    private bool constructed = false;

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
            Building detailsBuilding = selected.GetComponent<Building>();
            building = detailsBuilding.BuildingPrefab;
            buildingGhost = detailsBuilding.BuildingPrefabGhost;
        }
        else
        {
            building = null;
            buildingGhost = null;
        }
    }

    private void OnMouseEnter()
    {
        if (isConstructable && !IsMouseOverUI() && building != null)
        {
            ghostInstance = Instantiate(buildingGhost, new Vector3(), Quaternion.identity);
            ghostInstance.transform.SetParent(transform, false);
            isConstructable = false;
        }
    }

    private void OnMouseDown()
    {
        if (!constructed && !IsMouseOverUI() && building != null)
        {
            
            Destroy(ghostInstance);
            GameObject buildingInstance = Instantiate(building, new Vector3(), Quaternion.identity);
            buildingInstance.transform.SetParent(transform, false);
            constructed = true;
        }
    }

    private void OnMouseExit()
    {
        if (!constructed && !IsMouseOverUI())
        {
            Destroy(ghostInstance);
            isConstructable = true;
        }
    }

    private bool IsMouseOverUI()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }
}
