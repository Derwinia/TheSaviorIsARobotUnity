using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Craftable
{
    public string Name { get { return name; } }
    private string name;

    public int CraftingTime { get { return craftingTime; } }
    private int craftingTime;

    public int CraftingWoodCost { get { return craftingWoodCost; } }
    private int craftingWoodCost;
    public Craftable(string name, int craftingTime, int craftingWoodCost)
    {
        this.name = name;
        this.craftingTime = craftingTime;
        this.craftingWoodCost = craftingWoodCost;
    }
}
