using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;

public class Tile : MonoBehaviour
{
    private GameObject buildingGhost = null;
    private GameObject ghostInstance = null;
    private Building building = null;

    private bool constructMode = false;
    private bool isConstructable;
    public bool IsConstructable { get { return isConstructable; } }
    private bool constructed = false;

    PlayerControl playerControl;

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
            constructMode = true;
            building = selected.GetComponent<Building>();
            buildingGhost = building.BuildingPrefabGhost;
        }
        else
        {
            constructMode = false;
            buildingGhost = null;
        }
    }

    private void OnMouseEnter()
    {
        if (isConstructable && !IsMouseOverUI() && buildingGhost != null)
        {
            ghostInstance = Instantiate(buildingGhost, new Vector3(), Quaternion.identity);
            ghostInstance.transform.SetParent(transform, false);
            isConstructable = false;
        }
    }

    private void OnMouseDown()
    {

        if (constructMode)
        {
            if (!constructed && !IsMouseOverUI() && buildingGhost != null)
            {
                if (building.ConstructBuilding(transform))
                {
                    Destroy(ghostInstance);
                    constructed = true;
                }
            }
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
