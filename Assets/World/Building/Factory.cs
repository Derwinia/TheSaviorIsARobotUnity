using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Factory : Building
{
    private BuildingMenu buildingMenu;

    //private int level;

    private BuildingTask factoryTask;

    private void Start()
    {
        buildingMenu = FindObjectOfType<BuildingMenu>();

        //level = 1;

        factoryTask = new BuildingTask();
        Tool tool = new Tool("Hache", 5, 4, 1);
        factoryTask.craftableToolList.Add(tool);
    }

    private void OnMouseDown()
    {
        buildingMenu.ShowSelectedBuilding(factoryTask);
    }

}
