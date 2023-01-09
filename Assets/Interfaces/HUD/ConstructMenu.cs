using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class ConstructMenu : MonoBehaviour
{

    private GameObject panel;
    private WorldCreation worldCreation;

    private bool visible;
    private Vector3 hidePosition;

    void Start()
    {
        worldCreation = FindObjectOfType<WorldCreation>();
        visible = false;
        panel = transform.Find("ConstructPanel").gameObject;
        hidePosition = panel.transform.position;
    }

    public void ShowMenu()
    {
        if (!visible)
        {
            visible = true;
            panel.transform.position = hidePosition - new Vector3(hidePosition.x - Screen.width,0,0);
        }
    }

    public void HideMenu()
    {
        if (visible)
        {
            visible = false;
            panel.transform.position = hidePosition;
            foreach (Tile tile in worldCreation.WorldTiles)
            {
                if (tile.IsConstructable) tile.ChooseBuilding(null);
            }
        }
    }

    public void SelectBuilding(GameObject building)
    {
        foreach(Tile tile in worldCreation.WorldTiles)
        {
            if (tile.IsConstructable) tile.ChooseBuilding(building);
        }
    }
}
