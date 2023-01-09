using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    List<Tool> inventory = new List<Tool>();

    InventoryMenu inventoryMenu;
    void Start()
    {
        inventory.Add(new Tool("Vide", 0, 99999, 0));
        inventoryMenu = FindObjectOfType<InventoryMenu>();
        inventoryMenu.updateMenu(inventory);
    }

    public bool AddTool(Tool tool)
    {
        if (inventory.Count < 8)
        {
            inventory.Add(tool);
            inventoryMenu.updateMenu(inventory);
            return true;
        }
        return false;
    }
}
