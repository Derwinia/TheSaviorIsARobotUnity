using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InventoryMenu : MonoBehaviour
{
    [SerializeField] GameObject MenuButtonModel;
    [SerializeField] GameObject CloseMenuButton;

    private GameObject panel;
    private bool visible;
    private Vector3 hidePosition;

    List<Tool> inventory = new List<Tool>();
    List<GameObject> inventoryUI = new List<GameObject>();

    void Start()
    {
        visible = false;
        panel = transform.Find("InventoryPanel").gameObject;
        hidePosition = panel.transform.position;
    }

    public void ShowMenu()
    {
        if (!visible)
        {
            visible = true;
            panel.transform.position = hidePosition + new Vector3(0, 100, 0);
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

    public void updateMenu(List<Tool> inventory)
    {
        this.inventory = inventory;
        int nbItem = 0;
        foreach (Tool tool in inventory)
        {
            int posX = 0;
            int posY = 0;
            posX = posX + nbItem * 80;
            nbItem++;
            GameObject newButton = Instantiate(MenuButtonModel, new Vector3(posX, posY), Quaternion.identity);
            newButton.transform.SetParent(panel.transform, false);
            newButton.name = tool.Name;
            newButton.GetComponentInChildren<TextMeshProUGUI>().text = tool.Name;
            RectTransform rectPosition = newButton.GetComponentInChildren<RectTransform>();
            rectPosition.anchorMin = new Vector2(0,0.5f);
            rectPosition.anchorMax = new Vector2(0,0.5f);
            rectPosition.pivot = new Vector2(0, 0.5f);
        }
    }
}
