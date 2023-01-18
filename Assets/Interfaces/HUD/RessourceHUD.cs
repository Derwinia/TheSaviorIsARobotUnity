using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RessourceHUD : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI woodDisplay;
    [SerializeField] TextMeshProUGUI co2Display;

    public int Wood { get { return wood; } }
    private int wood;

    public int Co2 { get { return co2; } set { co2 = value; } }
    private int co2 = 0;

    void Start()
    {
        wood = 10;
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

    public void UpdateUI()
    {
        woodDisplay.text = ("Bois : " + wood);
        co2Display.text = ("CO2 : "+ co2);
    }
}
