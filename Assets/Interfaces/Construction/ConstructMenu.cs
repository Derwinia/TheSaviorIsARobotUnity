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
    private World world;

    private bool visible;
    private Vector3 hidePosition;

    PlayerControl playerControl;

    void Start()
    {
        world = FindObjectOfType<World>();
        visible = false;
        panel = transform.Find("ConstructPanel").gameObject;
        hidePosition = panel.transform.position;
        playerControl = FindObjectOfType<PlayerControl>();
    }

    public void ShowMenu()
    {
        if (!visible)
        {
            visible = true;
            playerControl.constructMode = true;
            panel.transform.position = hidePosition - new Vector3(hidePosition.x - Screen.width,0,0);
        }
    }

    public void HideMenu()
    {
        if (visible)
        {
            visible = false;
            playerControl.constructMode = false;
            panel.transform.position = hidePosition;
            foreach (Tile tile in world.WorldTiles)
            {
                if (tile.IsConstructable) tile.ChooseBuilding(null);
            }
        }
    }

    public void SelectBuilding(GameObject building)
    {
        foreach(Tile tile in world.WorldTiles)
        {
            if (tile.IsConstructable) tile.ChooseBuilding(building);
        }
    }
}
