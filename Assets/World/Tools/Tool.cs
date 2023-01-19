using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Tool : Craftable 
{
    public int MaxDurability { get { return maxDurability; } }
    private int maxDurability;
    public int Durability { get { return durability; } set { durability = value; } }
    private int durability;
    public Tool(string name, int craftingTime, int maxDurability, int CraftingWoodcost, Sprite picture) : base(name, craftingTime, CraftingWoodcost, picture)
    {
        this.maxDurability = maxDurability;
        durability = maxDurability;
    }
}
