using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Factory : Building
{
    [SerializeField] Sprite hachePic;

    private BuildingMenu buildingMenu;

    //private int level;

    private BuildingTask factoryTask;

    private void Start()
    {
        buildingMenu = FindObjectOfType<BuildingMenu>();

        //level = 1;

        factoryTask = new BuildingTask();
        Tool tool = new Tool("Hache", 5, 4, 1, hachePic);
        factoryTask.craftableToolList.Add(tool);
    }

    public override void SelectedBuilding()
    {
        buildingMenu.ShowSelectedBuilding(factoryTask);
    }

}
