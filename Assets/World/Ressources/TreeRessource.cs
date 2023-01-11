using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class TreeRessource : MonoBehaviour
{
    [SerializeField] int cutDelay = 1;
    private bool isUsed = false;
    private void OnMouseDown()
    {
        if (!IsMouseOverUI() && !isUsed) StartCoroutine(CutTree());
    }
    IEnumerator CutTree()
    {
        isUsed = true;
        Ressources ressources = FindObjectOfType<Ressources>();
        foreach (Transform child in transform)
        {
            yield return new WaitForSeconds(cutDelay);
            child.gameObject.SetActive(false);
            ressources.AddWood(1);
        }
        Destroy(gameObject);
    }

    private bool IsMouseOverUI()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }
}
