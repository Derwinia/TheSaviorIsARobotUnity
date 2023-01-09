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

    public bool RemoveWood(int quantity)
    {
        if(wood - quantity >= 0) {
            wood -= quantity;
            UpdateUI();
            return true;
        }
        return false;
    }

    private void UpdateUI()
    {
        woodDisplay.text = ("Bois : " + wood);
    }
}
