using System.Collections;
using UnityEngine;

public class Tree : Ressource
{
    int HarvestDelay = 4;
    int wood = 1;

    bool correctTool;

    public override IEnumerator Harvest()
    {
        correctTool = false;
        if (PlayerInventory.SelectedTool.Name == "Hache")
        {
            correctTool = true;
        }
        isUsed = true;
        RessourceHUD ressources = FindObjectOfType<RessourceHUD>();
        foreach (Transform child in transform)
        {
            if (child.gameObject.activeInHierarchy)
            {
                yield return new WaitForSeconds(HarvestDelay / (correctTool?4:1));
                if (isUsed)
                {
                    if (correctTool)
                    {
                        if (!PlayerInventory.UseTool()) correctTool = false;
                    }
                    child.gameObject.SetActive(false);
                    ressources.AddWood(wood);
                }
            }
        }
        Destroy(gameObject);
    }
}