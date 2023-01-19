using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuildingMenu : MonoBehaviour
{
    [SerializeField] GameObject MenuButtonModel;

    private RessourceHUD ressources;
    private PlayerInventory playerInventory;

    private GameObject panel;
    private bool visible;
    private Vector3 hidePosition;
    private List<GameObject> list = new List<GameObject>();

    void Start()
    {
        visible = false;
        panel = transform.Find("BuildingPanel").gameObject;
        hidePosition = panel.transform.position;
        ressources = FindObjectOfType<RessourceHUD>();
        playerInventory= FindObjectOfType<PlayerInventory>();
    }

    public void ShowSelectedBuilding(BuildingTask tasks)
    {
        if (!visible)
        {
            visible = true;
            panel.transform.position = hidePosition - new Vector3(hidePosition.x - Screen.width, 0, 0);
        }
        FillMenu(tasks);
        
    }

    private void FillMenu(BuildingTask tasks)
    {
        int nbButton = 0;
        foreach (Tool tool in tasks.craftableToolList)
        {
            nbButton++;

            float posX = -50f;
            float posY = 425f;
            if (nbButton % 2 == 0) posX = -posX;
            if(nbButton > 2)
            {
                if((nbButton-2) % 2 == 1)
                posY = posY - (Mathf.FloorToInt((nbButton-1)/2)* 95f);
            }
            GameObject newButton = Instantiate(MenuButtonModel, new Vector3(posX, posY), Quaternion.identity);
            newButton.transform.SetParent(panel.transform, false);
            newButton.name = nbButton.ToString();
            newButton.GetComponentInChildren<TextMeshProUGUI>().text = tool.Name;
            newButton.GetComponent<Button>().onClick.AddListener(delegate { Craft(tool); });
            Image[] images = newButton.GetComponentsInChildren<Image>();
            foreach (Image image in images)
            {
                if (image.transform.name == "Image") image.sprite = tool.Picture;
            }
            list.Add(newButton);
        }
    }

    public void HideSelectedBuilding()
    {
        if (visible)
        {
            visible = false;
            panel.transform.position = hidePosition;
            foreach(GameObject button in list) 
            {
                Destroy(button);
            }
        }
    }

    public void Craft(Tool tool)
    {
        Debug.Log("Durabilité de l'outil créer = " + tool.Durability);
        if(ressources.CheckIfEnoughWood(tool.CraftingWoodCost))
        {
            Tool copyTool = new Tool(tool.Name, tool.CraftingTime, tool.Durability, tool.CraftingWoodCost, tool.Picture);
            if (playerInventory.AddTool(copyTool))
            {
                ressources.RemoveResources(tool.CraftingWoodCost,0);
            }
        }
    }
}
