using System.Collections;
using UnityEngine;

public class Trash : Ressource
{
    private int HarvestDelay = 1;
    private int Co2 = 1;

    public override IEnumerator Harvest()
    {
        isUsed = true;
        RessourceHUD ressources = FindObjectOfType<RessourceHUD>();
        yield return new WaitForSeconds(HarvestDelay);
        if (isUsed)
        {
            ressources.RemoveResources(0,Co2);
        }
        Destroy(gameObject);
    }
}
