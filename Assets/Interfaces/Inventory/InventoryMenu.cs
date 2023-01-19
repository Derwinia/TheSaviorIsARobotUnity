using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryMenu : MonoBehaviour
{
    [SerializeField] GameObject MenuButtonModel;
    [SerializeField] GameObject CloseMenuButton;

    private PlayerInventory playerInventory;
    private GameObject panel;
    private bool visible;
    private Vector3 hidePosition;

    List<GameObject> inventoryUI = new List<GameObject>();

    void Start()
    {
        playerInventory = FindObjectOfType<PlayerInventory>();
        visible = false;
        panel = transform.Find("InventoryPanel").gameObject;
        hidePosition = panel.transform.position;
    }

    public void ShowMenu()
    {
        if (!visible)
        {
            visible = true;
            panel.transform.position = hidePosition + new Vector3(0,-hidePosition.y,0);
            CloseMenuButton.SetActive(true);
        }
    }

    public void HideMenu()
    {
        if (visible)
        {
            visible = false;
            panel.transform.position = hidePosition;
            CloseMenuButton.SetActive(false);
        }
    }

    public void UpdateMenu()
    {
        List<Tool> inventory = playerInventory.ToolInventory;
        foreach (GameObject tool in inventoryUI)
        {
            Destroy(tool);
        }
        int nbItem = 0;
        foreach (Tool tool in inventory)
        {
            float posX = 5f; 
            int posY = 0;
            posX = (int)(posX + nbItem * 95f);

            
            GameObject newButton = Instantiate(MenuButtonModel, new Vector3(posX, posY), Quaternion.identity);
            newButton.transform.SetParent(panel.transform, false);
            newButton.name = tool.Name;
            newButton.transform.Find("Name").GetComponent<TextMeshProUGUI>().text = tool.Name;
            newButton.transform.Find("Durability").GetComponent<TextMeshProUGUI>().text = tool.Durability +"/"+tool.MaxDurability;
            newButton.GetComponent<Button>().onClick.AddListener(() => ChooseTool(tool));
            Image[] images = newButton.GetComponentsInChildren<Image>();
            foreach (Image image in images)
            {
                if (image.transform.name == "Picture") image.sprite = tool.Picture;
            }
                //.sprite = tool.Picture;
            inventoryUI.Add(newButton);
            RectTransform rectPosition = newButton.GetComponentInChildren<RectTransform>();
            rectPosition.anchorMin = new Vector2(0,0.5f);
            rectPosition.anchorMax = new Vector2(0,0.5f);
            rectPosition.pivot = new Vector2(0, 0.5f);
            nbItem++;
        }
    }

    public void ChooseTool(Tool tool)
    {
        
        playerInventory.ChangeTool(tool);

    }
}
