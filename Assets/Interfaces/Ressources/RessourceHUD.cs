using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using TMPro;
using UnityEngine;
using System.Text.Json;
using System.Runtime.Serialization.Json;

public class RessourceHUD : MonoBehaviour
{
    World world;

    [SerializeField] TextMeshProUGUI woodDisplay;
    [SerializeField] TextMeshProUGUI co2Display;

    public int Wood { get { return wood; } }
    [SerializeField]private int wood = 0;

    public int Co2 { get { return co2; } }
    private int co2;

    void Start()
    {
        world = FindObjectOfType<World>();

        co2 = world.TrashInWorld;

        UpdateUI();
    }

    public void AddWood(int quantity)
    {
        wood += quantity;
        UpdateUI();
    }

    public bool RemoveResources(int wood, int co2)
    {
        if (!CheckIfEnoughWood(wood))return false;
        this.wood -= wood;
        if(co2 != 0)
        {
            this.co2 -= co2;
            using HttpClient c = new HttpClient();
            var r = c.PostAsync("https://localhost:7165/api/world/?id=1&co2=1", null).Result;
        }
        UpdateUI();
        return true;
    }

    public bool CheckIfEnoughWood(int wood)
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
