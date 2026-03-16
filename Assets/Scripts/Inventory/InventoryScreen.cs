using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryScreen : MonoBehaviour
{
    [SerializeField] private GameObject inventoryScreen;
    [SerializeField] private List<Item> itemsToBeDisplayed;
    [SerializeField] private Transform inventoryIconParent;
    [SerializeField] private GameObject itemPrefab;
    // Start is called before the first frame update
    public void OpenInventoryMenu(List<Item> _items)
    {
        itemsToBeDisplayed = _items;
        foreach (Item item in itemsToBeDisplayed)
        {
            if (!item.inDisplay)
            {
                GameObject itemObjectInstantiated = Instantiate(itemPrefab, inventoryIconParent);
                itemObjectInstantiated.GetComponentInChildren<Image>().sprite = item.sprite;
                item.inDisplay = true;
            }
        }
        inventoryScreen.SetActive(true);
    }

    public void CloseInventoryMenu()
    {
        inventoryScreen.SetActive(false);
    }
}
