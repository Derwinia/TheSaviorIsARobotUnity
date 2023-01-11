using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Ressources : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI woodDisplay;

    private int wood;
    public int Wood { get { return wood; } }

    void Start()
    {
        wood = 0;
        UpdateUI();
    }

    public void AddWood(int quantity)
    {
        wood += quantity;
        UpdateUI();
    }

    public bool removeResources(int wood)
    {
        if (CheckIfEnough(wood))
        {
            this.wood -= wood;
            UpdateUI();
            return true;
        }
        return false;
    }

    public bool CheckIfEnough(int wood)
    {
        if (this.wood - wood < 0) return false;
        return true;
    }

    private void UpdateUI()
    {
        woodDisplay.text = ("Bois : " + wood);
    }
}
