using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Craftable 
{
    public string Name { get { return name; } }
    private string name;

    public int CraftingTime { get { return craftingTime; } }
    private int craftingTime;

    public int CraftingWoodCost { get { return craftingWoodCost; } }
    private int craftingWoodCost;
    
    public Sprite Picture { get { return picture; } }
    private Sprite picture;


    public Craftable(string name, int craftingTime, int craftingWoodCost, Sprite picture)
    {
        this.name = name;
        this.craftingTime = craftingTime;
        this.craftingWoodCost = craftingWoodCost;
        this.picture = picture;
    }
}
