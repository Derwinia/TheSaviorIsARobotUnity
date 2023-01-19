using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public List<Tool> ToolInventory { get { return toolInventory; } }
    private List<Tool> toolInventory = new List<Tool>();

    public Tool SelectedTool { get { return selectedTool; } }
    private Tool selectedTool;

    InventoryMenu inventoryMenu;
    void Start()
    {
        inventoryMenu = FindObjectOfType<InventoryMenu>();
        AddTool(new Tool("Vide", 0, 99999, 0, null));
        selectedTool = toolInventory[0];
    }

    public bool AddTool(Tool tool)
    {
        if (CheckInventorySpace())
        {
            toolInventory.Add(tool);
            inventoryMenu.UpdateMenu();
            return true;
        }
        return false; 
    }

    public bool CheckInventorySpace()
    {
        if(toolInventory.Count < 8)return true; 
        return false;
    }

    public void ChangeTool(Tool tool)
    {
        selectedTool = tool;
        Debug.Log("Durabilité de l'outil selectionner = " +selectedTool.Durability);
    }

    public bool UseTool()
    {
        selectedTool.Durability--;
        Debug.Log("Durabilité de l'outil apres utilisation = " + selectedTool.Durability);
        if (selectedTool.Durability <= 0)
        {
            toolInventory.Remove(selectedTool);
            selectedTool = ToolInventory[0];
            inventoryMenu.UpdateMenu();
            return false;
        }
        else
        {
            inventoryMenu.UpdateMenu();
            return true;
        }
    }
}
