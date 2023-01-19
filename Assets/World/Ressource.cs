using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class Ressource : MonoBehaviour
{
    public PlayerControl Playercontrol { get { return playercontrol; } }
    private PlayerControl playercontrol;

    public PlayerInventory PlayerInventory { get { return playerInventory; } }
    private PlayerInventory playerInventory;

    public bool isUsed = false;

    private void Start()
    {
        playercontrol = FindObjectOfType<PlayerControl>();
        playerInventory = FindObjectOfType<PlayerInventory>();
    }

    public abstract IEnumerator Harvest();
}
