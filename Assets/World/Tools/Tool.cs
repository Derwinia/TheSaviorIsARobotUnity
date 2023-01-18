using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Tool : Craftable
{
    private string name;
    private int craftingTime;
    private int maxDurability;
    private int durability;
    private int craftingWoodcost;
    private Sprite picture;
    public Tool(string name, int craftingTime, int maxDurability, int CraftingWoodcost, Sprite picture) : base(name, craftingTime, CraftingWoodcost, picture)
    {
        this.maxDurability = maxDurability;
        durability = maxDurability;
    }
}
