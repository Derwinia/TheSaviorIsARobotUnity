using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class Ressource : MonoBehaviour
{
    private PlayerControl playercontrol;
    
    [SerializeField] int harvestDelay;
    public int HarvestDelay { get { return harvestDelay; } }

    public bool isUsed = false;

    private void Start()
    {
        playercontrol = FindObjectOfType<PlayerControl>();
    }

    public bool InRange()
    {
        if (!IsMouseOverUI() && !isUsed)
        {
            if (Vector3.Distance(transform.position, playercontrol.transform.position) < playercontrol.ActionRange) return true;
        }
        return false;
    }

    public IEnumerator Harvest()
    {
        isUsed = true;
        RessourceHUD ressources = FindObjectOfType<RessourceHUD>();
        foreach (Transform child in transform)
        {
            if (child.gameObject.activeInHierarchy)
            {
                yield return new WaitForSeconds(HarvestDelay);
                if(isUsed)
                {
                    child.gameObject.SetActive(false);
                    ressources.AddWood(1);
                }
            }
        }
        playercontrol.ressource = null;
        Destroy(gameObject);
    }

    private bool IsMouseOverUI()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }
}
