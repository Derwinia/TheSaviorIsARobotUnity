using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    private List<Tool> toolInventory = new List<Tool>();
    private Tool selectedTool;

    InventoryMenu inventoryMenu;
    void Start()
    {
        toolInventory.Add(new Tool("Vide", 0, 99999, 0, null));
        inventoryMenu = FindObjectOfType<InventoryMenu>();
        inventoryMenu.UpdateMenu(toolInventory);
        selectedTool = toolInventory[0];
    }

    public bool AddTool(Tool tool)
    {
        if (CheckInventorySpace())
        {
            toolInventory.Add(tool);
            inventoryMenu.UpdateMenu(toolInventory);
            return true;
        }
        return false; 
    }

    public bool CheckInventorySpace()
    {
        if(toolInventory.Count < 8)return true; 
        return false;
    }

    public void SelectedTool(int tool)
    {
        selectedTool = toolInventory[tool];
    }
}
