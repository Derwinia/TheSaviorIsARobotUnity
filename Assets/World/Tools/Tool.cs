using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Tool : Craftable
{
    private string name;
    private int craftingTime;
    private int maxDurability;
    private int durability;
    private int CraftingWoodcost;
    public Tool(string name, int craftingTime, int maxDurability, int CraftingWoodcost) : base(name, craftingTime, CraftingWoodcost)
    {
        this.maxDurability = maxDurability;
        durability = maxDurability;
    }
}
